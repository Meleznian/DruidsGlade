using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthRitual : MonoBehaviour
{

    public float decayTime;
    public float growthMult;
    public float sizeGoal;
    float timer;

    bool fullSize;
    bool begin;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingMenu.instance.disableMS == true)
        {
            Destroy(gameObject);
        }

        if (begin)
        {
            if (!fullSize)
            {
                player.transform.localScale += new Vector3(Time.deltaTime * growthMult, Time.deltaTime * growthMult, Time.deltaTime * growthMult);

                if (player.transform.localScale.x > sizeGoal)
                {
                    fullSize = true;
                }
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= decayTime)
                {
                    player.transform.localScale -= new Vector3(Time.deltaTime * growthMult, Time.deltaTime * growthMult, Time.deltaTime * growthMult);

                    if (player.transform.localScale.x <= 1f)
                    {
                        player.transform.localScale = Vector3.one;

                        Destroy(gameObject);
                    }
                }
            }
        }
    }


    public void Begin()
    {
        begin = true;
    }
}
