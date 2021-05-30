using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    UnitInfo unitInfo;
    healthBar hpBar;
    healthBar hungerBar;

    public float starvationTimer, resourceTimer, metabolismTimer;
    private enemyUnitsHandler enemyUnits;
    private NavMeshPath path;
    public bool gather;
    public GameObject target;
    public List<GameObject> enemyTargetingMe = new List<GameObject>();

    public string performAction;
    public GameObject location;
    public NavMeshAgent agent;
    public Slider healthSlider,hungerSlider;

    public updateAnimation onUpdateAnimation;
    public delegate void updateAnimation();

    private GameObject metabolismUtilTimer,starvationUtilTimer,resourceUtilTimer,attackTimer;

    public bool debug;

    private void Awake()
    {
        hpBar = healthSlider.GetComponent<healthBar>();
        hungerBar = hungerSlider.GetComponent<healthBar>();
        unitInfo = transform.GetComponent<UnitInfo>();
        path = new NavMeshPath();
        enemyUnits = enemyUnitsHandler.Instance;
        performAction = "idle";
        unitInfo.Hp = unitInfo.HpMax;
        unitInfo.hunger = unitInfo.hungerMax;
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Add to unitList script
        UnitSelections.Instance.unitList.Add(this.gameObject);
        metabolismUtilTimer = UtilTimer.Create(unitInfo.metabolism, metabolismTimer);
    }

    void Update()
    {
        // Debug Code ////////////////////////////////////////////////
        if(debug)
        {
            Debug.Log(location);
        }
        //////////////////////////////////////////////////////////////
        //UI Slider Updates
        hpBar.SetMaxProgress(unitInfo.HpMax);
        hpBar.SetProgress(unitInfo.Hp);

        hungerBar.SetMaxProgress(unitInfo.hungerMax);
        hungerBar.SetProgress(unitInfo.hunger);

        //Death
        if (unitInfo.Hp <= 0)
        {
            Destroy(gameObject);
        }
        


        //Find target if we dont Have one
        if (target == null)
        {
            //Metabolism
            if (metabolismUtilTimer == null) { metabolismUtilTimer = UtilTimer.Create(unitInfo.metabolism, metabolismTimer); }

            //hunger at zero so we begin to starve
            if (unitInfo.hunger == 0)
            {
                //Starvation
                if (starvationUtilTimer == null)
                    starvationUtilTimer = UtilTimer.Create(unitInfo.starvation, starvationTimer);
            }
            else
            {
                Destroy(starvationUtilTimer);
            }

            //health at zero so we died
            if (unitInfo.Hp == 0) { Destroy(this.gameObject); }

            if(gather)
            {
                if (location.GetComponent<Resource>() != null)
                {
                    Quaternion rotation = Quaternion.LookRotation(location.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
                }
                setAction("gathering");
                if (resourceUtilTimer == null)
                    resourceUtilTimer = UtilTimer.Create(gatherResource, resourceTimer);
            }
            else
            {
                Destroy(resourceUtilTimer);
            }

            if(agent.remainingDistance < 1f && gather == false)
            {
                agent.isStopped = true;
                setAction("idle");
            }

            if (agent.velocity.magnitude > 1)
            {
                setAction("walking");
                Destroy(attackTimer);
            }
        }

        if (target != null)
        {
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);

            if (Vector3.Distance(transform.position, target.transform.position) > unitInfo.attackRange)
            {
                setDestination(target.transform.position);
                agent.isStopped = false;
            }

            if (Vector3.Distance(transform.position, target.transform.position) <= unitInfo.attackRange)
            {
                agent.isStopped = true;
                //Attack
                if (attackTimer == null)
                {
                    attackTimer = UtilTimer.Create(Attack, unitInfo.attackSpeed);
                }
            }
        }
        else
        {
            agent.updateRotation = true;
            agent.isStopped = false;
            Destroy(attackTimer);
        }
    }

    void OnDestroy()
    {
        //Remove to unitList script
        Destroy(transform.GetComponent<unitAnimHandler>());
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }
    void gatherResource()
    {
        if(location.GetComponent<Resource>() != null)
        {
            var ResourceNode = location.GetComponent<Resource>();
            ResourceNode.ExtractResource();
        }
    }
    public void setDestination(Vector3 targetPositon)
    {
        
        agent.CalculatePath(targetPositon, path);
        agent.isStopped = false;
        agent.SetPath(path);
    }

    public void setAction(string action)
    {
        performAction = action;
        if(onUpdateAnimation != null)
            onUpdateAnimation.Invoke();
    }
    private void Attack()
    {
        setAction("attack");
        if (target != null)
        {
            target.GetComponent<enemyUnit>().takeDamage(unitInfo.attackDmg);
        }
    }


}
