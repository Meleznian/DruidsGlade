using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationRitual : MonoBehaviour
{ 

    public int numberOfObjects;
    public GameObject ObjectPrefab;

    public float burstForce;
    public float burstRadius;

    public float decayTime;

    float timer;

    Transform interactableHolder;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        interactableHolder = GameObject.Find("Interactables").transform;
    }

    // Update is called once per frame
    void Update()
    {
        while(i < numberOfObjects)
        {
            Rigidbody rb = Instantiate(ObjectPrefab, new Vector3(transform.position.x + Random.Range(0.3f,-0.3f),transform.position.y + 0.3f, transform.position.z + Random.Range(0.3f, -0.3f)), transform.rotation, interactableHolder).GetComponent < Rigidbody>();

            rb.AddExplosionForce(burstForce, transform.position, burstRadius);
            i++;
        }

        if(i >= numberOfObjects)
        {
            timer += Time.deltaTime;

            if (timer >= decayTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
