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

            //if(GetComponent<Rigidbody>().velocity.magnitude > endurance)
            //{
            //    Destroy(gameObject);
            //}
            //else if(collision.gameObject.GetComponent<Rigidbody>() != null)
            //{
            //    if(collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > endurance)
            //    {
            //        Destroy(gameObject);
            //    }
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mouth"))
        {
            print("Nom");
            eating = true;
        }
    }

    void Eat()
    {
        timer += Time.deltaTime;

        if(timer >= eatTime)
        {
            Destroy(gameObject);
        }
    }
}
