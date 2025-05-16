using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public static Journal instance = null;
    internal JournalEntry currentRecipe;

    int recipeIndex;
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
        recipeIndex = 0;
        currentRecipe = Recipes[0];
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

        internal int index;
        public void RevealIngredient()
        {
            if (index < ingredients.Length && progressionRecipe)
            {
                ingredients[index].SetActive(true);
                index++;

                //char[] charText = text.text.ToCharArray();
                //int i = 0;

                //foreach (char c in charText)
                //{
                //    if(c == '?')
                //    {
                //        break;
                //    }
                //    i++;
                //}
                //
                //charText[i] = ' ';
                //text.text = new string(charText);
            }

        }
        public void RevealAll()
        {
            foreach(GameObject g in ingredients)
            {
                g.SetActive(true);
            }
            result.SetActive(true);
            //text.text = text.text.Replace('?', ' ');
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

    //public void OnGrab()
    //{
    //    backCover.isTrigger = true;
    //}
    //public void OnDrop()
    //{
    //    backCover.isTrigger = false;
    //}
}
