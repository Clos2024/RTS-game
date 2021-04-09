using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public float hunger,attack,armor,health;

    public float starvationTimerMax, resourceTimerMax;
    private float starvationTimer, resourceTimer;
    public bool gathering, walking;
    public string unitName;

    GameObject resourceTarget;
    NavMeshAgent agent;

    Inventroy playerInventory;

    private void Awake()
    {
        playerInventory = GameObject.Find("Inventory").GetComponent<Inventroy>();
        health = 100;
        hunger = 100;
        attack = 5;
        armor = 0;
        starvationTimer = starvationTimerMax;//This is in seconds
        resourceTimer = resourceTimerMax;//This is in seconds
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
        if (Mathf.Abs(Vector3.Distance(agent.destination, transform.position)) < 1)
        {
            walking = false;
        }
        //Deplete health if hunger is 0;
        if(hunger == 0)
        {
            //Begin starving countdown
            starvationCountdown();
        }

        //We are out our resourceTarget and must begin gathering
        if (resourceTarget != null && Mathf.Abs(Vector3.Distance(resourceTarget.transform.position, transform.position)) < 1.45f)
        {
            gathering = true;
            transform.LookAt(resourceTarget.transform);
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

    public void SetResourceTarget(GameObject target)
    {
        resourceTarget = target;
        Debug.Log("Target Locked");
    }

    void gatherResource()
    {
        if(resourceTarget.GetComponent<woodResource>().resourceType == "wood" && resourceTarget.GetComponent<woodResource>().WoodResource > 0)
        {
            playerInventory.AddWood(resourceTarget.GetComponent<woodResource>().ExtractResource());
            resourceTimer = resourceTimerMax;
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
}
