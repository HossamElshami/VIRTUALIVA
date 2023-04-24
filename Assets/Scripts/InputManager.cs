using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //left click mouse
    private bool leftMouseDown = false;
    public bool LeftMouseDown { get { return leftMouseDown; } }
    //right click mouse
    private bool rightMouseDown = false;
    public bool RightMouseDown { get { return rightMouseDown; } } 
    //left click mouse hold
    private bool holdLeftMouseClick = false;
    public bool HoldLeftMouseClick { get { return holdLeftMouseClick; } }
    //horizontal value
    private float hValue = 0;
    public float HValue { get { return hValue; } } 
    //vertical value    
    private float vValue = 0;
    public float VValue { get { return vValue; } } 
    //Mouse X value
    private float mouseX = 0;
    public float MouseX { get { return mouseX; } }
    //Mouse Y value
    private float mouseY = 0;
    public float MouseY { get { return mouseY; } }
    //Mouse Posiiton value
    private Vector3 mousePos;
    public Vector3 MousePos { get { return mousePos; } }

    public static InputManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Update()
    {
        leftMouseDown = checkInput(KeyCode.Mouse0);
        rightMouseDown = checkInput(KeyCode.Mouse1);      
    }
    private void FixedUpdate()
    {
        hValue = checkInput("Horizontal");
        vValue = checkInput("Vertical");
        mouseX = checkInput("Mouse X");
        mouseY = checkInput("Mouse Y");
        mousePos = checkInput(Input.mousePosition);        
    }
    bool checkInput(KeyCode input)
    {
        if (Input.GetKey(input)) return true;
        return false;        
    }
    public bool inputDown(KeyCode input)
    {
        if (Input.GetKeyDown(input)) return true;
        return false;
    }
    float checkInput(string input)
    {
        return Input.GetAxisRaw(input);        
    }
    Vector3 checkInput(Vector3 input)
    {
        return input;        
    }
}
