using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Pot : MonoBehaviour
{
    [HideInInspector] public XRGrabInteractable grabbable;
    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public Vector3 velocity;
    [SerializeField] private float forceToBreak = 1.0f;
    [SerializeField] private GameObject potSmashEffect;
    private Vector3 lastPosition;
    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        UpdateVelocity();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Complete XR Origin Set Up Variant")
        {



            if (other.gameObject)
                if (velocity.magnitude < forceToBreak) return;

            // Trigger the Controller Haptic Feedback
            InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            leftController.SendHapticImpulse(0, 1.0f, 0.1f);
            rightController.SendHapticImpulse(0, 1.0f, 0.1f);
            // Trigger the particle effects.
            Instantiate(potSmashEffect, transform.position, Quaternion.identity);
            // Smash the pots.
            Destroy(gameObject);
            if (other.gameObject.GetComponent<Pot>() != null)
            {
                Destroy(other.gameObject);
                Instantiate(potSmashEffect, other.gameObject.transform.position, Quaternion.identity);
            }
        }
    }
    private void UpdateVelocity()
    {
        Vector3 currentPosition = transform.position;
        velocity = (currentPosition - lastPosition) / Time.fixedDeltaTime;
        lastPosition = currentPosition;
    }
}