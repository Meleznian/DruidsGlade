using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class ShowCanvasOnLook : MonoBehaviour
{
    private Canvas wristCanvas;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float requiredSimilarity = 0.5f;

void OnEnable()
    {
        wristCanvas = GetComponent<Canvas>();
    }
    void Update()
    {
        float dotProduct = Vector3.Dot(Camera.main.transform.forward, transform.forward);
        wristCanvas.enabled = (dotProduct > requiredSimilarity);
    }
}