using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using EasyTextEffects;

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
    TextEffect effect;

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
    float tipTimer;
    internal int progressedRecently;


    Vector3 lookPos;

    void Start()
    {
        dialogueCanvas.SetActive(false);
        player = Camera.main.transform;
        effect = dialogueText.GetComponent<TextEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();

        PlayDialogue();

        TipTimer();
    }

    void StartDialogue(string text)
    {
        if (!playingDialogue)
        {
            queueFinished = false;
            playingDialogue = true;
            queuedDialogue = text;
            i = 0;
            len = queuedDialogue.Length;
            dialogueText.text = text;
            effect.Refresh();
            dialogueCanvas.SetActive(true);
            effect.StartManualEffect("typewriter");
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
                    //print("Playing Dialogue");
                    //dialogueText.text += queuedDialogue[i];
                    i++;
                    AudioManager.instance.PlayAudio("SquirrelTalk");
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

    public void QueueFinished()
    {
        queueFinished = true;
    }

    void StopDialogue()
    {
        dialogueText.text = "";
        playingDialogue = false;
        dialogueCanvas.SetActive(false);
    }

    void LookAtPlayer()
    {
        lookPos = player.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
        Head.transform.LookAt(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Triggered");
        if (other.CompareTag("Left Controller") || other.CompareTag("Right Controller"))
        {
            GetHint();
        }

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


    void TipTimer()
    {
        tipTimer += Time.deltaTime;

        if(tipTimer >= 30)
        {
            if(progressedRecently > 0)
            {
                progressedRecently--;
                tipTimer = 0;
            }
            else
            {
                GetDialogue("Reminder");
            }
        }
    }
}
