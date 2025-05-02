using UnityEngine;

public class CurveMotionLoopLocal : MonoBehaviour
{
    public float distance = 5f;        // Total distance to fly (in local forward)
    public float curveHeight = 2f;     // Height of the arc
    public float duration = 2f;        // Time for one curve

    private Vector3 startPos;
    private float t = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        t += Time.deltaTime / duration;

        if (t > 1f)
        {
            t = 0f;
            transform.position = startPos;
        }

        // Move forward in local space
        Vector3 localForward = transform.right.normalized; // change to forward if needed
        Vector3 move = localForward * (t * distance);

        // Apply vertical curve
        float arc = Mathf.Sin(t * Mathf.PI) * curveHeight;
        move.y += arc;

        // Final position relative to start
        transform.position = startPos + move;
    }
}
