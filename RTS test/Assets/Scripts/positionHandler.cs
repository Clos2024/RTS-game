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
            if (transform.tag == "Resource")
                other.GetComponent<Unit>().location = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            other.GetComponent<Unit>().setAction("idle");
            other.GetComponent<Unit>().location = null;
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
    private void OnDestroy()
    {
        foreach(var pos in sitePosition)
        {
            if(pos.GetComponent<unitInSite>().unitInPos != null)
            {
                pos.GetComponent<unitInSite>().unitInPos.GetComponent<Unit>().setAction("idle");
                pos.GetComponent<unitInSite>().unitInPos.GetComponent<Unit>().location = null;
            }
        }
    }
}
