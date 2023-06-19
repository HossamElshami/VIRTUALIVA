using UnityEngine;
public class EditItemPage : MonoBehaviour
{
    Tool tool;
    DragingSystem dragingSystem;
    public GameObject mainObject;
    public static EditItemPage instance;
    public bool isEditting = false;
    [SerializeField] bool isRotating = false, isScaling = false;
    public bool isrotating { get { return isRotating; } }
    [SerializeField] GameObject[] itemOptions;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        dragingSystem = LabManager.instance.character.GetComponent<DragingSystem>();
    }
    private void Update()
    {
        if (!mainObject) return;

        tool = mainObject.GetComponent<Tool>();
        itemOptions[0].SetActive(tool.toolData.Rotateable);
        itemOptions[1].SetActive(tool.toolData.Scaleable);
        itemOptions[2].SetActive(tool.toolData.Material);
        itemOptions[3].SetActive(tool.toolData.Options);
        itemControls();

        if (isRotating) Rotate();
        if (isScaling) Scale();
    }

    public void Scale()
    {
        if (InputManager.instance.inputDown(KeyCode.Period) &&
             mainObject.transform.localScale.x < mainObject.GetComponent<Tool>().toolData.maxSize)
        {
            mainObject.transform.localScale *= 1.25f;
        }
        else if (InputManager.instance.inputDown(KeyCode.Comma) &&
         mainObject.transform.localScale.x > mainObject.GetComponent<Tool>().toolData.minSize)
        {
            mainObject.transform.localScale *= 0.75f;
        }
    }
    public void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * 20f;
        float mouseY = Input.GetAxis("Mouse Y") * 20f;

        mainObject.transform.Rotate(-Vector3.down, mouseX, Space.World);
        mainObject.transform.Rotate(Camera.main.gameObject.transform.right, mouseY, Space.World);
    }
    void itemControls()
    {
        if (!UI_Manager.instance.optionPanel.activeInHierarchy) return;

        switch (InputManager.instance.GetButton())
        {
            case "q":
                mainObject.transform.SetPositionAndRotation(dragingSystem.itemTransform.position, dragingSystem.itemTransform.rotation);
                mainObject.transform.localScale = Vector3.one;
                break;
            case "r":
                if (!tool.toolData.Rotateable) return;
                isRotating = !isRotating;
                HideControls("Move mouse to rotate item, Click [R] to out");
                break;
            case "t":
                if (!tool.toolData.Scaleable) return;
                isScaling = !isScaling;
                HideControls("Click [<] and [>] to scale item, Click [T] to out");
                break;
            default:
                break;
        }
    }
    void HideControls(string hint)
    {
        isEditting = !isEditting;
        string toolName = mainObject.GetComponent<Tool>().toolData.toolName;
        UI_Manager.instance.ItemNameText.text = hint;
        UI_Manager.instance.editItemOptions.SetActive(!isEditting);
    }
}
