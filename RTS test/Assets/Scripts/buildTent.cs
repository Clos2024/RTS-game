using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildTent : MonoBehaviour
{
    public GameObject tent;

    public void build_tent()
    {
        Instantiate(tent);
    }
}
