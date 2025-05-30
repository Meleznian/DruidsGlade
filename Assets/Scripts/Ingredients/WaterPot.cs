using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WaterPot : MonoBehaviour
{

    public Ingredient waterDrop;
    public Ingredient pot;
    public MeshRenderer water;
    public float tiltThreshold;
    IngredientScript i;
    RitualTable rt;

    bool full;
    bool inWater;
    bool inAA;

    public float colliderCooldown;
    float timer;

    BoxCollider jar;

    // Start is called before the first frame update
    void Start()
    {
        jar = GetComponent<BoxCollider>();
        i = GetComponent<IngredientScript>();
        rt = GameObject.Find("RitualArea").GetComponent<RitualTable>();

        PotManager.instance.potList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Empty();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = true;
        }
        if (other.GetComponent<ActivationArea>() != null)
        {
            inAA = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && full == false && Vector3.Dot(transform.up, Vector3.down) < tiltThreshold)
        {
            print("The Object " + other.gameObject.name + " is Water");
            water.enabled = true;
            full = true;
            i.ingredientScriptable = waterDrop;


            if (other.transform.parent.GetComponent<XRGrabInteractable>() != null)
            {
                Destroy(other.transform.parent.gameObject);
                inWater = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = false;
        }
        if (other.GetComponent<ActivationArea>() != null)
        {
            inAA = false;
        }
    }

    void Empty()
    {
        if (full && Vector3.Dot(transform.up, Vector3.down) > tiltThreshold && !inWater)
        {
            Instantiate(waterDrop.prefab, water.transform.position + (transform.up * 0.1f), transform.rotation, transform.parent);
            water.enabled = false;
            full = false;
            jar.enabled = false;
            timer = 0;
            i.ingredientScriptable = pot;

            if (inAA)
            {
                rt.ingredients.Remove(waterDrop);
                rt.ingredients.Add(pot);
            }

        }

        if (jar.enabled == false)
        {
            timer += Time.deltaTime;

            if (timer >= colliderCooldown)
            {
                jar.enabled = true;
            }
        }
    }

    public void RitualEmpty()
    {
        water.enabled = false;
        full = false;
        jar.enabled = false;
        timer = 0;
        i.ingredientScriptable = pot;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;

        GetComponent<IngredientScript>().DeactivateSparkle();
    }
}
