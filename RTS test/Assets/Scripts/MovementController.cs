using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private Camera myCam;
    public Vector3[] positions;

    // Start is called before the first frame update
    void Awake()
    {
        myCam = Camera.main;
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
                    SetDestination(hit.point);
                }
                else if (hit.transform.gameObject.layer == 8 && hit.transform.tag == "Resource") //We have hit a clickable resource spot
                {
                    Vector3 target = hit.transform.position;
                    SendResourceTarget(hit.transform.gameObject);
                    SetDestination(target);
                }
            }
        }

    }

    void SetDestination(Vector3 target)
    {
        List<GameObject> units = UnitSelections.Instance.unitsSelected;
        int unitSize = units.Count;

        foreach (var unit in units)
        {
            unit.GetComponent<Unit>().walking = true;
            NavMeshAgent unitAgent = unit.GetComponent<NavMeshAgent>();
            unitAgent.SetDestination(target);
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
