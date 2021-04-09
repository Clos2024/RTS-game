using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    Inventroy inventory;
    public Text woodText, stoneText, metalText,soldierName;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventroy>();
    }

    void Update()
    {
        woodText.text = inventory.GetWood().ToString();
        stoneText.text = inventory.GetStone().ToString();
        metalText.text = inventory.GetMetal().ToString();
        if (UnitSelections.Instance.unitsSelected.Count == 1)
        {
            string name = UnitSelections.Instance.unitsSelected[0].GetComponent<Unit>().unitName;
            string hunger = UnitSelections.Instance.unitsSelected[0].GetComponent<Unit>().hunger.ToString();
            soldierName.text = name + " " + hunger + "%";
        }
        else if(UnitSelections.Instance.unitsSelected.Count > 1)
        {
            soldierName.text = "Units count : " + UnitSelections.Instance.unitsSelected.Count.ToString();
        }
        else
            soldierName.text = "";
    }
}
