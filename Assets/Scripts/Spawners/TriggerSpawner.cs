using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.InputSystem;

public class TriggerSpawner : SpawnerBase
{

    public bool fire;

    bool leftHandin;
    bool rightHandin;

    public InputActionReference RightGrip;
    public InputActionReference LeftGrip;

    // Update is called once per frame

    private void Update()
    {
        SpawnIngredient();
    }


    private void OnTriggerEnter(Collider other)
    {
        print("Object Entered");

        if (other.CompareTag("Left Controller"))
        {
            print("Left Hand Entered");
            HandManager.instance.leftHand.SendHapticImpulse(0, 0.5f, 0.1f);
            leftHandin = true;

            if (fire)
            {
                HandManager.instance.leftHandOnFire = true;
            }
        }
        
        if (other.CompareTag("Right Controller"))
        {
            print("Right Hand Entered");
            HandManager.instance.rightHand.SendHapticImpulse(0, 0.5f, 0.1f);
            rightHandin = true;

            if (fire)
            {
                HandManager.instance.rightHandOnFire = true;
            }
        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Left Controller"))
        {
            print("Left Hand Removed");
            HandManager.instance.leftHand.SendHapticImpulse(0, 0.5f, 0.1f);
            leftHandin = false;
            HandManager.instance.leftHandOnFire = false;
        }
        else if (other.CompareTag("Right Controller"))
        {
            print("Right Hand Removed");
            HandManager.instance.rightHand.SendHapticImpulse(0, 0.5f, 0.1f);
            rightHandin = false;
            HandManager.instance.rightHandOnFire = false;
        }
        
    }


    void SpawnIngredient()
    {
        if(rightHandin && RightGrip.action.ReadValue<float>() >=0.9f && !HandManager.instance.rightInteractor.hasSelection)
        {
            GameObject i = Instantiate(ingredient.prefab, HandManager.instance.rightInteractor.transform.position, transform.rotation, ingredientHolder);
            i.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(HandManager.instance.rightInteractor, i.GetComponent<XRGrabInteractable>());
        }
        if (leftHandin && LeftGrip.action.ReadValue<float>() >=0.9f && !HandManager.instance.leftInteractor.hasSelection)
        {
            GameObject i = Instantiate(ingredient.prefab, HandManager.instance.leftInteractor.transform.position, transform.rotation, ingredientHolder);
            i.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(HandManager.instance.leftInteractor, i.GetComponent<XRGrabInteractable>());
        }
    }

    public override void ActivateSpawner()
    {
        active = true;
        if (fire)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        GetComponent<Collider>().enabled = true;
    }

    public override void DeactivateSpawner()
    {
        active = false;
        if (fire)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        GetComponent<Collider>().enabled = false;
    }

}
