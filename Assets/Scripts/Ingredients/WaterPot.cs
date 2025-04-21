using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WaterPot : MonoBehaviour
{

    public Ingredient waterDrop;
    public MeshRenderer water;
    public float tiltThreshold;

    bool full;
    bool inWater;

    public float colliderCooldown;
    float timer;

    BoxCollider jar;

    // Start is called before the first frame update
    void Start()
    {
        jar = GetComponent<BoxCollider>();
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && full == false && Vector3.Dot(transform.up, Vector3.down) < tiltThreshold)
        {
            print("The Object "+ other.gameObject.name + " is Water");
            water.enabled = true;
            full = true;   

            if(other.transform.parent.GetComponent<XRGrabInteractable>() != null)
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
}
