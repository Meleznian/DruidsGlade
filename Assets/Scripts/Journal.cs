using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public static Journal instance = null;
    [SerializeField]internal JournalEntry currentRecipe;

    [SerializeField]int recipeIndex;
    //public Collider backCover;

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
        recipeIndex = 1;
        currentRecipe = Recipes[1];
    }

    public JournalEntry[] Recipes;

    [Serializable]
    public class JournalEntry
    {
        public string ID;
        public TMP_Text text;
        public GameObject[] ingredients;
        public GameObject result;
        public bool progressionRecipe;

        public int index;
        public void RevealIngredient()
        {
            print("Revealing Ingredient");
            if (index < ingredients.Length && progressionRecipe)
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

                print("Comparing "+ entry.ID + " and "  +currentRecipe.ID);
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

    //public void OnGrab()
    //{
    //    backCover.isTrigger = true;
    //}
    //public void OnDrop()
    //{
    //    backCover.isTrigger = false;
    //}
}
