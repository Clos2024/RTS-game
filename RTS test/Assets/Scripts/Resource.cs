using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    //How much of said resource is in the object
    public int WoodResource;
    public int StoneResource;
    public int MetalResource;

    //Name of the resource materials aval
    public string resourceType;

    //How many units can be on the resource.
    public int unitCapacity;
    public List<GameObject> unitsInSite = new List<GameObject>();

    void Update()
    {
        if(WoodResource <= 0 && StoneResource <= 0 && MetalResource <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public int ExtractResource()
    {
        WoodResource--;
        StoneResource--;
        MetalResource--;
        return 1;
    }

    public int getUnitCapacity()
    {
        return unitCapacity;
    }

    //This will keep track internally how many units are mining currently
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Unit>() != null)//Is this a unit;
        {
            if(!unitsInSite.Contains(other.gameObject)) //Check if this object is in the list
            {
                unitsInSite.Add(other.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Unit>() != null)//Is this a unit;
        {
            if (unitsInSite.Contains(other.gameObject)) //Check if this object is in the list
            {
                unitsInSite.Remove(other.gameObject);
            }
        }
    }
}
