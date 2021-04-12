using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getSoldierCount : MonoBehaviour
{
    Text SoldierDisplay;

    void Awake()
    {
        SoldierDisplay = this.GetComponent<Text>();    
    }

    // Update is called once per frame
    void Update()
    {
        SoldierDisplay.text = UnitSelections.Instance.unitList.Count.ToString();
    }
}
