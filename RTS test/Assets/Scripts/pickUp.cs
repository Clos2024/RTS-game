using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour
{
    public LayerMask clickable;
    public string Name;
    public Sprite itemIcon;
    public Item Item;

    void Awake()
    {
        Item = new Item { amount = 1, itemName = Name, icon = itemIcon};
    }
    // Update is called once per frame
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
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
