using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeItem : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]
    private Item itemWithdrawn;
    public LayerMask m_layerMask;

    // Start is called before the first frame update
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 8)))
        {
            transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.position = hit.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.transform.gameObject.GetComponent<Unit>() != null)
            {
                Debug.Log("HitPlayer");
                if (itemWithdrawn.itemName == "bread")
                {
                    hit.transform.gameObject.GetComponent<UnitInfo>().hunger += 25;
                    Inventory.instance.consumeItem("bread", 1);
                }
                else if (itemWithdrawn.itemName.Contains("helmet"))
                {
                    Debug.Log("Applying Armor");
                    hit.transform.gameObject.GetComponent<UnitInfo>().equipArmor(new Item { itemName = itemWithdrawn.itemName, icon = itemWithdrawn.icon, amount = 1, withdrawable = true });
                }
                else if (itemWithdrawn.itemName.Contains("weapon"))
                {
                    Debug.Log("Applying Weapon");
                    hit.transform.gameObject.GetComponent<UnitInfo>().equipWeapon(new Item { itemName = itemWithdrawn.itemName, icon = itemWithdrawn.icon, amount = 1, withdrawable = true });
                }
            }
            else if (hit.transform.gameObject.GetComponent<Unit>() == null)
            {
                Inventory.instance.Add(itemWithdrawn);
            }
            Destroy(gameObject);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
            Inventory.instance.Add(itemWithdrawn);
        }

    }

    public void SetItem(Item item)
    {
        itemWithdrawn = new Item { itemName = item.itemName, icon = item.icon, amount = 1, withdrawable = true };
    }
}
