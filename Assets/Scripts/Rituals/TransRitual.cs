using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransRitual : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem sparkle;
    public float riseSpeed;
    bool transing;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sparkle = GameObject.Find("GreenSparkle").GetComponent<ParticleSystem>();       
    }

    private void Update()
    {
        if (transing)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (riseSpeed*Time.deltaTime), player.transform.position.z);
        }
    }

    // Update is called once per frame
    public void Transcend()
    {
        sparkle.Play();
        player.GetComponent<CharacterController>().enabled = false;
        transing = true;
    }
}
