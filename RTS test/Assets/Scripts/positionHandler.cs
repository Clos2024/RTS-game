using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionHandler : MonoBehaviour
{
    public List<Transform> positions = new List<Transform>();

    public int GetPositionCount()
    {
        return positions.Count;
    }
}
