using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildPanelControl : MonoBehaviour
{
    private bool active;
    public GameObject buildPanel;

    // Update is called once per frame
    void Update()
    {
        buildPanel.SetActive(active);
    }

    public void activatePanel()
    {
        if (active)
            active = false;
        else
            active = true;
    }
}
