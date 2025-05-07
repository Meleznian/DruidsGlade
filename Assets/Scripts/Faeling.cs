using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faeling : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    bool travelling;
    bool revolving;
    public float speed;
    Animator anim;
    Transform t;

    void Start()
    {
        t = transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (travelling)
        {
            Travel();
        }
        else if (revolving)
        {

        }
    }

    public void GetTarget(Transform Target)
    {
        target = Target;
        travelling = true;
    }

    void Travel()
    {
        t.position = Vector3.MoveTowards(t.position, target.position, speed);

        if(t.position == target.position)
        {
            travelling = false;
            revolving = true;
            anim.SetTrigger("Arrived");
        }
    }
}
