using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crafting : MonoBehaviour
{
    public int woodCost, stoneCost, metalCost;
    //public Text craftingCostText;
    private Item craftingItem;
    public string itemName;
    public Sprite icon;

    // Start is called before the first frame update
    void Start()
    {
        //craftingCostText.text = "Cost: " + 
        //System.Environment.NewLine + "Wood- " + woodCost.ToString() + 
        //System.Environment.NewLine + "Stone- " + stoneCost.ToString() + 
        //System.Environment.NewLine + "Metal- " + metalCost.ToString();
    }

    public void craft()
    {
        var woodAval = Inventory.instance.GetCountOfItem("wood");
        var stoneAval = Inventory.instance.GetCountOfItem("stone");
        var metalAval = Inventory.instance.GetCountOfItem("metal");

        if (woodAval >= woodCost && stoneAval >= stoneCost && metalAval >= metalCost)
        {
            Debug.Log("Craft Item");
            craftItem();
        }
    }

    void craftItem()
    {
        craftingItem = new Item { itemName = itemName, amount = 1, icon = icon };

        if (Inventory.instance.inventory.Find(x => x.itemName == craftingItem.itemName) == null)
            Inventory.instance.Add(craftingItem);
        else
            Inventory.instance.inventory.Find(x => x.itemName == craftingItem.itemName).amount++;

        Inventory.instance.consumeItem("wood", woodCost);
        Inventory.instance.consumeItem("stone", stoneCost);
        Inventory.instance.consumeItem("metal", metalCost);
    }
}
