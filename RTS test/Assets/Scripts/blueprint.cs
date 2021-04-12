using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprint : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;
    Inventroy playerInv;
    public int cost;

    void Awake()
    {
        playerInv = GameObject.Find("Inventory").GetComponent<Inventroy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, (1<<8)))
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
            transform.position = hit.point + new Vector3 (0,1,0);
        }

        if (Input.GetMouseButton(0))
        {
            if (playerInv.GetWood() >= 2)
            {
                playerInv.SubtractWood(cost);
                Instantiate(prefab, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
