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
    public bool edible;
    public bool flammable;
    public float endurance;
    public float eatTime;

    bool onFire;

    internal bool eating;
    float timer;
    float burnTimer;

    float nextEatSound;
    float eatSoundTime;

    GameObject fireEffect;
    GameObject sparkle;

    private void Start()
    {
        eatSoundTime = (eatTime / 3);
        nextEatSound = eatSoundTime;
        fireEffect = transform.Find("FireEffect").gameObject;
        sparkle = transform.Find("WhiteSparkle").gameObject;
    }


    private void Update()
    {
        if (eating)
        {
            Eat();
        }

        if(onFire)
        {
            burnTimer += Time.deltaTime;

            if(burnTimer >= 5)
            {
                Destroy(gameObject);
            }
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
            if (edible)
            {
                print("Nom");
                eating = true;
                nextEatSound = eatSoundTime;
                AudioManager.instance.PlayAudio("Eat");
            }
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            Ignite();
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            Douse();
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

    void Ignite()
    {
        onFire = true;
        fireEffect.SetActive(true);
    }

    void Douse()
    {
        onFire = false;
        fireEffect.SetActive(false);
    }

    public void ActivateSparkle()
    {
        sparkle.SetActive(true);
    }

    public void DeactivateSparkle()
    {
        sparkle.SetActive(false);
    }
}
