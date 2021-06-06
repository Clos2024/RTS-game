using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;

    inventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onInvChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<inventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.inventory.Count)
            {
                slots[i].setItem(inventory.inventory[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
