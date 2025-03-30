using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "RitualStuff/Ingredient", order = 1)]

public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public int ingredientID;
    public GameObject prefab;
}

