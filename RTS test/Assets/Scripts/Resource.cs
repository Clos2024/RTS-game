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
}
