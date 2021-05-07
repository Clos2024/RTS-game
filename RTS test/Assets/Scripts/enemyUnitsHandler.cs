using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyUnitsHandler : MonoBehaviour
{
    public List<GameObject> enemyUnits = new List<GameObject>();

    private static enemyUnitsHandler _instance;
    public static enemyUnitsHandler Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
