using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    public static LightChanger instance = null;

    Light lighting;
    internal Color defaultColour;
    float goalIntensity;
    Color goalColour;
    float t;
    bool changing;
    public float speed;
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

    private void Start()
    {
        lighting = GetComponent<Light>();
        defaultColour = lighting.color;
    }

    private void Update()
    {
        if (changing)
        {
            DoChange();
        }
    }

    public void ChangeLight(Color colour, float intensity)
    {
        goalColour = colour;
        goalIntensity = intensity;
        changing = true;
    }

    void DoChange()
    {
        lighting.color = Color.Lerp(lighting.color, goalColour, t);
        lighting.intensity = Mathf.Lerp(lighting.intensity, goalIntensity, t);

        if(t < 1)
        {
            t += (speed*Time.deltaTime);
        }
        else
        {
            changing = false;
            t = 0;
        }
    }
}
