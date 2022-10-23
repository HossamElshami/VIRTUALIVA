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
    private GameObject camera;
    [SerializeField]
    float moveX, moveY;

    private void Start()
    {
        camera = Camera.main.gameObject;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        rotateX -= mouseY * mouseSensitivity * Time.deltaTime;
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);

        camera.transform.localRotation = Quaternion.Euler(Vector3.right * rotateX);
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity * Time.deltaTime);
    }
}
