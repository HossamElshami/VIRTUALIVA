using UnityEngine;
using UnityEditor;

public class MakeToolWindow : EditorWindow
{
    string fileName;
    NewTool _tool;
    [MenuItem("Window/Custom editor/Make tool Data")]
    public static void ShowWindow()
    {
        GetWindow<MakeToolWindow>("Make Tool Data");
    }
    private void OnEnable()
    {
        _tool = new NewTool();
    }
    void OnGUI()
    {
        GUILayout.Label("Make new tool data");
        fileName = _tool.toolName + "_Data";
        _tool.toolName = createTextField("Tool name", _tool.toolName);
        _tool.toolDescription = createTextArea("Tool Description", _tool.toolDescription);
        _tool.toolCategory = createTextField("Tool category", _tool.toolCategory);
        _tool.toolVersion = createTextField("Tool version", _tool.toolVersion);

        _tool.toolIcon = createSpriteField("Tool icon", _tool.toolIcon);

        _tool.Scaleable = createCheckBox("Scaleable", _tool.Scaleable);
        if (_tool.Scaleable)
        {
            _tool.minSize = float.Parse(createTextField("Min size", _tool.minSize.ToString()));
            _tool.maxSize = float.Parse(createTextField("Max size", _tool.maxSize.ToString()));
        }
        _tool.Moveable = createCheckBox("Moveable", _tool.Moveable);
        _tool.Rotateable = createCheckBox("Rotateable", _tool.Rotateable);
        _tool.Skinable = createCheckBox("Skinable", _tool.Skinable);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            if (string.IsNullOrEmpty(_tool.toolName) | string.IsNullOrEmpty(_tool.toolDescription) | string.IsNullOrEmpty(_tool.toolCategory) | string.IsNullOrEmpty(_tool.toolVersion))
                Debug.LogWarning("Some fields is requeired!");
            else
                CreateMyAsset(fileName, _tool);
        }
        if (GUILayout.Button("Create & Clear"))
        {
            CreateMyAsset(fileName, _tool);
            clearData();
        }
        GUILayout.EndHorizontal();
    }
    public static void CreateMyAsset(string fileName, NewTool t)
    {
        Tool_SO tool = CreateInstance<Tool_SO>();
        tool.toolName = t.toolName;
        tool.toolDescription = t.toolDescription;
        tool.toolCategory = t.toolCategory;
        tool.toolVersion = t.toolVersion;
        tool.toolIcon = t.toolIcon;
        tool.Scaleable = t.Scaleable;
        tool.Rotateable = t.Rotateable;
        tool.Moveable = t.Moveable;
        tool.Skinable = t.Skinable;
        tool.minSize = t.minSize;
        tool.maxSize = t.maxSize;
        
        string name = AssetDatabase.GenerateUniqueAssetPath($"Assets/Data/Tools/{fileName}.asset");
        AssetDatabase.CreateAsset(tool, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = tool;
    }
    string createTextField(string label, string data)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        GUILayout.FlexibleSpace();
        data = EditorGUILayout.TextField("", data);
        GUILayout.EndHorizontal();
        return data;
    }
    string createTextArea(string label, string data)
    {
        GUILayout.Label(label);
        data = EditorGUILayout.TextField("", data);
        return data;
    }
    Sprite createSpriteField(string label, Sprite icon)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        GUILayout.FlexibleSpace();
        Sprite input = (Sprite)EditorGUILayout.ObjectField(icon, typeof(Sprite), allowSceneObjects: true);
        GUILayout.EndHorizontal();
        return input;
    }
    bool createCheckBox(string label, bool check)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        GUILayout.FlexibleSpace();
        bool input = EditorGUILayout.Toggle(check);
        GUILayout.EndHorizontal();
        return input;
    }
    void clearData()
    {
        _tool = new NewTool();
    }
}
public class NewTool
{
    public string toolName;
    [TextArea]
    public string toolDescription;
    public string toolCategory;
    public string toolVersion;
    public Sprite toolIcon;

    public float minSize, maxSize;
    public bool Scaleable, Moveable, Rotateable, Skinable;
}