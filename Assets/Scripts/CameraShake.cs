using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    bool shaking;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (shaking)
        {
            Shake();
        }
    }

    void Shake()
    {
        float randomX = Random.value - 0.5f;
        float randomY = Random.value - 0.5f;
        float randomZ = Random.value - 0.5f;

        transform.localEulerAngles = new Vector3(randomX, randomY, randomZ);
    }

    public void ShakeOn()
    {
        shaking = true;
    }
    public void ShakeOff()
    {
        shaking= false;
    }
}
