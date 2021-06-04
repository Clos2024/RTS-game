using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int amount;
    public bool withdrawable;
    public int Dmg = 0,armor = 0;

    public Item(string itemName = "", Sprite icon = null, int amount = 0, bool withdrawable = false, int Dmg = 0, int armor = 0)
    {
        this.itemName = itemName;
        this.icon = icon;
        this.amount = amount;
        this.withdrawable = withdrawable;
        this.Dmg = Dmg;
        this.armor = armor;
    }
}
