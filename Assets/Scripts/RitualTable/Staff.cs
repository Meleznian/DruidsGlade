using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Staff : MonoBehaviour
{
    public ActivationArea activationArea;
    
    public void PickedUp(SelectEnterEventArgs arg)
    {
        activationArea.staffHeld = true;

        if (arg.interactorObject == HandManager.instance.rightInteractor)
        {
            activationArea.StaffinRightHand = true;
        }
        else
        {
            activationArea.StaffinRightHand = false;
        }

    }

    public void Dropped()
    {
        activationArea.staffHeld = true;
    }
}
