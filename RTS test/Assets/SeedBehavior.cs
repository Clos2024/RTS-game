using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBehavior : MonoBehaviour
{
    public float cropGrowthTimerMax;
    private float cropGrowthTimer;

    public GameObject wheat;
    private GameObject crop;

    void Awake()
    {
        cropGrowthTimer = cropGrowthTimerMax;
    }
    void Sprout()
    {
        crop = Instantiate(wheat,transform);
        cropGrowthTimer = cropGrowthTimerMax;
    }
    public void GrowthTimer()
    {
        if (crop == null)
        {
            if (cropGrowthTimer > 0)
            {
                cropGrowthTimer -= 1 * Time.deltaTime;
            }
            else
            {
                Sprout();
            }
        }
    }
}
