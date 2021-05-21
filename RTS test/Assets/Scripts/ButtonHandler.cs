using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public List<GameObject> panels = new List<GameObject>();
    private void Start()
    {
        foreach (var pan in panels)
        {
            pan.SetActive(false);
        }
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);

        foreach(var pan in panels)
        {
            if(pan != panel)
            {
                pan.SetActive(false);
            }
        }
    }
}
