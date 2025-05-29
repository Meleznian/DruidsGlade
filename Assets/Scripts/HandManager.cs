using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandManager : MonoBehaviour
{

    public static HandManager instance = null;

    [Header("Variables")]
    public float forceMult;

    [Header("Technical Stuff")]
    public Transform leftHandT;
    public Transform rightHandT;
    public Rigidbody Staff;
    public Rigidbody book;
    public XRRayInteractor teleporter;
    public MeshRenderer teleportCrystal;
    public Material crystalActive;
    public Material crystalInactive;

    [HideInInspector]

    public IXRSelectInteractor leftInteractor;
    public IXRSelectInteractor rightInteractor;
    public bool leftHandOnFire;
    public bool rightHandOnFire;
    public UnityEngine.XR.InputDevice rightHand;
    public UnityEngine.XR.InputDevice leftHand;
    public InputActionReference RightGrip;
    public InputActionReference leftGrip;
    public InputActionReference RightA;

    public float HandHeight;

    public ParticleSystem handglow;
    public ParticleSystem lefthandglow;



    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        leftInteractor = leftHandT.GetComponent<IXRSelectInteractor>();
        rightInteractor = rightHandT.GetComponent<IXRSelectInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        ToggleTeleport();
        OnFire();
    }

    private void FixedUpdate()
    {
        print(RightGrip.action.ReadValue<float>());
        HandGlow();
        SummonStaff();
        SummonBook();
    }

    void SummonStaff()
    {
        if (rightHandT.position.y > (Camera.main.transform.position.y + HandHeight) && RightGrip.action.ReadValue<float>() >= 0.9f && !rightInteractor.hasSelection)
        {
            print("Summoning Staff");
            Staff.AddForce((rightHandT.position - Staff.transform.position)*(Vector3.Distance(rightHandT.position, Staff.transform.position)*forceMult), ForceMode.Impulse);

            if(Vector3.Distance(rightHandT.position, Staff.transform.position) <= 1)
            {
                Staff.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(rightInteractor, Staff.GetComponent<XRGrabInteractable>());
                Squirrel.instance.GetDialogue("StaffSummon");
            }
        }
    }
    void SummonBook()
    {
        if (leftHandT.position.y > (Camera.main.transform.position.y + HandHeight) && leftGrip.action.ReadValue<float>() >=0.9f && !leftInteractor.hasSelection)
        {
            //print("Summoning Staff");
            book.AddForce((leftHandT.position - book.transform.position) * (Vector3.Distance(leftHandT.position, book.transform.position) * forceMult), ForceMode.Impulse);

            if (Vector3.Distance(leftHandT.position, book.transform.position) <= 1)
            {
                book.GetComponent<XRGrabInteractable>().interactionManager.SelectEnter(leftInteractor, book.GetComponent<XRGrabInteractable>());
            }
        }
    }


    void ToggleTeleport()
    {
        if (RightA.action.triggered)
        {
            if(teleporter.enabled == true)
            {
                teleporter.enabled = false;
                teleportCrystal.material = crystalInactive;
            }
            else
            {
                teleporter.enabled = true;
                teleportCrystal.material = crystalActive;

            }
        }
    }

    public void CheckHands()
    {
        if (!rightHand.isValid)
        {
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

            if (rightHand.isValid)
            {
                print("Right Hand Found");
            }
        }
        if (!leftHand.isValid)
        {
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

            if (leftHand.isValid)
            {
                print("Right Hand Found");
            }
        }
    }

    void OnFire()
    {
        if (leftHandOnFire)
        {
            leftHand.SendHapticImpulse(0, 0.5f, 0.1f);
        }
        if (rightHandOnFire)
        {
            rightHand.SendHapticImpulse(0, 0.5f, 0.1f);
        }
    }

    public void PrintObject(HoverEnterEventArgs arg)
    {
        print("Looking at " + arg.interactableObject.transform.name);
    }

    void HandGlow()
    {
        if (rightHandT.position.y > (Camera.main.transform.position.y + HandHeight))
        {
            if (!handglow.isPlaying)
            {
                handglow.Play();
            }
        }
        else if (handglow.isPlaying)
        {
            handglow.Stop();
        }

        if (leftHandT.position.y > (Camera.main.transform.position.y + HandHeight))
        {
            if (!lefthandglow.isPlaying)
            {
                lefthandglow.Play();
            }
        }
        else if (lefthandglow.isPlaying)
        {
            lefthandglow.Stop();
        }
    }
}
