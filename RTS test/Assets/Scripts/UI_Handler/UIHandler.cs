using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text woodText, stoneText, metalText, soldierName;
    public GameObject inventoryPanel;
    private List<GameObject> invSlots = new List<GameObject>();
    Inventory inventory;

    void Awake()
    {
        inventory = Inventory.instance;
        inventory.onInvChangedCallback += UpdateInventoryUI;
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            invSlots.Add(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }
    void Update()
    {
        if (UnitSelections.Instance.unitsSelected.Count == 1 && UnitSelections.Instance.unitsSelected[0] != null)
        {
            string name = UnitSelections.Instance.unitsSelected[0].GetComponent<UnitInfo>().unitName;
            string hunger = UnitSelections.Instance.unitsSelected[0].GetComponent<UnitInfo>().Hp.ToString();
            string health = UnitSelections.Instance.unitsSelected[0].GetComponent<UnitInfo>().Hp.ToString();
            soldierName.text = string.Format("Lt.{0} | HP : {1} | Hunger : {2}%", name, health, hunger);
        }
        else if (UnitSelections.Instance.unitsSelected.Count > 1)
        {
            soldierName.text = "Units count : " + UnitSelections.Instance.unitsSelected.Count.ToString();
        }
        else
        {
            soldierName.text = "";
        }
    }

    void UpdateInventoryUI()
    {
        woodText.text = Inventory.instance.GetCountOfItem("wood").ToString();
        stoneText.text = Inventory.instance.GetCountOfItem("stone").ToString();
        metalText.text = Inventory.instance.GetCountOfItem("metal").ToString();

        for (int i = 0; i < invSlots.Count; i++)
        {
            if(i<inventory.inventory.Count)
            {
                invSlots[i].GetComponent<inventorySlot>().setItem(inventory.inventory[i]);
            }
            else
            {
                invSlots[i].GetComponent<inventorySlot>().setItem(new Item());
            }
        }
    }
}
