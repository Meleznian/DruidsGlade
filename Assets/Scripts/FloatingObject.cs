using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatAmplitude = 0.5f;
    public float floatSpeed = 1f;

    private Vector3 startPos;

    public ParticleSystem floatingParticles;

    void Start()
    {
        startPos = transform.position;

        if (floatingParticles != null && !floatingParticles.isPlaying)
            floatingParticles.Play();
    }



    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
