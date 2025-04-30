using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
    public int orbs;
    bool orbsActive;


    public int nextHint;
    bool potDone;

    private void Update()
    {
        if (orbsActive)
        {
            CheckMagic();
        }
    }


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

                if(r.refID == "PotRitual")
                {
                    Squirrel.instance.GetDialogue("Congrats");
                } 
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

        if(i == ritualList.Count)
        {
            nextHint = -1;
        }
    }

    public bool CheckCompletion(string ID)
    {
        foreach(RitualEntry r in ritualList)
        {
            if(r.refID == ID)
            {
                return r.completed;
            }
        }

        Debug.LogWarning("Ritual " + ID + " not found, please check ID");
        return false;
    }


    void SpawnRitual(Ritual r)
    {
        Instantiate(r.ritualPrefab, new Vector3(0, 1.2f, 0), Quaternion.identity);
        nextRitual = null;
        orbsActive = false;
    }

    public void StartMagic(Ritual r)
    {
        orbsActive = true;
        nextRitual = r;
    }

    Ritual nextRitual;

    void CheckMagic()
    {
        if (orbs == 0)
        {
            SpawnRitual(nextRitual);
        }
    }
}
