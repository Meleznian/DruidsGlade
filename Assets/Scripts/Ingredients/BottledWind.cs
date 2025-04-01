using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class BottledWind : IngredientScript
{
    public float burstForce;
    public float burstRadius;

    bool burst;
    float decayTimer;

    private void Update()
    {
        if (eating)
        {
            Eat();
        }

        if ( burst)
        {
            decayTimer += Time.deltaTime;
            if(decayTimer >= 0.5f)
            {
                Destroy(gameObject);
            }
        }

    }

    public override void Break()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, burstRadius);

        foreach (Collider c in nearby)
        {           
            if(c.GetComponent<Rigidbody>() != null)
            {
                c.GetComponent<Rigidbody>().AddExplosionForce(burstForce, transform.position, burstRadius, 0, ForceMode.Impulse);
                transform.Find("Model").gameObject.SetActive(false);
                burst = true;
            }
        }
    }
}
