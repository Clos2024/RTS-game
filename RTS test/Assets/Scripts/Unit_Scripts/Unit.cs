using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float hunger,attack,armor,health,attackRange,attackSpeed;

    public float starvationTimerMax, resourceTimerMax, metabolismMax;
    private float starvationTimer, resourceTimer, metabolismTimer, healthMax, hungerMax;
    public string unitName;
    public string performAction;

    public GameObject locationType;

    public NavMeshAgent agent;
    public Slider healthSlider,hungerSlider;
    healthBar hpBar;
    healthBar hungerBar;
    private enemyUnitsHandler enemyUnits;
    private NavMeshPath path;
    Inventory playerInventory;

    public delegate void updateAnimation();

    private GameObject target;
    public updateAnimation onUpdateAnimation;
    private float attackTimer, attackTimerMax;

    private void Awake()
    {
        path = new NavMeshPath();
        attackTimerMax = attackSpeed;
        attackTimer = attackTimerMax;
        enemyUnits = enemyUnitsHandler.Instance;
        hpBar = healthSlider.GetComponent<healthBar>();
        hungerBar = hungerSlider.GetComponent<healthBar>();

        playerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        healthMax = 100;
        hungerMax = 100;
        health = healthMax;
        hunger = hungerMax;
        attack = 5;
        armor = 0;
        starvationTimer = starvationTimerMax;//This is in seconds
        resourceTimer = resourceTimerMax;//This is in seconds
        metabolismTimer = metabolismMax;//This is in seconds
        performAction = "idle";
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Add to unitList script
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void Update()
    {
        if (target == null && enemyUnitsHandler.Instance.enemyUnits.Count != 0)
        {
            foreach (var unit in enemyUnitsHandler.Instance.enemyUnits)
            {
                if (Vector3.Distance(transform.position, unit.transform.position) <= attackRange)
                {
                    target = unit.transform.gameObject;
                }
            }
        }
        //UI Slider Updates
        hpBar.SetMaxProgress(healthMax);
        hpBar.SetProgress(health);

        hungerBar.SetMaxProgress(hungerMax);
        hungerBar.SetProgress(hunger);

        //Metabolism
        metabolismCountdown();

        //hunger at zero so we begin to starve
        if(hunger == 0) { starvationCountdown(); }

        //health at zero so we died
        if(health == 0) { Destroy(this.gameObject); }

        //Determine what location we are at
        if(locationType != null)
        {
            if (Vector3.Distance(transform.position, locationType.transform.position) < 4)
            {
                //if we are at a resource node begin to gatherResouces
                if (locationType.GetComponent<Resource>() != null) { resourceCountdown(); }
                else if (locationType.GetComponent<FarmSite>() != null) { };
            }
        }

        if(health <=0)
        {
            Destroy(gameObject);
        }

        if(target != null)
        {
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
            attackCountdown();
        }
        else
        {
            attackTimer = attackTimerMax;
            agent.updateRotation = true;
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
        if(health > 0)
            health--;
        starvationTimer = starvationTimerMax;
    }

    //Starvation rate of health decay
    void starvationCountdown()
    {
        if(starvationTimer > 0)
        {
            starvationTimer -= 1 * Time.deltaTime;
        }
        else
        {
            starvation();
        }
    }

    void gatherResource()
    {
        var ResourceNode = locationType.GetComponent<Resource>();
        ResourceNode.ExtractResource();
        resourceTimer = resourceTimerMax; 
    }
    void resourceCountdown()
    {
        if (resourceTimer > 0)
        {
            resourceTimer -= 1 * Time.deltaTime;
        }
        else
        {
            gatherResource();
        }
    }
    void metabolism()
    {
        if (hunger > 0)
        {
            hunger--;
            metabolismTimer = metabolismMax;
        }
    }
    void metabolismCountdown()
    {
        if (metabolismTimer > 0)
        {
            metabolismTimer -= 1 * Time.deltaTime;
        }
        else
        {
            metabolism();
        }
    }
    public void setDestination(Vector3 targetPositon)
    {
        //agent.SetDestination(targetPositon);
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
        health -= (dmg - armor);
    }
    private void Attack()
    {
        if (target != null)
            target.GetComponent<enemyUnit>().takeDamage(attack);
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
}
