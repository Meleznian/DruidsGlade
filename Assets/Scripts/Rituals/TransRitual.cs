using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransRitual : MonoBehaviour
{
    GameObject player;
    [SerializeField] ParticleSystem sparkle;
    [SerializeField] ParticleSystem vortex;
    [SerializeField] ScreenFader fader;

    public float riseSpeed;
    public float riseTime;
    float timer;
    bool transing;

   
    // Start is called before the first frame update
    void Start()
    {
        fader = GameObject.Find("ScreenFader GameObject").GetComponent<ScreenFader>();
        player = GameObject.Find("Player");
        sparkle = GameObject.Find("GreenSparkle").GetComponent<ParticleSystem>();      
        vortex = sparkle.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (transing)
        {
            if (SettingMenu.instance.disableMS == false)
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (riseSpeed * Time.deltaTime), player.transform.position.z);
            }
            timer += Time.deltaTime;

            if (timer > riseTime)
            {
                fader.FadeAndLoadScene("TheEnd");
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    public void Transcend()
    {
        sparkle.Play();
        vortex.Play();
        player.GetComponent<CharacterController>().enabled = false;
        transing = true;
    }
}
