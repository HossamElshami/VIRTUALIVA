using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    private static SaveGameManager instance;

    public List<SaveableObject> SaveableObjects{ get; private set; }

    public static SaveGameManager Instance {
        get
        {
            if (instance == null) instance = FindObjectOfType<SaveGameManager>();
            return instance;
        } 
    }

    private void Awake()
    {
        SaveableObjects = new List<SaveableObject>();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(Application.loadedLevel.ToString(), SaveableObjects.Count);
        for (int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i);
        }
    }

    public void Load()
    {
        foreach (SaveableObject obj in SaveableObjects)
        {
            if(obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        SaveableObjects.Clear();

        int objectCount = PlayerPrefs.GetInt(Application.loadedLevel.ToString());
        for (int i = 0; i < objectCount; i++)
        {
            string[] value = PlayerPrefs.GetString(Application.loadedLevel.ToString() + "-" + i.ToString()).Split('_');
            GameObject tmp = Instantiate(Resources.Load(value[0].Replace("(Clone)", "")) as GameObject);
            tmp.GetComponent<SaveableObject>().Load(value);
        }
    }
    public Vector3 StringToVector(string value)
    {
        //(1, 2, 3)
        value = value.Trim(new char[] { '(',')'});
        //1, 2, 3
        value = value.Replace(" ", "");
        //1,2,3
        string[] pos = value.Split(',');
        //[0]=1,[1]=2,[2]=3

        return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
    }
    public Quaternion StringToQuaternion(string value)
    {
        //(1, 2, 3)
        value = value.Trim(new char[] { '(', ')' });
        //1, 2, 3
        value = value.Replace(" ", "");
        //1,2,3
        string[] pos = value.Split(',');
        //[0]=1,[1]=2,[2]=3

        return new Quaternion(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]), float.Parse(pos[3]));
    }
}
