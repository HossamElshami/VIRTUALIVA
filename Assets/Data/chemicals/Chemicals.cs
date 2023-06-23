using UnityEngine;

[CreateAssetMenu(fileName = "New Chemicals", menuName = "Chemicals/New Chemicals")]
public class Chemicals : ScriptableObject
{
    public string _name;
    public string symbol;
    public Chemicals Chemicals1, Chemicals2;
    public string color;
    public enum Type { Gas, Liquid, Solid }
    public Type type;
}
