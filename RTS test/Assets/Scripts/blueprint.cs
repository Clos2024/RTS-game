using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprint : MonoBehaviour
{
    RaycastHit hit;
    public GameObject prefab;
    Inventroy playerInv;

    public int woodCost, stoneCost, metalCost;


    void Awake()
    {
        playerInv = GameObject.Find("Inventory").GetComponent<Inventroy>();
    }
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
            if(woodCost <= playerInv.GetWood() && stoneCost <= playerInv.GetStone() && metalCost <= playerInv.GetMetal())
            {
                var building = Instantiate(prefab,transform.position,transform.rotation);
                playerInv.SubtractWood(woodCost);
                playerInv.SubtractStone(stoneCost);
                playerInv.SubtractMetal(metalCost);
            }
            Destroy(gameObject);
        }
    }
}
