using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class TriggerSpawner : MonoBehaviour
{
    public Ingredient ingredient;
    public Transform interactableHolder;

    bool leftHandin;
    bool rightHandin;

    public InputActionReference RightGrip;
    public InputActionReference LeftGrip;

    // Update is called once per frame
    void Start()
    {

    }

    private void Update()
    {
        SpawnIngredient();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Left Controller")
        {
            print("Left Hand Entered");
            HandManager.instance.leftHand.SendHapticImpulse(0, 0.5f, 0.1f);
            leftHandin = true;
        }
        else if(other.gameObject.name == "Right Controller")
        {
            print("Right Hand Entered");
            HandManager.instance.rightHand.SendHapticImpulse(0, 0.5f, 0.1f);
            rightHandin = true;
        }     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Left Controller")
        {
            print("Left Hand Removed");
            HandManager.instance.leftHand.SendHapticImpulse(0, 0.5f, 0.1f);
            leftHandin = false;
        }
        else if (other.gameObject.name == "Right Controller")
        {
            print("Right Hand Removed");
            HandManager.instance.rightHand.SendHapticImpulse(0, 0.5f, 0.1f);
            rightHandin = false;
        }
    }


    void SpawnIngredient()
    {
        if(rightHandin && RightGrip.action.ReadValue<float>() == 1 && !HandManager.instance.rightInteractor.hasSelection)
        {
            GameObject i = Instantiate(ingredient.prefab, HandManager.instance.rightInteractor.transform.position, transform.rotation, interactableHolder);
            i.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(HandManager.instance.rightInteractor, i.GetComponent<XRGrabInteractable>());
        }
        if (leftHandin && LeftGrip.action.ReadValue<float>() == 1 && !HandManager.instance.leftInteractor.hasSelection)
        {
            GameObject i = Instantiate(ingredient.prefab, HandManager.instance.leftInteractor.transform.position, transform.rotation, interactableHolder);
            i.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(HandManager.instance.leftInteractor, i.GetComponent<XRGrabInteractable>());
        }
    }
}
