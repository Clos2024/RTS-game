using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventroy : MonoBehaviour
{
    [SerializeField]
    private int wood, stone, metal;

    public void AddWood(int amount)
    {
        wood += amount;
    }
}
