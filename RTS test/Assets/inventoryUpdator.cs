using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryUpdator : MonoBehaviour
{
    public GameObject inventoryPanel;
    private List<GameObject> inventorySlots =  new List<GameObject>();
    Inventroy inv;

    void Awake()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventroy>();
        for(int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            if(inventoryPanel.transform.GetChild(i).GetComponent<inventorySlot>() != null)
            {
                inventorySlots.Add(inventoryPanel.transform.GetChild(i).gameObject);
            }
        }
    }
    public void updateInventorySlots()
    {
        foreach(var slot in inventorySlots)
        {
            foreach (var item in inv.inventory)
            {
                if (slot.GetComponent<inventorySlot>().item == "")
                {
                    slot.GetComponent<inventorySlot>().addItem(item.name, 0);
                }
            }
        }
    }
}
