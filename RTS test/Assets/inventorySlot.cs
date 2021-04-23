using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour
{
    public Image imageSlot;
    public Text textAmount;
    private Item item;

    public void setItem(Item item)
    {
            this.item = item;
            textAmount.text = item.amount.ToString();
            imageSlot.sprite = item.icon;
    }
    public bool hasItem()
    {
        if (item == null)
            return false;
        else
            return true;
    }
}
