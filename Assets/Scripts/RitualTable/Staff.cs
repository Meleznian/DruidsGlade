using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Staff : MonoBehaviour
{
    public ActivationArea activationArea;
    GameObject effect;
    public Transform gem;
    bool aloft;

    private void Start()
    {
        effect = transform.Find("StaffEffect").gameObject;
    }

    private void Update()
    {
        StaffAloft();
    }

    public void PickedUp(SelectEnterEventArgs arg)
    {
        activationArea.staffHeld = true;

        if (arg.interactorObject == HandManager.instance.rightInteractor)
        {
            activationArea.StaffinRightHand = true;
        }
        else
        {
            activationArea.StaffinRightHand = false;
        }

    }

    public void Dropped()
    {
        activationArea.staffHeld = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ActivationArea>() == activationArea)
        {
            effect.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ActivationArea>() == activationArea)
        {
            effect.SetActive(false);
        }
    }


    void StaffAloft()
    {
        if (!aloft)
        {
            if (gem.position.y > (Camera.main.transform.position.y + 0.5f))
            {
                effect.SetActive(true);
                aloft = true;
            }
        }
        else if (aloft)
        {
            if (gem.position.y < (Camera.main.transform.position.y + 0.5f))
            {
                effect.SetActive(false);
                aloft = false;
            }
        }
    }
}
