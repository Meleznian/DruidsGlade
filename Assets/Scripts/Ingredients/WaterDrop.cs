using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class WaterDrop : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Puddle;
    public Rigidbody rb;

    bool grabbed;

    public LayerMask ignore;
    
    // Start is called before the first frame update
    void Start()
    {
        Puddle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickedUp()
    {
        grabbed = true;
        Ball.SetActive(true);
        Puddle.SetActive(false);
    }

    public void Dropped()
    {
        grabbed= false;
        rb.isKinematic = false;
    }

    void Landed()
    {
        rb.isKinematic = true;

        Ball.SetActive(false);
        Puddle.SetActive(true);
        transform.rotation = Quaternion.Euler(Vector3.zero);

        GetComponent<XRGrabInteractable>().enabled = false;
        GetComponent<XRGrabInteractable>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!grabbed && other.gameObject.name == "WaterSpawner")
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        RaycastHit hit;
        if (!grabbed && Physics.Raycast(transform.position, -Vector3.up,out hit, 0.15f, ~ignore))
        {
            if (hit.collider == collision.collider)
            {
                Landed();
            }
        }
    }
}
