using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipProgress : MonoBehaviour
{
    public GameObject shipOrigin;
    public GameObject smokeParticle;
    public GameObject shipProgressState1, shipProgressState2, shipProgressState3;
    public static shipProgress instance;

    private GameObject currentState;

    private int progress;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            progress++;
        }
        //Debug.Log(progress);
        if(progress == 1)
        {
            currentState = Instantiate(shipProgressState1, shipOrigin.transform);
            progress++;
        }
        else if(progress == 3)
        {
            Destroy(currentState);
            currentState = Instantiate(shipProgressState2, shipOrigin.transform);
            progress++;
        }
        else if(progress == 5)
        {
            Destroy(currentState);
            currentState = Instantiate(shipProgressState3, shipOrigin.transform);
            progress++;
        }
    }

    public void increaseProgress()
    {
        progress++;
    }
}
