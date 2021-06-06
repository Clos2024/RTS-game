using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int amount;
    public int Dmg,armor;
    public bool withdrawable;

    public Item(string itemName = "", Sprite icon = null, int amount = 0, int Dmg = 0, int armor = 0, bool withdrawable = false)
    {
        this.itemName = itemName;
        this.icon = icon;
        this.amount = amount;
        this.Dmg = Dmg;
        this.armor = armor;
        this.withdrawable = withdrawable;
    }

    public Item copyItem()
    {
        return new Item(itemName, icon, amount, Dmg, armor, withdrawable);
    }

    public void setAmount(int amount)
    {
        this.amount = amount;
    }
}
