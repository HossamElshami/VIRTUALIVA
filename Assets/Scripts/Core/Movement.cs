using UnityEngine;

public class Movement : MonoBehaviour
{
    private new GameObject camera;

    private float mouseSensitivity = 500f;
    float moveX, moveY, rotateX = 0, Speed = 5f;

    private void Start()
    {
        camera = Camera.main.gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //updateMouseVisibale();
        if (EditItemPage.instance.isEditting) return;

        moveX = InputManager.instance.HValue;
        moveY = InputManager.instance.VValue;
        RotateCamera(InputManager.instance.MouseX, InputManager.instance.MouseY);
    }

    private void FixedUpdate()
    {
        if (LabManager.instance.editingItem) return;

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
