using UnityEngine;

public class CurveMotionLoopLocal : MonoBehaviour
{
    public float distance = 5f;       
    public float curveHeight = 2f;   
    public float duration = 2f;     

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

        Vector3 localForward = transform.right.normalized; 
        Vector3 move = localForward * (t * distance);

 
        float arc = Mathf.Sin(t * Mathf.PI) * curveHeight;
        move.y += arc;

        transform.position = startPos + move;
    }
}
