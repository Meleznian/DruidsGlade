using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RitualTable : MonoBehaviour
{
    List<IngredientScript> ingredientObjects = new List<IngredientScript>();
    public List<Ingredient> ingredients = new List<Ingredient>();

    public List<Ritual> rituals = new List<Ritual>();

    public float failureForce;
    public float failureSize;

    Ritual currentRitual;

    public float activationTime;
    public float floatStrength;
    public float activationTimer;


    bool validRitual;
    bool ritualActive;

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
        if (ingredients.Count > 0 && !ritualActive)
        {
            bool ritualFound = false;
            List<Ingredient> tempRecipe = new List<Ingredient>();

            foreach (Ritual r in rituals)
            {
                if (ingredients.Count == r.ingredients.Length)
                {
                    print("Comparing Ritual: " + r.ritualName);

                    bool recipeMatch = true;

                    tempRecipe = r.ingredients.ToList();

                    tempRecipe = tempRecipe.OrderBy(x => x.ingredientID).ToList();
                    ingredients = ingredients.OrderBy(x => x.ingredientID).ToList();

                    for (int i = 0; i < ingredients.Count; i++)
                    {

                        print("Comparing " + ingredients[i].ingredientName + " and " + tempRecipe[i].ingredientName);

                        if (tempRecipe[i].ingredientID == ingredients[i].ingredientID)
                        {
                            print("Match Success");
                            continue;
                        }
                        else
                        {
                            print("Match Fail");
                            recipeMatch = false;
                            break;
                        }
                    }

                    if (recipeMatch == true)
                    {
                        print("Ritual Found: " + r.ritualName);
                        currentRitual = r;
                        validRitual = true;
                        ritualFound = true;
                        PrepareRitual();
                        break;
                    }
                }
            }

            if(!ritualFound)
            {
                print("Ritual Not Found");
                validRitual = false;
                PrepareRitual();
            }

        }
    }

    //Throw ingredients everywhere
    void RitualFail()
    {
        foreach(IngredientScript i in ingredientObjects)
        {
            i.GetComponent<Collider>().enabled = true;
            i.GetComponent<Rigidbody>().useGravity = true;
            
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
            i.GetComponent<Rigidbody>().AddForce(Vector3.up * floatStrength);
        }

        ritualActive = true;
        activationTimer = 0;
    }
     
    //Instantiate ritual prefab and clear the table
    void RitualSuccess(Ritual r)
    {
        print("Activating " + r.name);

        for(int i = ingredientObjects.Count - 1; i >= 0 ; i--)
        {
            Destroy(ingredientObjects[i].gameObject);
        }

        ingredientObjects.Clear();
        ingredients.Clear();

        Instantiate(r.ritualPrefab, transform.position, Quaternion.identity);

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
