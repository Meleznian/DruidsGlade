using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRitual : MonoBehaviour
{

    public string[] ObjectToActivate;

    public float decayTime;

    float timer;


    // Start is called before the first frame update
    void Start()
    {
        ActivateSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    public void ActivateSpawner()
    {
        foreach (string s in ObjectToActivate)
        {
            MasterSpawner.instance.ActivateSpawner(s);
        }
    }

    public void Timer()
    {
        timer += Time.deltaTime;

        if (timer >= decayTime)
        {
            Destroy(gameObject);
        }
    }
}
