using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    UnitInfo unitInfo;
    healthBar hpBar;
    healthBar hungerBar;
    Inventory playerInventory;

    public float starvationTimer, resourceTimer, metabolismTimer;
    private enemyUnitsHandler enemyUnits;
    private NavMeshPath path;
    private GameObject target;

    public string performAction;
    public GameObject locationType;
    public NavMeshAgent agent;
    public Slider healthSlider,hungerSlider;
    public updateAnimation onUpdateAnimation;
    public delegate void updateAnimation();

    private GameObject metabolismUtilTimer,starvationUtilTimer,resourceUtilTimer,attackTimer;
    private void Awake()
    {
        hpBar = healthSlider.GetComponent<healthBar>();
        hungerBar = hungerSlider.GetComponent<healthBar>();
        unitInfo = transform.GetComponent<UnitInfo>();
        path = new NavMeshPath();
        enemyUnits = enemyUnitsHandler.Instance;
        playerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
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
        metabolismUtilTimer = UtilTimer.Create(metabolism, metabolismTimer);
    }

    void Update()
    {
        if (target == null && enemyUnitsHandler.Instance.enemyUnits.Count != 0)
        {
            foreach (var unit in enemyUnitsHandler.Instance.enemyUnits)
            {
                if (Vector3.Distance(transform.position, unit.transform.position) <= unitInfo.attackRange)
                {
                    target = unit.transform.gameObject;
                }
            }
        }

        //UI Slider Updates
        hpBar.SetMaxProgress(unitInfo.HpMax);
        hpBar.SetProgress(unitInfo.Hp);

        hungerBar.SetMaxProgress(unitInfo.hungerMax);
        hungerBar.SetProgress(unitInfo.hunger);

        //Metabolism
        if(metabolismUtilTimer == null){ metabolismUtilTimer = UtilTimer.Create(metabolism, metabolismTimer); }

        //hunger at zero so we begin to starve
        if (unitInfo.hunger == 0) {
            //Starvation
            if(starvationUtilTimer == null)
                starvationUtilTimer = UtilTimer.Create(starvation, starvationTimer);
        }
        else
        {
            Destroy(starvationUtilTimer);
        }

        //health at zero so we died
        if (unitInfo.Hp == 0) { Destroy(this.gameObject); }

        //Determine what location we are at
        if(locationType != null)
        {
            if (Vector3.Distance(transform.position, locationType.transform.position) < 4)
            {
                //if we are at a resource node begin to gatherResouces
                if (locationType.GetComponent<Resource>() != null) 
                {
                    //Gather Resource
                    if (resourceUtilTimer == null)
                        resourceUtilTimer = UtilTimer.Create(gatherResource, resourceTimer);
                }
            }
            else
            {
                Destroy(resourceUtilTimer);
            }
        }

        if(unitInfo.Hp <=0)
        {
            Destroy(gameObject);
        }

        if(target != null)
        {
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
            //Attack
            if (attackTimer == null)
                attackTimer = UtilTimer.Create(Attack, unitInfo.attackSpeed);
        }
        else
        {
            agent.updateRotation = true;
            Destroy(attackTimer);
        }

        if(agent.hasPath == false && locationType == null)
        {
            setAction("idle");
        }
    }

    void OnDestroy()
    {
        //Remove to unitList script
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }

    //Make unit take starvation damage and reset starvationTimer
    void starvation()
    {
        if(unitInfo.Hp > 0)
            unitInfo.Hp--;
    }
    void gatherResource()
    {
        var ResourceNode = locationType.GetComponent<Resource>();
        ResourceNode.ExtractResource();
    }
    void metabolism()
    {
        if (unitInfo.hunger > 0)
            unitInfo.hunger--;
    }
    public void setDestination(Vector3 targetPositon)
    {
        agent.CalculatePath(targetPositon, path);
        agent.SetPath(path);
    }

    public void setAction(string action)
    {
        performAction = action;
        if(onUpdateAnimation != null)
            onUpdateAnimation.Invoke();
    }

    public void takeDamage(float dmg)
    {
        Debug.Log("took damage");
        unitInfo.Hp -= (dmg - unitInfo.armor);
    }
    private void Attack()
    {
        if (target != null)
            target.GetComponent<enemyUnit>().takeDamage(unitInfo.attackDmg);
    }
}
