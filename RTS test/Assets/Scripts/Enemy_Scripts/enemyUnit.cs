using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyUnit : MonoBehaviour
{
    private Vector3 homePosition,roamPosition;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private NavMeshPath path;
    private UnitSelections playerUnits;
    [SerializeField]
    private GameObject target;
    private float attackTimer,attackTimerMax;
    Animator unitAnimator;
    healthBar hpBar;
    public Slider healthSlider;

    public GameObject[] listOfTwo = new GameObject[2];

    public float hp,HpMax;
    public float armor;
    public float attackDamage;
    public int attackSpeed;
    public float enemyDetectionRadius;
    public float attackRange;
    public float speed;
    private State state;
    public bool debug;

    private enum State
    {
        Wandering,
        Chase,
        Attack,
    }

    void Awake()
    {
        unitAnimator = GetComponent<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
        state = State.Wandering;
        playerUnits = UnitSelections.Instance;
        attackTimerMax = attackSpeed;
        attackTimer = attackTimerMax;
        hpBar = healthSlider.GetComponent<healthBar>();

        path = new NavMeshPath();
        agent.speed = speed;
    }

    private void Start()
    {
        enemyUnitsHandler.Instance.enemyUnits.Add(this.gameObject);
        homePosition = transform.position;
        roamPosition = GetRoamingPostion();
    }

    private void Update()
    {
        hpBar.SetMaxProgress(HpMax);
        hpBar.SetProgress(hp);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (target == null)
        {
            foreach (var unit in UnitSelections.Instance.unitList)
            {
                if (Vector3.Distance(transform.position, unit.transform.position) <= enemyDetectionRadius)
                {
                    if (listOfTwo[0] == null)
                    {
                        listOfTwo[0] = unit;
                    }
                    else
                    {
                        if (Vector3.Distance(listOfTwo[0].transform.position, transform.position) > Vector3.Distance(unit.transform.position, transform.position))
                        {
                            listOfTwo[1] = listOfTwo[0];
                            listOfTwo[0] = unit;
                        }
                    }
                }
            }
            if (listOfTwo[0] != null)
            {
                target = listOfTwo[0];
                listOfTwo[0].GetComponent<Unit>().enemyTargetingMe.Add(gameObject);
            }
        }
        else
        {
            if(target.GetComponent<Unit>().enemyTargetingMe.Count > 1 && listOfTwo[1] != null)
            {
                target = listOfTwo[1];
                listOfTwo[1].GetComponent<Unit>().enemyTargetingMe.Remove(gameObject);
                listOfTwo[0].GetComponent<Unit>().enemyTargetingMe.Remove(gameObject);
            }
        }

        if (debug)
        {
            Debug.Log(state);
            Debug.Log(path.status);
        }

        switch(state)
        {
            default:
            case State.Wandering:
                {
                    unitAnimator.SetBool("Walking", true);
                    Wander();
                    if (target != null)
                    {
                        state = State.Chase;
                    }
                }
                break;
            case State.Chase:
                {
                    Chase();

                    if (Vector3.Distance(transform.position, target.transform.position) >= enemyDetectionRadius)
                    {
                        target = null;
                        listOfTwo[0] = null;
                        listOfTwo[1] = null;
                    }

                    if (target == null)
                    {
                        state = State.Wandering;
                    }
                    else if(Vector3.Distance(transform.position,target.transform.position) <= attackRange)
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
                        attackTimer = attackTimerMax;
                        agent.updateRotation = true;
                        agent.isStopped = false;
                        state = State.Wandering;
                    }
                    else
                    {
                        Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
                        agent.isStopped = true;

                        if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
                        {
                            state = State.Chase;
                            agent.isStopped = false;
                        }
                    }
                }
                break;
        }
    }

    private void Wander()
    {
        if(agent.remainingDistance <=1f)
        {
            roamPosition = GetRoamingPostion();
            agent.CalculatePath(roamPosition, path);
            agent.SetPath(path);
        }
        else if(path.status == NavMeshPathStatus.PathInvalid || path.status == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Invalid path recalculating");
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
        if (target != null)
        {
            target.GetComponent<UnitInfo>().takeDamage(attackDamage, gameObject);
            unitAnimator.SetBool("Attack", true);
        }
        attackTimer = attackTimerMax;
    }
    void attackCountdown()
    {
        unitAnimator.SetBool("Attack", false);
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

    public void takeDamage(float dmg)
    {
        hp -= (dmg - armor);
    }
    void OnDestroy()
    {
        enemyUnitsHandler.Instance.enemyUnits.Remove(this.gameObject);
    }

#if UNITY_EDITOR

    public void OnDrawGizmosSelected()
    {
        Color c = new Color(0, 0, 0.7f, 0.1f);
        UnityEditor.Handles.color = c;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, Vector3.forward, 360, enemyDetectionRadius);
    }

#endif
}
