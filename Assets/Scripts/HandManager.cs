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
    public InputActionReference RightA;




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
        SummonStaff();
    }

    void SummonStaff()
    {
        if (rightHandT.position.y > (Camera.main.transform.position.y + 0.3f) && RightGrip.action.ReadValue<float>() == 1 && !rightInteractor.hasSelection)
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
}
