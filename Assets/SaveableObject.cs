using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum ObjectType { Tools,Decor }

public abstract class SaveableObject : MonoBehaviour
{
    protected string save;
    [SerializeField]
    private ObjectType objectType;

    void Start()
    {
        SaveGameManager.Instance.SaveableObjects.Add(this);
    }

    public virtual void Save(int id)
    {
        PlayerPrefs.SetString(Application.loadedLevel.ToString() + "-" + id.ToString(), gameObject.name + "_" + transform.position.ToString() + "_" + transform.localScale.ToString() + "_" + transform.localRotation.ToString());
    }
    public virtual void Load(string[] values)
    {
        transform.localPosition = SaveGameManager.Instance.StringToVector(values[1]);
        transform.localScale = SaveGameManager.Instance.StringToVector(values[2]);
        transform.localRotation = SaveGameManager.Instance.StringToQuaternion(values[3]);
    }
    public virtual void DestroySaveable()
    {
        SaveGameManager.Instance.SaveableObjects.Remove(this);
        Destroy(gameObject);
    }
}
