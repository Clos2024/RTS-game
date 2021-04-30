using UnityEngine;
using UnityEngine.UI;

public class cooking : MonoBehaviour
{
    public int wheatCost;
    public Text craftingCostText;
    private Item craftingItem;
    public string itemName;
    public Sprite icon;

    // Start is called before the first frame update
    void Start()
    {
        craftingCostText.text = "Cost: " +
        System.Environment.NewLine + "Wheat- " + wheatCost.ToString();
    }

    public void craft()
    {
        if (wheatCost <= Inventory.instance.GetCountOfItem("wheat"))
        {
            craftItem();
        }
    }

    void craftItem()
    {
        craftingItem = new Item { itemName = itemName, amount = 1, icon = icon, withdrawable = true};

        if (Inventory.instance.inventory.Find(x => x.itemName == craftingItem.itemName) == null)
            Inventory.instance.Add(craftingItem);
        else
            Inventory.instance.inventory.Find(x => x.itemName == craftingItem.itemName).amount++;

        Inventory.instance.consumeItem("wheat", wheatCost);
    }
}
