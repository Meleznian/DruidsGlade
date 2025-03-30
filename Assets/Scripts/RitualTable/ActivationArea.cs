using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationArea : MonoBehaviour
{
    public float activationSpeed;
    public GameObject staff;
    public RitualTable table;
    bool staffInArea;


    //private void Update()
    //{
    //    if(staffInArea)
    //    {
    //        print(staff.GetComponent<Rigidbody>().velocity.magnitude);
    //
    //        if(staff.GetComponent<Rigidbody>().velocity.magnitude == activationSpeed)
    //        {
    //            print("velocity reached");
    //            table.ActivateRitual();
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Staff")
        {
            staffInArea = true;
            table.ActivateRitual();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Staff")
        {
            staffInArea = false;
        }
    }

}
