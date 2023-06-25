using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuXrManager : MonoBehaviour
{
    public InputActionReference toggleRef = null;
    

    void Awake()
    {
        toggleRef.action.started += Toggle;
    }

    
    void OnDestroy()
    {
        toggleRef.action.started -= Toggle;
    }
    public void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
    }
}
