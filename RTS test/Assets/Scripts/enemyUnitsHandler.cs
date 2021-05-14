using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyUnitsHandler : MonoBehaviour
{
    public List<GameObject> enemyUnits = new List<GameObject>();

    private static enemyUnitsHandler InstanceEnemy;

    public GameObject enemyPrefab;

    public static enemyUnitsHandler Instance { get { return InstanceEnemy; } }

    void Awake()
    {
        if (InstanceEnemy != null && InstanceEnemy != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            InstanceEnemy = this;
        }
    }
    void Update()
    {
        if(enemyUnits.Count < 6)
        {
            var newEnemy = Instantiate(enemyPrefab,transform);
        }
    }
}
