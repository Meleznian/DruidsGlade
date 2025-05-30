using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Rock : MonoBehaviour
{
    public float strength;
    public GameObject sparks;
    public Animator anim;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grow") == true)
        {
            print("Growing");
            return;
        }
        else if(collision.gameObject.CompareTag("Pickaxe"))
        {
            Vector3 controllerVelocity;
            Instantiate(sparks, transform.position, Quaternion.identity);

            if (collision.gameObject.GetComponent<HandCheck>().held == true)
            {
                collision.gameObject.GetComponent<HandCheck>().heldBy.TryGetFeatureValue(CommonUsages.deviceVelocity, out controllerVelocity);

                if (controllerVelocity.magnitude >= strength)
                {
                    Instantiate(sparks, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
    }
}
