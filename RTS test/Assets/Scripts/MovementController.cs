using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    private Camera myCam;


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
                    Vector3 target = hit.transform.position - new Vector3(1, 1, 1);
                    SendResourceTarget(hit.transform.gameObject);
                    SetDestination(target);
                }
            }
        }

    }

    void SetDestination(Vector3 target)
    {
        foreach (var unit in UnitSelections.Instance.unitsSelected)
        {
            unit.GetComponent<Unit>().walking = true;
            NavMeshAgent unitAgent = unit.GetComponent<NavMeshAgent>();
            unitAgent.SetDestination(target);
        }
    }
    void SendResourceTarget(GameObject target)
    {
        foreach (var unit in UnitSelections.Instance.unitsSelected)
        {
            NavMeshAgent unitAgent = unit.GetComponent<NavMeshAgent>();
            unitAgent.GetComponent<Unit>().SetResourceTarget(target);
        }
    }
}
