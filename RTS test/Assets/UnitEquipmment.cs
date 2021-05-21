using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitEquipmment : MonoBehaviour
{
    public GameObject equipmentButton;

    public Item testItem1,testItem2,testItem3;

    public List<Button> equipmentSlots = new List<Button>();

    private void Start()
    {
        Inventory.instance.Add(testItem1);
        Inventory.instance.Add(testItem2);
        Inventory.instance.Add(testItem3);
    }
    // Update is called once per frame
    void Update()
    {
        if(UnitSelections.Instance.unitsSelected.Count == 1)
        {
            equipmentButton.SetActive(true);
        }
        else
        {
            equipmentButton.SetActive(false);
        }
    }

    public void showMenu()
    {
        var unitInfo = UnitSelections.Instance.unitsSelected[0].GetComponent<UnitInfo>();
        equipmentSlots[0].GetComponent<equipmentSlot>().equipmentSlotItem = unitInfo.Armor;
        equipmentSlots[3].GetComponent<equipmentSlot>().equipmentSlotItem = unitInfo.Weapon;
        equipmentSlots[0].GetComponent<equipmentSlot>().menuToggle();
        equipmentSlots[3].GetComponent<equipmentSlot>().menuToggle();
    }
}
