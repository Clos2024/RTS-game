using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    private Camera myCam;
    public List<Vector3> offsets;
    public float offsetAmount;
    GameObject objClicked,prev;
    public LayerMask myLayers;
    // Start is called before the first frame update
    void Awake()
    {
        myCam = Camera.main;

        //directions used for offsets later
        Vector3 north = new Vector3(0, 0, offsetAmount);
        Vector3 west = new Vector3(offsetAmount, 0, 0);
        Vector3 south = new Vector3(0, 0, -offsetAmount);
        Vector3 east = new Vector3(-offsetAmount, 0, 0);
        Vector3 northWest = new Vector3(offsetAmount, 0, offsetAmount);
        Vector3 northEast = new Vector3(-offsetAmount, 0, offsetAmount);
        Vector3 southWest = new Vector3(offsetAmount, 0, -offsetAmount);
        Vector3 southEast = new Vector3(-offsetAmount, 0, -offsetAmount);
        //offsets.Add(Vector3.zero);
        offsets.Add(north);
        offsets.Add(west);
        offsets.Add(south);
        offsets.Add(east);
        offsets.Add(northWest);
        offsets.Add(northEast);
        offsets.Add(southWest);
        offsets.Add(southEast);

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            if(EventSystem.current.IsPointerOverGameObject() != true)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayers))
                {
                    //Get Previous click
                    if (prev == null)
                    {
                        prev = hit.transform.gameObject;
                        objClicked = hit.transform.gameObject;
                    }
                    prev = objClicked;
                    objClicked = hit.transform.gameObject;

                    //if (hit.transform.gameObject.layer == 7) //We have hit the ground
                    //{
                    //    MoveTo(hit.point, hit.transform.gameObject);
                    //}
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        foreach(var unit in UnitSelections.Instance.unitsSelected)
                        {
                            unit.GetComponent<Unit>().target = hit.transform.gameObject;
                        }
                    }
                    else
                    {
                        MoveTo(hit.point, hit.transform.gameObject);
                    }
                    //else if (hit.transform.gameObject.layer == 8) //We have hit a clickable resource spot
                    //{
                    //    if (hit.transform.tag == "Resource")
                    //    {
                    //        MoveTo(hit.point, hit.transform.gameObject);
                    //        UnitSelections.Instance.DeselectAll();
                    //    }
                    //    else if (hit.transform.tag == "Building")
                    //    {
                    //        MoveTo(hit.point, hit.transform.gameObject);
                    //        UnitSelections.Instance.DeselectAll();
                    //    }
                    //}
                }

            }
        }

    }

    void MoveTo (Vector3 target, GameObject go)
    {
        List<GameObject> units = UnitSelections.Instance.unitsSelected;
        int unitSize = units.Count;

        int j = 0;
        int i = 0;

        bool canSend = true;

        foreach (var unit in units)
        {
            //Remove this unit from the location if he is still moving there he will be re-added later.
            if (prev.GetComponent<unitCapacity>() != null)
                prev.GetComponent<unitCapacity>().unitsInSite.Remove(unit);

            NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();

            Vector3 offset = Vector3.zero;
            if (j < offsets.Count)
                offset = offsets[j];
            else
            {
                j = 0;
                i++;
                offset = Vector3.zero;
            }

            if (go != null)
                canSend = checkCapacity(go,unit);
            
            if (canSend)
            {
                unit.GetComponent<Unit>().target = null;
                unit.GetComponent<Unit>().setAction("walking");
                unit.GetComponent<Unit>().location = go;
                unit.GetComponent<Unit>().setDestination(target + (offset + new Vector3(i, i, i)));
                j++;
            }
        }
    }

    bool checkCapacity(GameObject location, GameObject unit)
    {
        unitCapacity cap = location.GetComponent<unitCapacity>();
        if (cap.unitsInSite.Count < cap.unitMaxCapacity || location.tag == "Ground")
        {
            cap.unitsInSite.Add(unit);

            return true;
        }
        return false;
    }
}
