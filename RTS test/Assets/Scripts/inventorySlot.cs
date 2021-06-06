using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour
{
    public Image imageSlot;
    public Text textAmount;
    public Item item;
    public Button button;
    public GameObject equipObj;

    public void setItem(Item newItem)
    {
        item = newItem;
        imageSlot.sprite = item.icon;
        imageSlot.enabled = true;
        button.interactable = item.withdrawable;
        textAmount.text = Inventory.instance.GetCountOfItem(item.itemName).ToString();
    }

    public void ClearSlot()
    {
        item = null;
        imageSlot.sprite = null;
        imageSlot.enabled = false;
        textAmount.text = 0.ToString();
        button.interactable = false;
    }

    public void withdraw()
    {
        GameObject withdrawItem = Instantiate(equipObj);
        placeItem PlaceItem = withdrawItem.GetComponent<placeItem>();
        PlaceItem.item = new Item(item.itemName,item.icon,item.amount,item.Dmg,item.armor,item.withdrawable);
        Inventory.instance.Remove(item.itemName);
    }
}
