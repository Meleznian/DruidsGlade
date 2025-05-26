using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveMagic : MonoBehaviour
{
    public float startDelay = 1;
    public float speed;
    float timer;
    bool banded;
    public bool smallOrb;

    Rigidbody rb;

    public string goalName;
    public Vector3 goal;
    public GameObject burstPrefab;

    Transform t;

    public UnityEvent GoalEffect;

    private void Start()
    {
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        if(goalName != "")
        {
            goal = GameObject.Find(goalName).transform.position;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (timer >= startDelay)
        {
            if (!banded)
            {
                rb.AddForce((-(goal - t.position)) * speed);

                if (timer >= 0.3)
                {
                    banded = true;
                }
            }
            else
            {
                rb.AddForce((goal - t.position) * speed);

                if (Vector3.Distance(goal, t.position) <= 0.1f)
                {
                    Instantiate(burstPrefab, transform.position, Quaternion.Euler(Vector3.zero));

                    if (GoalEffect != null)
                    {
                        GoalEffect.Invoke();
                    }

                    if (smallOrb)
                    {
                        RitualController.instance.orbs -= 1;
                        AudioManager.instance.PlayAtLocation("SoftThud",transform.position);
                    }

                    Destroy(gameObject);
                }
            }
        }



        timer += Time.deltaTime;
    }
}
