using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private Camera myCam;
    public List<Vector3> offsets;
    // Start is called before the first frame update
    void Awake()
    {
        myCam = Camera.main;

        //directions used for offsets later
        Vector3 north = new Vector3(0, 0, 1.5f);
        Vector3 west = new Vector3(1.5f, 0, 0);
        Vector3 south = new Vector3(0, 0, -1.5f);
        Vector3 east = new Vector3(-1.5f, 0, 0);
        Vector3 northWest = new Vector3(1.5f, 0, 1.5f);
        Vector3 northEast = new Vector3(-1.5f, 0, 1.5f);
        Vector3 southWest = new Vector3(1.5f, 0, -1.5f);
        Vector3 southEast = new Vector3(-1.5f, 0, -1.5f);
        //offsets.Add(Vector3.zero);
        offsets.Add(north);
        offsets.Add(west);
        offsets.Add(south);
        offsets.Add(east);
        offsets.Add(northWest);
        offsets.Add(northEast);
        offsets.Add(southWest);
        offsets.Add(southEast);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == 7) //We have hit the ground
                {
                    MoveTo(hit.point);
                }
                else if (hit.transform.gameObject.layer == 8 && hit.transform.tag == "Resource") //We have hit a clickable resource spot
                {
                    //We hit a resource block and now we send the units to it
                    GameObject hitObj = hit.transform.gameObject;
                    Vector3 target = hit.transform.position;
                    SendResourceTarget(hitObj);
                    MoveToResource(target,hitObj.GetComponent<Resource>().unitCapacity);
                }
            }
        }

    }

    void MoveTo (Vector3 target)
    {
        List<GameObject> units = UnitSelections.Instance.unitsSelected;
        int unitSize = units.Count;

        int j = 0;
        int i = 0;

        foreach (var unit in units)
        {
            NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();

            Vector3 offset = Vector3.zero;
            if (j < offsets.Count)
                offset = offsets[j];
            else
            {
                j = 0;
                i++;
                offset = Vector3.zero;
            }

            agent.SetDestination(target + (offset + new Vector3(i,i,i)));
            j++;
        }
    }
    void MoveToResource(Vector3 target, int capacity)
    {
        List<GameObject> units = UnitSelections.Instance.unitsSelected;
        int unitSize = units.Count;

        int j = 0;
        int i = 0;

        for (int z = 0; z < capacity; z++)
        {
            if (z >= unitSize)
                return;

            NavMeshAgent agent = units[z].GetComponent<NavMeshAgent>();
            
            Vector3 offset = Vector3.zero;
            if (j < offsets.Count)
                offset = offsets[j];
            else
            {
                j = 0;
                i++;
                offset = Vector3.zero;
            }

            agent.SetDestination(target + (offset + new Vector3(i, i, i)));
            j++;
        }
    }

    void SendResourceTarget(GameObject target)
    {
        List<GameObject> units = UnitSelections.Instance.unitsSelected;
        int unitSize = units.Count;

        foreach (var unit in units)
        {
            NavMeshAgent unitAgent = unit.GetComponent<NavMeshAgent>();
            unitAgent.GetComponent<Unit>().SetResourceTarget(target);
        }
    }
}
