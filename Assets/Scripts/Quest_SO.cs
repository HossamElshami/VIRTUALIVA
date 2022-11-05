using UnityEngine;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Quest/Quest data")]
public class Quest_SO : ScriptableObject
{
    public int ID;
    public int grade;
    public string description;
}
