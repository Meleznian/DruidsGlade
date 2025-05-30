using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector3 initalPos;
    Quaternion initalRot;
    Rigidbody rb;

    private void Start()
    {
        initalPos = transform.position;
        initalRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RespawnField")
        {
            print("Respawning");
            transform.position = new Vector3(0, 20,0);
            //transform.rotation = initalRot;
            rb.velocity = Vector3.zero;
        }
    }

}
