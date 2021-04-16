using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFarm : MonoBehaviour
{
    public GameObject farm;

    public void build_farm()
    {
        Instantiate(farm);
    }
}
