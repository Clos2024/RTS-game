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

    void Update()
    {
        if(HP <= 0)
        {
            for (int i = 0; i < resources; i++)
            {
                var resource = Instantiate(resourcePrefab);
                resource.name = resourceType;
                resource.transform.position = origin.position;
            }
            resources = 0;
            Destroy(gameObject);
        }
    }

    public void ExtractResource()
    {
        HP--;
    }
}
