using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyUnit : MonoBehaviour
{
    private Vector3 homePosition,roamPosition;
    private NavMeshAgent agent;
    private NavMeshPath path;
    private void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        homePosition = transform.position;
        roamPosition = GetRoamingPostion();
        path = new NavMeshPath();
        agent.CalculatePath(new Vector3(100,100,100),path);
    }

    private void Update()
    {
        ///OMFG THIS WORKS IN RESETTING THE PATH SAVE THIS WORK
        if(path.status == NavMeshPathStatus.PathInvalid || path.status == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("reset path");
            roamPosition = GetRoamingPostion();
            agent.CalculatePath(roamPosition, path);
            agent.SetPath(path);
        }

        if(Vector3.Distance(transform.position,roamPosition) < 1f)
        {
            Debug.Log("Arrived at path time for new path");
            roamPosition = GetRoamingPostion();
            agent.CalculatePath(roamPosition, path);
            agent.SetPath(path);
        }

    }
    private Vector3 GetRoamingPostion()
    {
        return homePosition + GetRandomDir() * Random.Range(10f, 50f);
    }

    private Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f),0,Random.Range(-1, 1f)).normalized;
    }
}
