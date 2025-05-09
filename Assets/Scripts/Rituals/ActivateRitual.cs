using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateRitual : MonoBehaviour
{

    public string[] ObjectToActivate;
    public Color skyColour = Color.white;
    public float skyLight = 1;

    //public float decayTime;
    public UnityEvent finishedEvent;

    float timer;


    // Start is called before the first frame update
    void Start()
    {
        LightChanger.instance.ChangeLight(skyColour, skyLight);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateSpawner()
    {
        foreach (string s in ObjectToActivate)
        {
            MasterSpawner.instance.ActivateSpawner(s);
        }

        finishedEvent.Invoke();

    }

    //public void Timer()
    //{
    //    timer += Time.deltaTime;
    //
    //    if (timer >= decayTime)
    //    {
    //    }
    //}
}
