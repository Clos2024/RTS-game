using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprint : MonoBehaviour
{
    RaycastHit hit;
    public GameObject prefab;

    public int woodCost, stoneCost, metalCost;

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
            var wood = Inventory.instance.GetCountOfItem("wood");
            var stone = Inventory.instance.GetCountOfItem("stone");
            var metal = Inventory.instance.GetCountOfItem("metal");

            Debug.Log(wood >= woodCost);
            Debug.Log(stone >= stoneCost);
            Debug.Log(metal >= metalCost);

            if (wood >= woodCost && stone >= stoneCost && metal >= metalCost)
            {
                Debug.Log("Buildings Place");
                placePrefab();
            }
            Destroy(gameObject);
        }
    }
    void placePrefab()
    {
        Inventory.instance.consumeItem("wood", woodCost);
        Inventory.instance.consumeItem("stone", stoneCost);
        Inventory.instance.consumeItem("metal", metalCost);
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
