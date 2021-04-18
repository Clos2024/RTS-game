using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour
{
    public string item;
    private Image itemIcon;
    private Text itemCount;

    void Awake()
    {
        itemIcon = transform.GetComponent<Image>();
    }

    public void addItem(string item, int count)
    {
        item = this.item;
        itemCount.text = count.ToString();
        itemIcon = Resources.Load(item) as Image;
    }
}
