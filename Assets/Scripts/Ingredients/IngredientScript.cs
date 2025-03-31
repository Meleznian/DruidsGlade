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


    bool eating;
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
                Destroy(gameObject);
            }
        }
        else if(fragile)
        {
            if(collision.relativeVelocity.magnitude >= endurance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mouth"))
        {
            print("Nom");
            eating = true;
            AudioManager.instance.PlayAudio("Eat" + gameObject.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Mouth"))
        {
            print("Wait no don't eat that!");
            eating = false;
            timer = 0;
        }
    }

    void Eat()
    {
        timer += Time.deltaTime;

        if (timer >= nextEatSound)
        {
            AudioManager.instance.PlayAudio("Eat" + gameObject.name);
            nextEatSound = (eatSoundTime * 2);
        }

        if (timer >= eatTime)
        {
            Destroy(gameObject);
        }
    }
}
