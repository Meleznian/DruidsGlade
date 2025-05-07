using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Staff : MonoBehaviour
{
    public ActivationArea activationArea;
    ParticleSystem effect;
    TrailRenderer trail;
    public Transform gem;
    bool aloft;

    private void Start()
    {
        effect = transform.Find("StaffEffect").GetComponent<ParticleSystem>();
        trail = transform.Find("StaffEffect").GetComponent<TrailRenderer>();

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
            effect.Play();
            trail.emitting = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ActivationArea>() == activationArea)
        {
            effect.Stop(); 
            trail.emitting = false;

        }
    }


    void StaffAloft()
    {
        if (!aloft)
        {
            if (gem.position.y > (Camera.main.transform.position.y + 0.5f))
            {
                effect.Play();
                trail.emitting = true;
                aloft = true;
            }
        }
        else if (aloft)
        {
            if (gem.position.y < (Camera.main.transform.position.y + 0.5f))
            {
                effect.Stop();
                trail.emitting = false;
                aloft = false;
            }
        }
    }
}
