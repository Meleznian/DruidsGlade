using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{
    public float delay;
    float timer;
    public bool scaleRate;

    public GameObject objToActivate;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            objToActivate.SetActive(true);
            if (scaleRate)
            {
                objToActivate.GetComponent<ScaleParticleRate>().SetRate(false);
            }

            this.enabled = false;           
        }
    }
}
