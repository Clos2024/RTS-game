using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToGround : MonoBehaviour
{
    public LayerMask layerMask;

    // Update is called once per frame
    public void Awake()
    {
       // RaycastHit hitInfo;
        var hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 10f);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.layer != layerMask)
            {
                if (hit.collider.gameObject == transform.gameObject)
                    continue;
                transform.position = hit.point + new Vector3(0, 2.55f, 0);
                break;
            }
        }
    }
}
