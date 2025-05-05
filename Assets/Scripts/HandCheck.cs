using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandCheck : MonoBehaviour
{
    internal bool held;
    public InputDevice heldBy;

    // Start is called before the first frame update
    public void PickedUp(SelectEnterEventArgs arg)
    {
        held = true;    
        if (arg.interactorObject == HandManager.instance.rightInteractor)
        {
            heldBy = HandManager.instance.rightHand;
        }
        else
        {
            heldBy = HandManager.instance.leftHand;
        }

    }

    public void Dropped()
    {
        held = false;
    }
}
