using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookFlipOnGrab : MonoBehaviour
{
    public Animator animator;

    private void OnEnable()
    {
        var grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        animator.SetTrigger("FlipPage");
    }
}
