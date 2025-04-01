using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class IngredientDialogue : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IngredientScript>() != null)
        {
            if (other.GetComponent<XRGrabInteractable>().isSelected == true)
            {
                Squirrel.instance.GetDialogue(other.GetComponent<IngredientScript>().ingredientScriptable.ingredientName);
            }
        }
    }
}
