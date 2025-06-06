using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightningBehaviour : MonoBehaviour
{
    [SerializeField] FadeInObject cloud;
    public ParticleSystem lightning;
    public Transform campfire;
    public float delay;
    float timer;
    bool startTimer;

    private void Start()
    {
        campfire = GameObject.Find("Campfire").transform;
        //lightning.transform.position = new Vector3(campfire.position.x, 12, campfire.position.z);
    }

    private void Update()
    {
        if (startTimer)
        {
            lightning.Play();
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                cloud.FadeOut();
                startTimer = false;
            }
        }
    }

    public void Finished()
    {
        Destroy(gameObject);
    }


    public void Strike()
    {
        AudioManager.instance.PlayAudio("Fire");
        AudioManager.instance.PlayAtLocation("LightningStrike", campfire.transform.position);
        GetComponent<ActivateRitual>().ActivateSpawner();
        startTimer = true;
    }
}
