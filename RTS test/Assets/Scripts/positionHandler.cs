using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionHandler : MonoBehaviour
{
    public List<GameObject> sitePosition = new List<GameObject>();
    unitCapacity unitCap;
    public string action;

    void Awake()
    {
        unitCap = transform.GetComponent<unitCapacity>();
    }

    public int GetPositionCount()
    {
        return sitePosition.Count;
    }

    public void AssignPos(GameObject unit)
    {
        if (unitCap.unitsInSite.Contains(unit) == true)
        {
            foreach (var pos in sitePosition)
            {
                if (pos.GetComponent<unitInSite>().unitInPos == null)
                {
                    unit.GetComponent<Unit>().setDestination(pos.transform.position);
                    unit.GetComponent<Unit>().setAction(action);
                    pos.GetComponent<unitInSite>().unitInPos = unit;
                    return;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Unit>() != null)
        {
            AssignPos(other.transform.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            other.GetComponent<Unit>().setAction("idle");
            foreach (var pos in sitePosition)
            {
                if (pos.GetComponent<unitInSite>().unitInPos == other.transform.gameObject)
                {
                    pos.GetComponent<unitInSite>().unitInPos = null;
                    return;
                }
            }
        }
    }
}
