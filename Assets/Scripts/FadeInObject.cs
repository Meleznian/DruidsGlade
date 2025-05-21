using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeInObject : MonoBehaviour
{
    Material r;

    public Color startColour;
    public Color goalColour;
    float t;
    public float speed;
    bool changed;
    bool started;
    bool fadeOut;

    public UnityEvent fadeInFunction;
    public UnityEvent fadeOutFunction;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<MeshRenderer>().material;
        r.color = startColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (started && !changed)
        {
            if (!fadeOut)
            {
                r.color = Color.Lerp(r.color, goalColour, t);

                if (t < 1)
                {
                    t += (speed * Time.deltaTime);
                }
                else
                {
                    changed = true;
                    t = 0;

                    if (fadeInFunction != null)
                    {
                        fadeInFunction.Invoke();
                    }
                }
            }
            else
            {
                r.color = Color.Lerp(r.color, startColour, t);

                if (t < 1)
                {
                    t += (speed * Time.deltaTime);
                }
                else
                {
                    changed = true;
                    t = 0;

                    if (fadeOutFunction != null)
                    {
                        fadeOutFunction.Invoke();
                    }
                }
            }
        }

    }

    public void FadeIn()
    {
        started = true;
        changed = false;
    }

    public void FadeOut()
    {
        started = true;
        changed = false;
        fadeOut = true;
    }
}