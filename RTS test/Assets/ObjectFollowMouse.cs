using UnityEngine;

public class ObjectFollowMouse : MonoBehaviour
{
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.position = hit.point;
        }
    }
}
