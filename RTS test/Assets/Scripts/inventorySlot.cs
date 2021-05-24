using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour
{
    public Image imageSlot;
    public Text textAmount;
    public Item item;
    public GameObject itemObj;

    public void setItem(Item newItem)
    {
        item = newItem;
        textAmount.text = newItem.amount.ToString();
        imageSlot.sprite = newItem.icon;
    }

    public void withdrawItem()
    {
        if(item.withdrawable == true)
        {
            Instantiate(itemObj);
            itemObj.GetComponent<placeItem>().SetItem(new Item { itemName = item.itemName, icon = item.icon, amount = 1, withdrawable = true });
            Inventory.instance.Remove(item.itemName);
        }
    }
}
