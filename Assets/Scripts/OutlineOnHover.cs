
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

[RequireComponent(typeof(XRBaseInteractable))]
public class OutlineOnHover : MonoBehaviour
{
    private Outline outline;
    private XRBaseInteractable interactable;
    private Transform playerTransform;
    private bool isHovered = false;
    private Color grabDistanceColor = Color.white;
    private Color hoverColor = Color.green;
    private const float OUTLINE_WIDTH = 2.0f;
    private const float GRAB_LINE_SHOW_DISTANCE = 5.0f;
    void Awake()
    {
        // Try to get the Outline component; if it's not there, add it.
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
        outline.OutlineWidth = OUTLINE_WIDTH;
        // Start with the outline disabled.
        outline.enabled = false;
        // Get the XR interactable component and setup event listeners.
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEntered);
            interactable.hoverExited.AddListener(OnHoverExited);
        }


    // Find the player transform for distance calculations.
XROrigin origin = FindFirstObjectByType<XROrigin>();
        if (origin != null)
        {
            playerTransform = origin.transform;
        }
    }
    void Update()
    {
        // If the player doesn't exist, don't show outlines.
        if (playerTransform == null) return;
        // Find the distance from the player to the grabbable.
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (isHovered)
        {
            outline.OutlineColor = hoverColor;
            outline.enabled = true;
        }
        else if (distance <= GRAB_LINE_SHOW_DISTANCE && !interactable.isSelected)
        {
            outline.OutlineColor = grabDistanceColor;
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
    // Called when an XR controller hovers over the object.
    void OnHoverEntered(HoverEnterEventArgs args)
    {
        isHovered = true;
    }
    // Called when the XR controller stops hovering.
    void OnHoverExited(HoverExitEventArgs args)
    {
        isHovered = false;
    }
    void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverEntered);
            interactable.hoverExited.RemoveListener(OnHoverExited);
        }
    }
}