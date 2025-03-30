using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    public bool active;
    public Ingredient ingredient;
    public Transform ingredientHolder;

    private void Start()
    {
        ingredientHolder = GameObject.Find("Ingredients").transform;

        if (!active)
        {
            DeactivateSpawner();
        }
        else
        {
            ActivateSpawner();
        }
    }

    public virtual void ActivateSpawner()
    {
        active = true;
    }
    public virtual void DeactivateSpawner()
    {
        active = false;
    }
}
