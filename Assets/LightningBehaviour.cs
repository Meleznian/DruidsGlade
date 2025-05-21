using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehaviour : MonoBehaviour
{

    public GameObject lightning;
    public GameObject strikeEffect;
    public Transform campfire;

    private void Start()
    {
        campfire = GameObject.Find("Campfire").transform;
        lightning.transform.position = new Vector3(campfire.position.x, 12, campfire.position.z);
    }

    private void Update()
    {
        if(lightning == null)
        {
            Instantiate(strikeEffect, new Vector3(campfire.position.x, 1, campfire.position.z), campfire.rotation);
            Destroy(lightning);
            GetComponent<ActivateRitual>().ActivateSpawner();
            Destroy(transform.parent);
        }
    }

    public void Strike()
    {
        lightning.SetActive(true);
    }
}
