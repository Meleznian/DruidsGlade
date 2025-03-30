using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotRitualScript : MonoBehaviour
{

    public int numberOfPots;
    public GameObject PotPrefab;

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
        while(i < numberOfPots)
        {
            Rigidbody rb = Instantiate(PotPrefab, new Vector3(transform.position.x + Random.Range(0.3f,-0.3f),transform.position.y + 0.3f, transform.position.z + Random.Range(0.3f, -0.3f)), transform.rotation, interactableHolder).GetComponent < Rigidbody>();

            rb.AddExplosionForce(burstForce, transform.position, burstRadius);
            i++;
        }

        if(i >= numberOfPots)
        {
            timer += Time.deltaTime;

            if (timer >= decayTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
