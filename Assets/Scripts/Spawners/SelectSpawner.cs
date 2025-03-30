using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SelectSpawner : SpawnerBase
{
    public Transform spawnPoint;

    public void SpawnIngredient(SelectEnterEventArgs arg)
    {
        if (active)
        {
            GetComponent<XRSimpleInteractable>().interactionManager.SelectExit(arg.interactorObject, GetComponent<XRSimpleInteractable>());

            GameObject i = Instantiate(ingredient.prefab, spawnPoint.position, transform.rotation, ingredientHolder);

            i.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(arg.interactorObject, i.GetComponent<XRGrabInteractable>());
        }
    }

    public override void ActivateSpawner()
    {
        active = true;
        GetComponent<XRSimpleInteractable>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public override void DeactivateSpawner()
    {
        active = false;
        GetComponent<XRSimpleInteractable>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

}
