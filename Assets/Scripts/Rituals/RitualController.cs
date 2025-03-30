using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RitualController : MonoBehaviour
{

    public static RitualController instance = null;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    [Serializable]
    public class RitualEntry
    {
        public string refID;
        public Ritual ritual;
        public bool completed;
        public bool getsHints;
    }

    public List<RitualEntry> ritualList = new();

    public int nextHint;

    public Ritual GetRitual(List<Ingredient> ingredients)
    {
        List<Ingredient> tempRecipe = new List<Ingredient>();

        foreach (RitualEntry r in ritualList)
        {
            if (ingredients.Count == r.ritual.ingredients.Length)
            {
                print("Comparing Ritual: " + r.ritual.ritualName);

                bool recipeMatch = true;

                tempRecipe = r.ritual.ingredients.ToList();

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
                    return r.ritual;
                }
            }
        }

        return null;
        
    }

    public void CheckOffRitual(Ritual ritual)
    {
        foreach(RitualEntry r in ritualList)
        {
            if(r.ritual == ritual)
            {
                r.completed = true;
            }
        }

        int i = 0;

        foreach (RitualEntry r in ritualList)
        {
            if(!r.completed && r.getsHints)
            {
                nextHint = i;
                break;
            }

            i++;
        }
    }
}
