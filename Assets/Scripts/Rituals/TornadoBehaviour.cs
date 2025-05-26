using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoBehaviour : MonoBehaviour
{
    public GameObject bottle;
    public GameObject appearEffect;
    public GameObject BottledWind;
    public ParticleSystem fog;
    public ParticleSystem leaves;
    public ParticleSystem tornado;

    private void Start()
    {
        AudioManager.instance.PlayAudio("Tornado");
    }

    private void Update()
    {
        if(fog == null && leaves == null && tornado == null)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void BottleAppear()
    {
        Instantiate(appearEffect, bottle.transform.position,bottle.transform.rotation);
        bottle.GetComponent<MeshRenderer>().enabled = true;
    }

    public void CreateBottle()
    {
        Instantiate(appearEffect, bottle.transform.position, bottle.transform.rotation);
        Rigidbody rb = Instantiate(BottledWind, bottle.transform.position, bottle.transform.rotation).GetComponent<Rigidbody>();
        rb.AddExplosionForce(50, transform.position, 3);
        Squirrel.instance.GetDialogue("Primordial");
    }

    public void Stop()
    {
        tornado.Stop();
        Destroy(bottle);
        fog.Stop();
        leaves.Stop();
    }


}
