using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventroy : MonoBehaviour
{
    [SerializeField]
    private int wood, stone, metal, wheat;

    public LayerMask clickable;

    public void AddWood(int amount)
    {
        wood += amount;
    }
    public void AddStone(int amount)
    {
        stone += amount;
    }
    public int GetWood()
    {
        return wood;
    }
    public int GetStone()
    {
        return stone;
    }
    public int GetMetal()
    {
        return metal;
    }
    public void SubtractWood(int cost)
    {
        wood -= cost;
    }
    public void SubtractStone(int cost)
    {
        stone -= cost;
    }
    public void SubtractMetal(int cost)
    {
        metal -= cost;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if(hit.transform.gameObject.tag == "Wheat")
                {
                    Destroy(hit.transform.gameObject);
                    wheat++;
                }
            }
        }
    }
}
