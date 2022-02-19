using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public GameObject myDestructable;
    public Animator myAnimator;
    public string myTrigger;

    public Animator exitAnimator;
    public string exitTrigger;

    // Start is called before the first frame update
    void Update()
    {
        if (myDestructable == null)
        {
            myAnimator.SetTrigger(myTrigger);
        }
        
    }

    void ActivateExit()
    {
        exitAnimator.SetTrigger(exitTrigger);
    }

}
