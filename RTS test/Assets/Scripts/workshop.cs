using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workshop : MonoBehaviour
{
    unitCapacity unitCap;
    public GameObject buttonCanvas;
    [SerializeField]
    private List<GameObject> unitsHere;
    // Start is called before the first frame update
    void Awake()
    {
        unitCap = transform.GetComponent<unitCapacity>();
    }

    void Update()
    {
        if(unitsHere.Count == unitCap.unitMaxCapacity)
        {
            buttonCanvas.SetActive(true);
        }
        else
        {
            buttonCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            unitsHere.Add(other.transform.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            unitsHere.Remove(other.transform.gameObject);
        }
    }
}
