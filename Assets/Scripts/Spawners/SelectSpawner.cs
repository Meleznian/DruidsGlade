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
    public GameObject grabEffect;

    public void SpawnIngredient(SelectEnterEventArgs arg)
    {
        if (active)
        {
            GetComponent<XRSimpleInteractable>().interactionManager.SelectExit(arg.interactorObject, GetComponent<XRSimpleInteractable>());

            GameObject i = Instantiate(ingredient.prefab, spawnPoint.position, transform.rotation, ingredientHolder);

            i.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(arg.interactorObject, i.GetComponent<XRGrabInteractable>());
            AudioManager.instance.PlayAtLocation("Leaves", transform.position);
            Instantiate(grabEffect, i.transform.position, Quaternion.Euler(Vector3.zero));

        }
    }

    public override void ActivateSpawner()
    {
        active = true;
        GetComponent<XRSimpleInteractable>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<OutlineOnHover>().enabled = true;
    }

    public override void DeactivateSpawner()
    {
        active = false;
        GetComponent<XRSimpleInteractable>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

}
