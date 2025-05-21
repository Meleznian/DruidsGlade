using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{
    public GameObject splash;
    public bool manualY;
    public float ypos;

    private void OnTriggerEnter(Collider other)
    {
        if (manualY)
        {
            Instantiate(splash, new Vector3(other.transform.position.x, ypos, other.transform.position.z), transform.rotation);
        }
        else
        {
            Instantiate(splash, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), transform.rotation);
        }
        AudioManager.instance.PlayAtLocation("Splash", transform.position);
    }

}
