using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ritual", menuName = "RitualStuff/Ritual", order = 1)]
public class Ritual : ScriptableObject
{
    public string ritualName;
    public GameObject ritualPrefab;
    public Ingredient[] ingredients;
}

