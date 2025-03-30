using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ActivationArea : MonoBehaviour
{
    public float activationSpeed;
    public GameObject staff;
    public RitualTable table;

    bool staffInArea;

    public bool staffHeld;
    public bool StaffinRightHand;


    private void Update()
    {
        CheckActivation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Staff")
        {
            staffInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Staff")
        {
            staffInArea = false;
        }
    }

    void CheckActivation()
    {
        if (staffInArea && staffHeld)
        {
            Vector3 controllerVelocity;

            if (StaffinRightHand)
            {
                HandManager.instance.rightHand.TryGetFeatureValue(CommonUsages.deviceVelocity, out controllerVelocity);
            }
            else
            {
                HandManager.instance.leftHand.TryGetFeatureValue(CommonUsages.deviceVelocity, out controllerVelocity);
            }

            print(controllerVelocity.magnitude);

            if (controllerVelocity.magnitude > activationSpeed)
            {
                table.ActivateRitual();
            }
        }
    }

}
