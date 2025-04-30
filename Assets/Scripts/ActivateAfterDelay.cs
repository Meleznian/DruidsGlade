using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{
    public float delay;
    float timer;

    public GameObject objToActivate;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            objToActivate.SetActive(true);
            this.enabled = false;
        }
    }
}
