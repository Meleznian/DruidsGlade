using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ScaleParticleRate : MonoBehaviour
{
    bool begun;
    public float goal;
    public float initialRate;
    bool deleteAfter;
    public float speed;
    ParticleSystem.EmissionModule em;

    ParticleSystem effect;

    private void Start()
    {
        effect = GetComponent<ParticleSystem>();
        em = effect.emission;
    }
    void Update()
    {
        if (begun)
        {
            ChangeRate();
        }
    }

    public void SetRate(bool stopAfter)
    {
        initialRate = 0;
        deleteAfter = stopAfter;
        begun = true;
        if(stopAfter)
        {
            goal = 0;
        }
    }

    void ChangeRate()
    {
        float change = Mathf.Lerp(initialRate, goal, speed * Time.deltaTime);
        initialRate = change;
        em.rateOverTime = change;

        if(Mathf.Abs(change - goal) < 2)
        {
            begun = false;
            if (deleteAfter)
            {
                effect.Stop();
            }
        }
    }
}
