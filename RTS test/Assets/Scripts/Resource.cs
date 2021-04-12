using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int WoodResource;
    public string resourceType = "wood";
    public int unitCapacity = 4;
    // Start is called before the first frame update
    void Start()
    {
        WoodResource = 20;    
    }

    void Update()
    {
        if(WoodResource <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public int ExtractResource()
    {
        WoodResource--;
        return 1;
    }

    public int getUnitCapacity()
    {
        return unitCapacity;
    }
}
