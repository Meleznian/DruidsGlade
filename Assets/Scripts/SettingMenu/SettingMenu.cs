using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer; 
    public Slider volumeSlider;
    public Toggle muteToggle;

    private float currentVolume = 0.75f; // Temporary record of volume (to return when unmuted)

    private void Start()
    {
        // Load saved volume (0.75 if not present)
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        currentVolume = savedVolume;

        // Slider setting
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // Toggle setting
        if (muteToggle != null)
        {
            muteToggle.onValueChanged.AddListener(SetMute);
        }
    }

    public void SetVolume(float volume)
    {
        currentVolume = volume;

        // Toggle setting not change volume if it is on mute (reflected when toggle is released).
        if (muteToggle != null && muteToggle.isOn)
            return;

        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat("Master", dB);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetMute(bool isMuted)
    {
        if (isMuted)
        {
            // When muted: volume set to -80dB (virtually silent)
            audioMixer.SetFloat("Master", -80f);
        }
        else
        {
            // When unmuted: Restore original volume
            SetVolume(currentVolume);
        }
    }
}
