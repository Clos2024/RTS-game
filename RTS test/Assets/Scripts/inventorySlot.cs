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
    private Inventory inventory;
    public Button slotButton;


     void Awake()
    {
        inventory = Inventory.instance;
        slotButton = transform.GetComponent<Button>();
        slotButton.onClick.AddListener(withdrawItem);
    }

    public void setItem(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;
            textAmount.text = item.amount.ToString();
            imageSlot.sprite = item.icon;
        }
        else
        {
            return;
        }
    }

    public void withdrawItem()
    {
        if(item.withdrawable == true)
        {
            Instantiate(itemObj);
            placeItem placeItemScript = itemObj.GetComponent<placeItem>();
            Debug.Log("Withdrawing item: " + item.itemName);
            placeItemScript.SetItem(new Item(item.itemName, item.icon, 1, true));
            inventory.Remove(item.itemName);
            item = new Item();
        }
    }
}
