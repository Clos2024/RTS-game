using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitCapacity : MonoBehaviour
{
    private positionHandler PositionHandler;
    public int unitMaxCapacity;
    public List<GameObject> unitsInSite = new List<GameObject>();

    void Awake()
    {
        PositionHandler = transform.GetComponent<positionHandler>();
        unitMaxCapacity = PositionHandler.GetPositionCount();
    }
}
