using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipmentSlot : MonoBehaviour
{
    public Item equipmentSlotItem;
    public Image image;
    public string itemType;

    public void menuToggle()
    {
        if(equipmentSlotItem != null)
        {
            image.sprite = equipmentSlotItem.icon;
        }
    }
}
