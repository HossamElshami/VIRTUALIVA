using UnityEngine;

public class MenuBtnsInformation : MonoBehaviour
{
    public MenuButtonInfoSPO data;    
    public void MouseEnter()
    {
        UI_Manager.instance.ShowInformation(data, true);
    } 
    public void MouseExit()
    {
        UI_Manager.instance.ShowInformation(data, false);        
    }
}
