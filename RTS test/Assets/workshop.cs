using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workshop : MonoBehaviour
{
    unitCapacity unitCap;
    public GameObject buttonCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        unitCap = transform.GetComponent<unitCapacity>();
    }

    void Update()
    {
        if(unitCap.unitsInSite.Count == unitCap.unitMaxCapacity)
        {
            buttonCanvas.SetActive(true);
        }
        else
        {
            buttonCanvas.SetActive(false);
        }
    }
}
