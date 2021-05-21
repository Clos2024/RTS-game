using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    UnitSelections unitsAlive;
    // Start is called before the first frame update
    void Start()
    {
        unitsAlive = UnitSelections.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(unitsAlive.unitList.Count <= 0)
        {
            Debug.Log("Entered");
            //Lose Screen
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
