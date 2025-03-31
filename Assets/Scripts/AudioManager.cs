using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Audio
    {
        [Tooltip("ID used to locate audio")]
        public string refID;
        [Tooltip("If more then one audio clip is added, one will be selected base on the random bool")]
        public AudioClip[] clips;

        [Header("Sound Mixing")]
        [Range(0f, 1f)]
        public float volume;

        [Tooltip("Note: Pitch cannot be used with PlayAtLocation()")]
        [Range(.1f, 3f)]
        public float pitch;

        [Tooltip("Is the audio play randomly or in sequence")]
        public bool random;
        [Tooltip("Does the Audio Loop")]
        public bool loop;

        [Header ("Optional")]
        [Tooltip("If no source is added one will be created")]
        public AudioSource source;

        internal int index;
    }

    public Audio[] audioList;

    Audio debugAudio;

    public static AudioManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }

        foreach (Audio s in audioList)
        {
            if (s.source == null)
            {
                s.source = gameObject.AddComponent<AudioSource>();
            }

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        debugAudio = audioList[0];

        PlayAudio("Ambient");
    }


    //Finds the audio entry based on the given ID and returns the AudioSource
    Audio GetAudio(string ID)
    {
        foreach(Audio s in audioList)
        {
            if(s.refID == ID)
            {
                if (s.random)
                {
                    s.index = UnityEngine.Random.Range(0, s.clips.Length);
                }

                s.source.clip = s.clips[s.index];

                if(!s.random)
                {
                    s.index++;

                    if(s.index >= s.clips.Length)
                    {
                        s.index = 0;
                    }
                }

                return s;
            }
        }

        Debug.LogWarning("Sound of ID " +  ID + " not found");
        return debugAudio;
    }

    public void PlayAudio(string ID)
    {
        Audio audio = GetAudio(ID);
        audio.source.Play();
    }

    public void PauseAudio(string ID)
    {
        Audio audio = GetAudio(ID);
        audio.source.Pause();
    }

    public void StopAudio(string ID)
    {
        Audio audio = GetAudio(ID);
        audio.source.Stop();
    }

    //Plays audio at specific location seperate from audio source. Cannot be stopped or paused
    public void PlayAtLocation(string ID, Vector3 location)
    {
        Audio audio = GetAudio(ID);
        AudioSource.PlayClipAtPoint(audio.clips[audio.index], location, audio.volume);
    }

    //Plays audio independantly of source, can be played without interrupting currently playing audio, Cannot be stopped or paused
    public void PlayIndependantly(string ID)
    {
        Audio audio = GetAudio(ID);
        audio.source.PlayOneShot(audio.clips[audio.index]);
    }

    //Plays audio if AudioSource is not currently playing anything
    public void PlaySoundIfNot(string ID)
    {
        Audio audio = GetAudio(ID);

        if (!audio.source.isPlaying)
        {
            audio.source.Play();
        }
    }
}
