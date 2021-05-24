using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int HP;

    //Name of the resource materials aval
    public string resourceType;

    //Store log prefabs
    public int resources;
    public GameObject resourcePrefab;
    public Transform origin;
    public GameObject smokePrefab;
    public GameObject condition1, condition2, condition3;
    public List<Vector3> angles = new List<Vector3>();

    void Update()
    {
        if(HP <= 0)
        {
            for (int i = 0; i < resources; i++)
            {
                var resource = Instantiate(resourcePrefab);
                resource.name = resourceType;
                resource.transform.position = origin.position + (new Vector3(Random.Range(-1, 1), Random.Range(-1, 2), Random.Range(-1, 1)));
            }
            resources = 0;
            Destroy(gameObject);

        }

        if (condition1 != null && condition2 != null && condition3 != null)
        {
            if (HP < 33)
            {
                condition1.SetActive(false);
                condition2.SetActive(false);
                condition3.SetActive(true);
            }
            else if (HP < 17)
            {
                condition1.SetActive(false);
                condition2.SetActive(true);
            }
        }
    }

    public void ExtractResource()
    {
        HP--;
        Instantiate(smokePrefab,transform);
    }
}
