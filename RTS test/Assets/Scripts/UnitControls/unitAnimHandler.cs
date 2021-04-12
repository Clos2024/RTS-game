using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitAnimHandler : MonoBehaviour
{
    Animator unitAnimator;
    Unit unitInfo;

    // Start is called before the first frame update
    void Start()
    {
        unitAnimator = GetComponent<Animator>();
        unitInfo = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        unitAnimator.SetBool("walking", unitInfo.walking);
        unitAnimator.SetBool("gathering", unitInfo.gathering);
    }
}
