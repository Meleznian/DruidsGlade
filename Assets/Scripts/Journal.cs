using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public static Journal instance = null;
    internal JournalEntry currentRecipe;

    int recipeIndex;

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

    private void Start()
    {
        recipeIndex = 0;
        currentRecipe = Recipes[0];
    }

    public JournalEntry[] Recipes;

    [Serializable]
    public class JournalEntry
    {
        public string ID;
        public GameObject[] ingredients;
        public GameObject result;

        internal int index;
        public void RevealIngredient()
        {
            if (index <= ingredients.Length)
            {
                ingredients[index].SetActive(true);
                index++;
            }
        }
        public void RevealAll()
        {
            foreach(GameObject g in ingredients)
            {
                g.SetActive(true);
            }
            result.SetActive(true);
        }
    }

    public void Complete(string Id)
    {
        foreach(JournalEntry entry in Recipes)
        {
            if(entry.ID == Id)
            {
                entry.RevealAll();

                if(entry == currentRecipe)
                {
                    recipeIndex++;
                    if (recipeIndex <= Recipes.Length)
                    {
                        currentRecipe = Recipes[recipeIndex];
                    }
                }
            }
        }
    }
}
