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

    public ParticleSystem fireEffect;
    public ParticleSystem sparkle;
    public ParticleSystem breakEffect;
    public GameObject smoke;

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

        if(onFire)
        {
            burnTimer += Time.deltaTime;

            if(burnTimer >= 5)
            {
                Instantiate(smoke, transform.position, Quaternion.Euler(Vector3.zero));
                AudioManager.instance.PlayAtLocation("Douse", transform.position);
                Break();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlayAtLocation("Thud", transform.position);

        if (unstable)
        {
            if(collision.gameObject.name != "RitualTable")
            {
                print(collision.gameObject.name);
                Instantiate(smoke, transform.position, Quaternion.Euler(Vector3.zero));
                AudioManager.instance.PlayAtLocation("Douse", transform.position);
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
        if (other.gameObject.CompareTag("Fire") && flammable)
        {
            print("Aflame");
            Ignite();
        }
        else if (other.gameObject.CompareTag("Water") && onFire)
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
        }
    }

    internal void Eat()
    {
        timer += Time.deltaTime;

        if (timer >= eatTime)
        {
            Break();
        }

        if (timer >= nextEatSound)
        {
            AudioManager.instance.PlayAudio("Eat");

            nextEatSound *= 2;
        }
    }

    public virtual void Break()
    {
        if (ingredientScriptable.ingredientID == 0)
        {
            PotManager.instance.RemovePot(gameObject, false);
            AudioManager.instance.PlayAtLocation("PotSmash", transform.position);
        }
        if (breakEffect != null)
        {
            Instantiate(breakEffect,transform.position,transform.rotation);
        }

        Destroy(gameObject);
    }

    void Ignite()
    {
        onFire = true;
        fireEffect.Play();
    }

    void Douse()
    {
        onFire = false;
        fireEffect.Stop();
        if(ingredientScriptable.name == "Ball of Fire")
        {
            Instantiate(smoke, transform.position, Quaternion.Euler(Vector3.zero));
            AudioManager.instance.PlayAtLocation("Douse", transform.position);
            Destroy(gameObject);
        }
    }

    public void ActivateSparkle()
    {
        sparkle.Play();
        if (GetComponent<Outline>() != null)
        {
            GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            GetComponent<Outline>().enabled = false;
        }
    }

    public void DeactivateSparkle()
    {
        sparkle.Stop();
        if (GetComponent<Outline>() != null)
        {
            GetComponent<Outline>().enabled = true;
            GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
        }

    }
}