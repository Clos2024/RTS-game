using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class craftResearch : MonoBehaviour
{
    public int woodCost, stoneCost, metalCost;
    public Text craftingCostText;

    void Start()
    {
        craftingCostText.text = "Cost: " +
        System.Environment.NewLine + "Wood- " + woodCost.ToString() +
        System.Environment.NewLine + "Stone- " + stoneCost.ToString() +
        System.Environment.NewLine + "Metal- " + metalCost.ToString();
    }

    public void craft()
    {
        if (woodCost <= Inventory.instance.GetCountOfItem("wood") && stoneCost <= Inventory.instance.GetCountOfItem("stone") && stoneCost <= Inventory.instance.GetCountOfItem("stone"))
        {
            Debug.Log("test");
            craftItem();
        }
    }

    void craftItem()
    {
        Inventory.instance.consumeItem("wood", woodCost);
        Inventory.instance.consumeItem("stone", stoneCost);
        Inventory.instance.consumeItem("metal", metalCost);
        shipProgress.instance.increaseProgress();
    }
}
