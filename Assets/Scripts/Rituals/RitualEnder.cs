using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualEnder : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem effect;
    public float killDelay;
    float timer;
    bool begun;

    // Update is called once per frame
    void Update()
    {
        if (begun)
        {
            timer += Time.deltaTime;

            if (effect == null)
            {
                Destroy(gameObject);
            }
            else if (timer >= killDelay)
            {
                LightChanger.instance.ChangeLight(LightChanger.instance.defaultColour, 1);
                
                if(effect.GetComponent<ScaleParticleRate>() != null)
                {
                    effect.GetComponent<ScaleParticleRate>().SetRate(true);
                }
                else
                {
                    effect.Stop();
                }
            }
        }


    }
    public void StartEnd()
    {
        begun = true;
    }
}
