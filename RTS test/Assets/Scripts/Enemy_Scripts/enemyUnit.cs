using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyUnit : MonoBehaviour
{
    private Vector3 homePosition,roamPosition;
    private NavMeshAgent agent;
    [SerializeField]
    private NavMeshPath path;
    private UnitSelections playerUnits;
    [SerializeField]
    private GameObject target;
    private float attackTimer,attackTimerMax;

    public float hp;
    public float armor;
    public float attackDamage;
    public int attackSpeed;
    public float enemyDetectionRadius;
    public float attackRange;
    public float speed;
    private State state;

    private enum State
    {
        Wandering,
        Chase,
        Attack,
    }

    void Awake()
    {
        state = State.Wandering;
        playerUnits = UnitSelections.Instance;
        attackTimerMax = attackSpeed;
        attackTimer = attackTimerMax;
    }

    private void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        homePosition = transform.position;
        roamPosition = GetRoamingPostion();
        path = new NavMeshPath();
        agent.CalculatePath(new Vector3(100,100,100),path);
        agent.speed = speed;
    }

    private void Update()
    {
        if (target == null && playerUnits.unitList.Count != 0)
        {
            foreach (var unit in playerUnits.unitList)
            {
                if (Vector3.Distance(transform.position, unit.transform.position) < enemyDetectionRadius)
                {
                    target = unit.transform.gameObject;
                }
            }
        }

        Debug.Log(state);
        switch(state)
        {
            default:
            case State.Wandering:
                {
                    Wander();
                    if(target != null)
                    {
                        state = State.Chase;
                    }
                }
                break;
            case State.Chase:
                {
                    Chase();
                    if(target == null)
                    {
                        state = State.Wandering;
                    }

                    if(Vector3.Distance(transform.position,target.transform.position) < attackRange)
                    {
                        state = State.Attack;
                    }
                }
                break;
            case State.Attack:
                {
                    attackCountdown();

                    if (target == null)
                    {
                        state = State.Wandering;
                        roamPosition = GetRoamingPostion();
                        agent.CalculatePath(roamPosition, path);
                        agent.SetPath(path);
                    }
                }
                break;
        }
    }

    private void Wander()
    {
        if (path.status == NavMeshPathStatus.PathInvalid || path.status == NavMeshPathStatus.PathPartial || Vector3.Distance(transform.position, roamPosition) <= 1f)
        {
            Debug.Log(" Wander called");
            roamPosition = GetRoamingPostion();
            agent.CalculatePath(roamPosition, path);
            agent.SetPath(path);
        }
    }
    private void Chase()
    {
        agent.CalculatePath(target.transform.position, path);
        agent.SetPath(path);
    }
    private void Attack()
    {
        Debug.Log("ATTACK");
        if (target != null)
            target.GetComponent<Unit>().takeDamage(attackDamage);
        attackTimer = attackTimerMax;
    }
    void attackCountdown()
    {
        if (attackTimer > 0)
        {
            attackTimer -= 1 * Time.deltaTime;
        }
        else
        {
            Attack();
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
