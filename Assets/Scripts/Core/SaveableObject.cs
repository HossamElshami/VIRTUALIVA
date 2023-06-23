using UnityEngine;
using UnityEngine.SceneManagement;

enum ObjectType { Tools, Decor }

public abstract class SaveableObject : MonoBehaviour
{
    protected string save;
    [SerializeField]
    private ObjectType objectType;

    void Start()
    {
        SaveManager.Instance.SaveableObjects.Add(this);
    }

    public virtual void Save(int id)
    {
        PlayerPrefs.SetString(SceneManager.GetActiveScene().ToString() + "-" + id.ToString(), gameObject.name + "_" + transform.position.ToString() + "_" + transform.localScale.ToString() + "_" + transform.localRotation.ToString());
    }
    public virtual void Load(string[] values)
    {
        transform.localPosition = SaveManager.Instance.StringToVector(values[1]);
        transform.localScale = SaveManager.Instance.StringToVector(values[2]);
        transform.localRotation = SaveManager.Instance.StringToQuaternion(values[3]);
    }
    public virtual void DestroySaveable()
    {
        SaveManager.Instance.SaveableObjects.Remove(this);
        Destroy(gameObject);
    }
}
