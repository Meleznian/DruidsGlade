using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    [SerializeField] GameObject leafParticle;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(leafParticle, other.transform.position ,other.transform.rotation);
    }

}
