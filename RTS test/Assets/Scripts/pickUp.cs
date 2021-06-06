using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour
{
    public LayerMask clickable;
    public Item Item;

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (hit.transform.gameObject.tag == "PickUp")
                {
                    Inventory.instance.Add(Item);
                    Destroy(gameObject);
                }
            }
        }
    }
}
