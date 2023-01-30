using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditItemPage : MonoBehaviour
{
    public GameObject mainObject;
    public static EditItemPage instance;
    void Awake()
    {
        instance = this;
    }

    public void Scale(float ratio)
    {
        mainObject.transform.localScale = transform.localScale * ratio;
    }
    public void Rotate(float ratio)
    {
        mainObject.transform.Rotate(Vector3.up * ratio);
    }
}
