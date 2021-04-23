using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workBenchMenu : MonoBehaviour
{
    public GameObject craftingPanel;
    
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
}
