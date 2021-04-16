using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float hunger,attack,armor,health;

    public float starvationTimerMax, resourceTimerMax, metabolismMax;
    private float starvationTimer, resourceTimer, metabolismTimer, healthMax, hungerMax;
    public bool gathering, walking;
    public string unitName;

    public GameObject resourceTarget;
    public GameObject buildingTarget;
    public NavMeshAgent agent;
    public Slider healthSlider,hungerSlider;
    healthBar hpBar;
    healthBar hungerBar;

    Inventroy playerInventory;

    private void Awake()
    {
        hpBar = healthSlider.GetComponent<healthBar>();
        hungerBar = hungerSlider.GetComponent<healthBar>();

        playerInventory = GameObject.Find("Inventory").GetComponent<Inventroy>();
        healthMax = 100;
        hungerMax = 100;
        health = healthMax;
        hunger = hungerMax;
        attack = 5;
        armor = 0;
        starvationTimer = starvationTimerMax;//This is in seconds
        resourceTimer = resourceTimerMax;//This is in seconds
        metabolismTimer = metabolismMax;//This is in seconds
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
        hpBar.SetMaxProgress(healthMax);
        hpBar.SetProgress(health);

        hungerBar.SetMaxProgress(hungerMax);
        hungerBar.SetProgress(hunger);
        //Check if we have are pathing to a destination
        if (Mathf.Abs(Vector3.Distance(agent.destination, transform.position)) < 1)
        {
            walking = false;
        }
        else
            walking = true;

        //Metabolism
        metabolismCountdown();

        //hunger at zero so we begin to starve
        if(hunger == 0) { starvationCountdown(); }

        //health at zero so we died
        if(health == 0) { Destroy(this.gameObject); }

        //We are out our resourceTarget and must begin gathering
        if (resourceTarget != null && Mathf.Abs(Vector3.Distance(resourceTarget.transform.position, transform.position)) <= 1.6f)
        {
            gathering = true;
            resourceCountdown();
        }
        else
            gathering = false;
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
        if(resourceTarget.GetComponent<Resource>().resourceType == "wood" && resourceTarget.GetComponent<Resource>().WoodResource > 0)
        {
            playerInventory.AddWood(resourceTarget.GetComponent<Resource>().ExtractResource());
            resourceTimer = resourceTimerMax;
            hunger -= 5;
        }
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
    public void SetResourceTarget(GameObject target)
    {
        resourceTarget = target;
    }
    public void SetBuildingTarget(GameObject target)
    {
        buildingTarget = target;
    }
}
