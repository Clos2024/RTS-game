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
        unitInfo.onUpdateAnimation += setAnimationBool;
    }

    void setAnimationBool()
    {
        var action = unitInfo.performAction;
        ResetAnimator();
        if(action != "idle")
            unitAnimator.SetBool(action, true);
    }

    void ResetAnimator()
    {
        unitAnimator.SetBool("walking", false);
        unitAnimator.SetBool("gathering", false);
        unitAnimator.SetBool("farming", false);
        unitAnimator.SetBool("resting", false);
    }

}
