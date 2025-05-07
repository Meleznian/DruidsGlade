using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecipeBookDisplay : MonoBehaviour
{
    public GameObject recipeCanvas;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        recipeCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        recipeCanvas.SetActive(true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        recipeCanvas.SetActive(false);
    }
}
