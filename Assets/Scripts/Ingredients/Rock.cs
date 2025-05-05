using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Rock : MonoBehaviour
{
    public float strength;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pickaxe"))
        {
            Vector3 controllerVelocity;

            if (collision.gameObject.GetComponent<HandCheck>().held == true)
            {
                collision.gameObject.GetComponent<HandCheck>().heldBy.TryGetFeatureValue(CommonUsages.deviceVelocity, out controllerVelocity);

                if (controllerVelocity.magnitude >= strength)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
