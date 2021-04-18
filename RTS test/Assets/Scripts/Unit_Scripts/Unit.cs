using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float hunger,attack,armor,health;

    public float starvationTimerMax, resourceTimerMax, metabolismMax;
    private float starvationTimer, resourceTimer, metabolismTimer, healthMax, hungerMax;
    public string unitName;

    public GameObject locationType;

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
            //if we are at a resource node begin to gatherResouces
            if (locationType.GetComponent<Resource>() != null) { resourceCountdown(); }
            else if(locationType.GetComponent<FarmSite>() != null) { };
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

        if (ResourceNode.resourceType == "wood")
        {
            if (ResourceNode.WoodResource > 0)
            {
                playerInventory.AddWood(locationType.GetComponent<Resource>().ExtractResource());
                resourceTimer = resourceTimerMax;
                hunger -= 5;
            }
        }
        else if(ResourceNode.resourceType == "stone")
        {
            if (ResourceNode.StoneResource > 0)
            {
                playerInventory.AddStone(locationType.GetComponent<Resource>().ExtractResource());
                resourceTimer = resourceTimerMax;
                hunger -= 5;
            }
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
    public void setDestination(Vector3 targetPositon)
    {
        agent.SetDestination(targetPositon);
    }
}
