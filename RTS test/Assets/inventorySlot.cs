using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour
{
    public Image imageSlot;
    public Text textAmount;
    public Item item;
    public GameObject bread;
    public Button InventoryButton;

    private void Awake()
    {
        InventoryButton = transform.GetComponent<Button>();
        InventoryButton.onClick.AddListener(withdrawItem);
    }

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
            Instantiate(bread);
            Inventory.instance.Remove(item.itemName);
        }
    }
}
