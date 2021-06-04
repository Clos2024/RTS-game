using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeItem : MonoBehaviour
{
    [SerializeField]
    private Item itemWithdrawn;
    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (hit.transform.gameObject.GetComponent<Unit>() != null)
            //{
            //    Debug.Log("HitPlayer");
            //    if (itemWithdrawn.itemName == "bread")
            //    {
            //        hit.transform.gameObject.GetComponent<UnitInfo>().hunger += 25;
            //        Inventory.instance.consumeItem("bread", 1);
            //    }
            //    else if (itemWithdrawn.itemName.Contains("helmet"))
            //    {
            //        hit.transform.gameObject.GetComponent<UnitInfo>().equipArmor(new Item { itemName = itemWithdrawn.itemName, icon = itemWithdrawn.icon, amount = 1, withdrawable = true });
            //    }
            //    else if (itemWithdrawn.itemName.Contains("weapon"))
            //    {
            //        hit.transform.gameObject.GetComponent<UnitInfo>().equipWeapon(new Item { itemName = itemWithdrawn.itemName, icon = itemWithdrawn.icon, amount = 1, withdrawable = true });
            //    }
            //    else
            //    {
            //        Debug.Log("hitplayer but didnt know what to do");
            //    }
            //}
            //else
            //{
            //    Debug.Log("Inventory miss");
            //    Inventory.instance.Add(itemWithdrawn);
            //}
            //Destroy(gameObject);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            inventory.Add(itemWithdrawn);
            Debug.Log("PlaceItem - Adding Item: " + itemWithdrawn.itemName);
            Destroy(this.gameObject);
        }
    }

    public void SetItem(Item item)
    {
        itemWithdrawn = null;
        itemWithdrawn = new Item(item.itemName, item.icon, 1, true);
        Debug.Log("Withdrawn item on mouse: " + itemWithdrawn.itemName);
    }
}
