using UnityEngine;

[CreateAssetMenu(fileName = "New MenuBtnInfo", menuName = "AI Information Data/New MenuDataInfo")] 
public class MenuButtonInfoSPO : ScriptableObject
{
    public string title;
    [TextArea]    
    public string description;
}
