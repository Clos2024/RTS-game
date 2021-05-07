using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmSite : MonoBehaviour
{
    public GameObject seedOne, seedTwo, seedThree;
    unitCapacity unitCap;

    private void Awake()
    {
        unitCap = transform.GetComponent<unitCapacity>();
    }
    private void Update()
    {
        if (unitCap.unitsInSite.Count == 1)
        {
            seedOne.GetComponent<SeedBehavior>().GrowthTimer();
        }
        else if (unitCap.unitsInSite.Count == 2)
        {
            seedOne.GetComponent<SeedBehavior>().GrowthTimer();
            seedTwo.GetComponent<SeedBehavior>().GrowthTimer();
        }
        else if (unitCap.unitsInSite.Count == 3)
        {
            seedOne.GetComponent<SeedBehavior>().GrowthTimer();
            seedTwo.GetComponent<SeedBehavior>().GrowthTimer();
            seedThree.GetComponent<SeedBehavior>().GrowthTimer();
        }
    }
}
