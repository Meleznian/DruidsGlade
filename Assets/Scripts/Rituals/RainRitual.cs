using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainRitual : MonoBehaviour
{

    public string[] ObjectToActivate;

    public float decayTime;

    float timer;


    // Start is called before the first frame update
    void Start()
    {
        foreach(string s in ObjectToActivate)
        {
            GameObject.Find(s).GetComponent<GrowSpawner>().active = true;
            GameObject.Find(s).GetComponent<GrowSpawner>().active = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= decayTime)
        {
            Destroy(gameObject);
        }

    }
}
