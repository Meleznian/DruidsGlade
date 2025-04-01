using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Search;
using System;
public class Squirrel : MonoBehaviour
{

    public static Squirrel instance = null;
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

    public GameObject dialogueCanvas;
    public TMP_Text dialogueText;
    public GameObject Head;

    Transform player;

    bool playingDialogue;
    bool queueFinished;
    string queuedDialogue;

    public float dialogueDecay;
    public float dialogueSpeed;
    float timer;

    int i;
    int len;

    [Serializable]
    public class Dialogue
    {
        public string refID;
        public string[] dialogueOptions;

        [Header("Options")]
        public bool sequence;
        public bool autoContinue;
        public bool loop;
        internal bool played;

        internal int index;

    }

    bool ac;
    string mostRecentDialogue;
    public List<Dialogue> dialogueList = new();
    bool welcomed;


    Vector3 lookPos;

    void Start()
    {
        dialogueCanvas.SetActive(false);
        player = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();

        PlayDialogue();
    }

    void StartDialogue(string text)
    {
        if (!playingDialogue)
        {
            dialogueCanvas.SetActive(true);
            queueFinished = false;
            playingDialogue = true;
            queuedDialogue = text;
            i = 0;
            len = queuedDialogue.Length;
        }
    }

    void PlayDialogue()
    {
        if(playingDialogue)
        {
            timer += Time.deltaTime;

            if (timer > dialogueSpeed && !queueFinished)
            {
                if (i < len)
                {
                    dialogueText.text += queuedDialogue[i];
                    i++;
                    AudioManager.instance.PlayAudio("SquirrelTalk");
                }
                else
                {
                    queueFinished = true;
                }

                timer = 0f;
            }
            else if(queueFinished && timer >= dialogueDecay)
            {
                if (ac)
                {
                    dialogueText.text = "";
                    playingDialogue = false;
                    GetDialogue(mostRecentDialogue);
                }
                else
                {
                    StopDialogue();
                }
            }
        }
    }

    void StopDialogue()
    {
        dialogueText.text = "";
        playingDialogue = false;
        dialogueCanvas.SetActive(false);
    }

    void LookAtPlayer()
    {
        lookPos = player.position - transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Left Controller") || other.CompareTag("Right Controller"))
        //{
        //    GetHint();
        //}

        if (other.CompareTag("Water"))
        {
            GetDialogue("Wet");
        }

        if (other.CompareTag("Fire"))
        {
            GetDialogue("OnFire");
        }

    }


    public void GetDialogue(string RefID)
    {
        if (!playingDialogue)
        {
            foreach (Dialogue d in dialogueList)
            {
                if (d.refID == RefID)
                {
                    int r;
                    ac = d.autoContinue;
                    mostRecentDialogue = d.refID;

                    if(!d.loop && d.played)
                    {
                        break;
                    }

                    if (d.sequence == false)
                    {
                        r = UnityEngine.Random.Range(0, d.dialogueOptions.Length);
                    }
                    else
                    {
                        r = d.index;

                        d.index++;

                        if (d.index >= d.dialogueOptions.Length)
                        {
                            d.index = 0;
                            ac = false;
                            d.played = true;
                        }
                    }

                    StartDialogue(d.dialogueOptions[r]);
                }
            }
        }
    }

    public void GetHint()
    {
        int i = RitualController.instance.nextHint;

        if (i != -1)
        {
            GetDialogue(RitualController.instance.ritualList[i].refID);
        }
        else
        {
            GetDialogue("Ambient");
        }
    }
}
