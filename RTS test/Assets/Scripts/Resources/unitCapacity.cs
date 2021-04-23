using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitCapacity : MonoBehaviour
{
    private positionHandler PositionHandler;
    public int unitMaxCapacity;
    public List<GameObject> unitsInSite = new List<GameObject>();
    Unit unit;

    void Awake()
    {
        unit = transform.GetComponent<Unit>();
        PositionHandler = transform.GetComponent<positionHandler>();
        unitMaxCapacity = PositionHandler.GetPositionCount();
    }
}
