using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    public List<SaveableObject> SaveableObjects { get; private set; }
    public static SaveManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<SaveManager>();
            return instance;
        }
    }
    private void Awake()
    {
        SaveableObjects = new List<SaveableObject>();
    }
    public void Save()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().ToString(), SaveableObjects.Count);
        for (int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i);
        }
    }
    public void Load()
    {
        foreach (SaveableObject obj in SaveableObjects)
        {
            if (obj != null)
            {
                Destroy(obj.gameObject);
            }
        }
        SaveableObjects.Clear();

        int objectCount = PlayerPrefs.GetInt(SceneManager.GetActiveScene().ToString());
        for (int i = 0; i < objectCount; i++)
        {
            string[] value = PlayerPrefs.GetString(SceneManager.GetActiveScene().ToString() + "-" + i.ToString()).Split('_');
            GameObject tmp = Instantiate(Resources.Load(value[0].Replace("(Clone)", "")) as GameObject);
            tmp.GetComponent<SaveableObject>().Load(value);
        }
    }
    public Vector3 StringToVector(string value)
    {
        value = value.Trim(new char[] { '(', ')' });  //(1, 2, 3)        
        value = value.Replace(" ", "");  //1, 2, 3
        string[] pos = value.Split(',');  //1,2,3
        return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));  //[0]=1,[1]=2,[2]=3
    }
    public Quaternion StringToQuaternion(string value)
    {
        value = value.Trim(new char[] { '(', ')' });  //(1, 2, 3)
        value = value.Replace(" ", "");  //1, 2, 3
        string[] pos = value.Split(',');  //1,2,3        
        return new Quaternion(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]), float.Parse(pos[3]));  //[0]=1,[1]=2,[2]=3
    }
}
