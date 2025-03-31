using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDialogue : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IngredientScript>() != null)
        {
            Squirrel.instance.GetDialogue(other.GetComponent<IngredientScript>().ingredientScriptable.ingredientName);
        }
    }
}
