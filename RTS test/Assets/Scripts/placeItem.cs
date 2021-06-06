using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeItem : MonoBehaviour
{
    public Item item = null;
    public LayerMask myLayers;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayers))
            {
                if(hit.transform.gameObject.tag == "Player")
                {
                    if (hit.transform.gameObject.GetComponent<UnitInfo>() != null)
                    {
                        var unit = hit.transform.gameObject.GetComponent<UnitInfo>();
                        if (item.itemName.Contains("helmet"))
                        {
                            unit.equipArmor(item);
                            Destroy(gameObject);
                        }
                        else if (item.itemName.Contains("weapon"))
                        {
                            unit.equipWeapon(item);
                            Destroy(gameObject);
                        }
                        else if(item.itemName.Contains("bread"))
                        {
                            if (unit.hunger >= unit.hungerMax)
                            {
                                Inventory.instance.Add(item);
                                Destroy(gameObject);
                            }
                            else
                            {
                                unit.eat(20);
                                Destroy(gameObject);
                            }
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Inventory.instance.Add(item);
            Destroy(gameObject);
        }
    }
}
