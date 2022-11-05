using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float Speed = 5f;
    [SerializeField]
    private float mouseSensitivity = 100f;
    float rotateX = 0;
    [SerializeField]
    private new GameObject camera;
    [SerializeField]
    float moveX, moveY;

    private void Start()
    {
        camera = Camera.main.gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        updateMouseVisibale();
        RotateCamera(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move(moveX, moveY);
    }
    void Move(float horizontal, float vertical)
    {
        Vector3 dir = new Vector3(horizontal, 0, vertical) * Speed * Time.fixedDeltaTime;
        dir = transform.TransformDirection(dir);
        transform.position += dir;
    }
    void RotateCamera(float mouseX, float mouseY)
    {
        if (UI_Manager.instance.panelOpen) return;
        rotateX -= mouseY * mouseSensitivity * Time.deltaTime;
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(Vector3.right * rotateX);
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity * Time.deltaTime);
    }
    void updateMouseVisibale()
    {
        Cursor.visible = UI_Manager.instance.panelOpen;
        Cursor.lockState = UI_Manager.instance.panelOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
