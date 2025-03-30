using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SelectSpawner : MonoBehaviour
{
    public Ingredient ingredient;
    public Transform spawnPoint;
    public Transform interactableHolder;

    public bool activeOnStart;

    private void Start()
    {
        if (!activeOnStart)
        {
            Deactivate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnIngredient(SelectEnterEventArgs arg)
    {
        GetComponent<XRSimpleInteractable>().interactionManager.SelectExit(arg.interactorObject, GetComponent<XRSimpleInteractable>());

        GameObject i = Instantiate(ingredient.prefab, spawnPoint.position, transform.rotation, interactableHolder);

        i.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(arg.interactorObject, i.GetComponent<XRGrabInteractable>());
    }

    void Activate()
    {
        GetComponent<XRSimpleInteractable>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    void Deactivate()
    {
        GetComponent<XRSimpleInteractable>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

}
