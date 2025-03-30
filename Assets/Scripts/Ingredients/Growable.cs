using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
    public GrowSpawner spawner;
    public bool attachedToSpawner;

    public int bushReference;

    bool notkin;

    private void Start()
    {

    }
    public void RemoveFromBush()
    {
        if (attachedToSpawner)
        {
            spawner.RemoveGrowable(bushReference);
            attachedToSpawner = false;
        }
    }

    public void UpdateRB()
    {
        if(!notkin)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            notkin = true;
        }
    }
}
