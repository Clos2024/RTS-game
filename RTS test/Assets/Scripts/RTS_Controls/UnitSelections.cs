using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private GameObject UIInfoCanvas;

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    public delegate void OnUnitChange();
    public OnUnitChange onUnitChangedCallback;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        unitToAdd.transform.Find("UnitInfoCanvas").gameObject.SetActive(true);

        if (onUnitChangedCallback != null)
            onUnitChangedCallback.Invoke();
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if(!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.transform.Find("UnitInfoCanvas").gameObject.SetActive(true);
        }
        else
        {
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
            unitToAdd.transform.Find("UnitInfoCanvas").gameObject.SetActive(false);
            unitsSelected.Remove(unitToAdd);
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if(!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.transform.Find("UnitInfoCanvas").gameObject.SetActive(true);
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            unit.transform.Find("UnitInfoCanvas").gameObject.SetActive(false);
            unit.transform.GetChild(0).gameObject.SetActive(false);
        }
        unitsSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect)
    {
        unitsSelected.Remove(unitToDeselect);
        unitToDeselect.transform.GetChild(0).gameObject.SetActive(false);
        unitToDeselect.transform.Find("UnitInfoCanvas").gameObject.SetActive(false);
    }
}
