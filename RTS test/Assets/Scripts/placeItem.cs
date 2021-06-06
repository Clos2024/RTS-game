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
                            if (unit.Armor.itemName == "")
                            {
                                unit.equipArmor(item);
                            }
                            else
                            {
                                Item oldArmor = unit.Armor.copyItem();
                                oldArmor.setAmount(1);
                                unit.Armor = null;
                                unit.equipArmor(item);
                                Inventory.instance.Add(oldArmor);
                            }     
                            Destroy(gameObject);
                        }
                        else if (item.itemName.Contains("weapon"))
                        {
                            if(unit.Weapon.itemName == "")
                            {
                                unit.equipWeapon(item);
                            }
                            else
                            {
                                Item oldWeapon = unit.Weapon.copyItem();
                                oldWeapon.setAmount(1);
                                unit.Armor = null;
                                unit.equipWeapon(item);
                                Inventory.instance.Add(oldWeapon);
                            }  
                            Destroy(gameObject);
                        }
                        else if(item.itemName.Contains("bread"))
                        {
                            if (unit.hunger >= unit.hungerMax)
                            {
                                Inventory.instance.Add(item);
                            }
                            else
                            {
                                unit.eat(20);
                            }
                            Destroy(gameObject);
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
