using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public delegate void OnInvChange();

    public OnInvChange onInvChangedCallback;

    public List<Item> inventory = new List<Item>();

    public LayerMask clickable;


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    public void Add(Item item)
    {
                                                                                                    //If this item is in the inventory increase the amount
        if (inventory.Find(x => x.itemName == item.itemName) != null)                               //Use item's name to check if its in inventory
            inventory.Find(x => x.itemName == item.itemName).amount++;
        else                                                                                        //If we cant find a item with the same name make a new item.
            inventory.Add(new Item { amount = 1, itemName = item.itemName, icon = item.icon, withdrawable = item.withdrawable });

        if(onInvChangedCallback != null)
            onInvChangedCallback.Invoke();
    }
    public void Remove(string name)
    {
        if(inventory.Find(x=> x.itemName == name) == null)
        {
            return;
        }

        if (inventory.Find(x=> x.itemName == name).amount > 1)
        {
            inventory.Find(x => x.itemName == name).amount--;
        }
        else
        {
            inventory.Remove(inventory.Find(x => x.itemName == name));
        }

        if (onInvChangedCallback != null)
            onInvChangedCallback.Invoke();
    }

    public void consumeItem(string name, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Inventory.instance.Remove(name);
        }
    }

    public int GetCountOfItem(string name)
    {
        if (inventory.Find(x => x.itemName == name) != null)
            return inventory.Find(x => x.itemName == name).amount;
        else
            return 0;
    }

}
