using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RitualTable : MonoBehaviour
{
    List<IngredientScript> ingredientObjects = new List<IngredientScript>();
    public List<Ingredient> ingredients = new List<Ingredient>();

    public float failureForce;
    public float failureSize;
    public ParticleSystem failEffect;

    Ritual currentRitual;

    public float activationTime;
    public float floatStrength;
    public float activationTimer;


    bool validRitual;
    bool ritualActive;

    public GameObject magicOrb;

    private void Update()
    {
        if (ritualActive)
        {
            activationTimer += Time.deltaTime;

            if(activationTimer >= activationTime)
            {
                if (validRitual)
                {
                    RitualSuccess(currentRitual);
                    currentRitual = null;
                }
                else
                {
                    RitualFail();
                    currentRitual = null;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IngredientScript>() != null)
        {
            ingredientObjects.Add(other.GetComponent<IngredientScript>());
            ingredients.Add(other.GetComponent<IngredientScript>().ingredientScriptable);
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (ingredientObjects.Contains(other.gameObject.GetComponent<IngredientScript>()))
        {
            ingredientObjects.Remove(other.gameObject.GetComponent<IngredientScript>());
            ingredients.Remove(other.gameObject.GetComponent<IngredientScript>().ingredientScriptable);
        }
    }

    public void ActivateRitual()
    {
        CheckIngredients();
        if (!ritualActive)
        {
            if (ingredients.Count > 0)
            {
                Ritual r = RitualController.instance.GetRitual(ingredients);

                if (r != null)
                {
                    print("Ritual Found: " + r.ritualName);
                    currentRitual = r;
                    validRitual = true;
                    PrepareRitual();
                }
                else
                {
                    print("Ritual Not Found");
                    validRitual = false;
                    PrepareRitual();
                }
            }
            else
            {
                print("No Ingredients Found");
                Squirrel.instance.GetDialogue("NoIng");
            }
        }
    }

    //Throw ingredients everywhere
    void RitualFail()
    {
        failEffect.Play();
        Journal.instance.currentRecipe.RevealIngredient();
        Squirrel.instance.GetDialogue("RitFailed");

        foreach(IngredientScript i in ingredientObjects)
        {
            i.GetComponent<Collider>().enabled = true;
            i.GetComponent<Rigidbody>().useGravity = true;

            i.DeactivateSparkle();
            i.GetComponent<Rigidbody>().AddExplosionForce(failureForce, transform.position, failureSize);
        }

        ingredientObjects.Clear();
        ingredients.Clear();

        ritualActive = false;
    }


    //Prepare the ingredients for the ritual by making them float;
    void PrepareRitual()
    {
        foreach(IngredientScript i in  ingredientObjects)
        {
            i.GetComponent<Collider>().enabled = false;
            i.GetComponent<Rigidbody>().useGravity = false;

            if (i.GetComponent<WaterDrop>() != null)
            {
                i.GetComponent<WaterDrop>().Dropped();
            }
            i.ActivateSparkle();
            i.GetComponent<Rigidbody>().AddForce(Vector3.up * floatStrength);
        }
        AudioManager.instance.PlayAudio("Sparkle");
        ritualActive = true;
        activationTimer = 0;
    }
     
    //Instantiate ritual prefab and clear the table
    void RitualSuccess(Ritual r)
    {
        print("Activating " + r.name);

        for(int i = ingredientObjects.Count - 1; i >= 0 ; i--)
        {
            Instantiate(magicOrb, ingredientObjects[i].gameObject.transform.position, transform.rotation);
            RitualController.instance.orbs += 1;

            if (ingredientObjects[i].ingredientScriptable.ingredientID == 0)
            {
                print("Removing Pot");
                if (r.ritualName == "Pot Ritual")
                {
                    PotManager.instance.RemovePot(ingredientObjects[i].gameObject, true);
                }
                else
                {
                    PotManager.instance.RemovePot(ingredientObjects[i].gameObject, false);
                }
            }

            if (ingredientObjects[i].ingredientScriptable.ingredientID == 3 && ingredientObjects[i].GetComponent<WaterPot>() != null)
            {
                ingredientObjects[i].GetComponent<WaterPot>().RitualEmpty();
            }
            else
            {
                Destroy(ingredientObjects[i].gameObject);
            }
        }

        ingredientObjects.Clear();
        ingredients.Clear();

        RitualController.instance.StartMagic(r);

        RitualController.instance.CheckOffRitual(r);

        ritualActive = false;
    }

    void CheckIngredients()
    {
        for (int i = ingredientObjects.Count - 1; i >= 0; i--)
        {
            if (ingredientObjects[i] == null)
            {
                ingredientObjects.Remove(ingredientObjects[i]);
                ingredients.Remove(ingredients[i]);
            }
        }
    }
}
