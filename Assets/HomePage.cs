using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HomePage : MonoBehaviour
{
    public GameObject[] Tabs;
    public GameObject[] TabBtns;
    int indexOfTab = 0;
    private void Start()
    {
        for (int i = 0; i < Tabs.Length; i++)
            Tabs[i].SetActive(false);

        Tabs[0].SetActive(true);
        TabBtns[0].GetComponent<Animator>().SetBool("Selected", true);
    }

    public void SelectTab(GameObject tab)
    {
        for (int i = 0; i < Tabs.Length; i++)
        {
            Tabs[i].SetActive(false);
            TabBtns[i].GetComponent<Animator>().SetBool("Selected", false);
            if (tab == Tabs[i]) indexOfTab = i;
        }
        TabBtns[indexOfTab].GetComponent<Animator>().SetBool("Selected", true);
        tab.SetActive(true);
    }
}
