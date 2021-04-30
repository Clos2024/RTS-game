using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeItem : MonoBehaviour
{
    RaycastHit hit;

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

        if (Input.GetMouseButton(0))
        {
            if (hit.transform.gameObject.GetComponent<Unit>() != null)
            {
                hit.transform.gameObject.GetComponent<Unit>().hunger += 25;
                Inventory.instance.consumeItem("bread",1);
            }
            else
            {
                Inventory.instance.inventory.Add(new Item { itemName = "bread", icon = null, amount = 1, withdrawable = true });
            }
            Destroy(gameObject);
        }

    }
}
