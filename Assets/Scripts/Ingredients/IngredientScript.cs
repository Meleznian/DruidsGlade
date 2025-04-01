using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{ 
    public Ingredient ingredientScriptable;

    [Header("Options")]
    public bool unstable;
    public bool fragile;
    public float endurance;
    public float eatTime;


    internal bool eating;
    float timer;

    float nextEatSound;
    float eatSoundTime;

    private void Start()
    {
        eatSoundTime = (eatTime / 3);
        nextEatSound = eatSoundTime;
    }

    private void Update()
    {
        if (eating)
        {
            Eat();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (unstable)
        {
            if(collision.gameObject.name != "RitualTable")
            {
                print(collision.gameObject.name);
                Break();
            }
        }
        else if(fragile)
        {
            if(collision.relativeVelocity.magnitude >= endurance)
            {
                Break();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mouth"))
        {
            print("Nom");
            eating = true;
            nextEatSound = eatSoundTime;
            AudioManager.instance.PlayAudio("Eat");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Mouth"))
        {
            print("Wait no don't eat that!");
            eating = false;
            nextEatSound = eatSoundTime;
            timer = 0;
        }
    }

    internal void Eat()
    {
        timer += Time.deltaTime;

        if (timer >= eatTime)
        {
            Destroy(gameObject);
        }

        if (timer >= nextEatSound)
        {
            AudioManager.instance.PlayAudio("Eat");

            nextEatSound *= 2;
        }
    }

    public virtual void Break()
    {
        Destroy(gameObject);
    }
}
