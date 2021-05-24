using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class workBenchMenu : MonoBehaviour
{
    public GameObject craftingPanel;
    //public Button fireplaceButton;
    //private GameObject exitButton;

    private void Awake()
    {
        craftingPanel = GameObject.Find("craftingPanelParent");
        //craftingPanel = GameObject.Find("craftingPanelParent").transform.GetChild(0).gameObject;
        //exitButton = GameObject.Find("craftingPanelParent").transform.GetChild(1).gameObject;
        //fireplaceButton = transform.GetComponent<Button>();
    }

    public void TogglePanel()
    {
        if(craftingPanel.activeInHierarchy == false)
        {
            craftingPanel.SetActive(true);
        }
        else
        {
            craftingPanel.SetActive(false);
        }
    }

    //public void ButtonCLicked()
    //{
    //    if (exitButton.activeInHierarchy == false)
    //    {
    //        exitButton.SetActive(true);
    //    }
    //    else
    //    {
    //        craftingPanel.SetActive(false);
    //    }
    //}
}
