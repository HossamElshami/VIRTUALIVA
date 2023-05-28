using UnityEngine;
[CreateAssetMenu(fileName = "ToolData",menuName = "Tools/New Tool Data")]
public class Tool_SO : ScriptableObject
{
    public string toolName;
    [TextArea]
    public string toolDescription;
    public string toolCategory;
    public string toolVersion;
    public Sprite toolIcon;

    public float minSize, maxSize;
    public bool Scaleable, Moveable, Rotateable, Skinable, Options, Material;
}
