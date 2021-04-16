using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSite : MonoBehaviour
{
    public int unitCapacity;
    public List<GameObject> unitsInSite = new List<GameObject>();
    private List<Transform> positionList = new List<Transform>();
    public Transform positonOne, positionTwo, positionThree;
    public GameObject seedOne, seedTwo, seedThree;

    private void Awake()
    {
        positionList.Add(positonOne);
        positionList.Add(positionTwo);
        positionList.Add(positionThree);

    }
    private void Update()
    {
        if(unitsInSite.Count == 1)
        {
            seedOne.GetComponent<SeedBehavior>().GrowthTimer();
        }
    }

    void AssignPosition(int position, GameObject unit)
    {
        unit.transform.GetComponent<Unit>().agent.SetDestination(positionList[position - 1].position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Unit>() != null)
        {
            if (other.transform.GetComponent<Unit>().buildingTarget == this.gameObject && unitsInSite.Count < unitCapacity)
            {
                unitsInSite.Add(other.transform.gameObject);
                AssignPosition(unitsInSite.Count, other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Unit>() != null)
        {
            unitsInSite.Remove(other.gameObject);
        }
    }
}
