using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelter : MonoBehaviour
{
    unitCapacity unitCap;
    public float restTimerMax;
    private float restTimer;
    public int hungerCost, HealthIncrease;

    void Awake()
    {
        unitCap = transform.GetComponent<unitCapacity>();
    }

    void Update()
    {
        if (unitCap.unitsInSite.Count != 0)
            RestTimer();
    }

    void RestTimer()
    {
        if (restTimer > 0)
            restTimer -= 1 * Time.deltaTime;
        else
            Rest();
    }
    void Rest()
    {
        foreach(var unit in unitCap.unitsInSite)
        {
            var unitInfo = unit.GetComponent<Unit>();

            if(unitInfo.health < 100)
            {
                unitInfo.hunger -= hungerCost;
                unitInfo.health += HealthIncrease;
                if(unitInfo.health > 100)
                    unitInfo.health = 100;
                restTimer = restTimerMax;
            }
        }
    }
}
