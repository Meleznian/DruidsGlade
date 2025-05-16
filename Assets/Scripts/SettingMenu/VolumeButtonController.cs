using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class VolumeButtonController : MonoBehaviour
{
    [Header("TextMeshPro UI Display")]
    public TextMeshProUGUI volumeText;

    [Header("Volume setting")]
    public float volumeStep = 0.1f;  // Range of increase/decrease
    public float minVolume = 0f;
    public float maxVolume = 1f;

    void Start()
    {
        UpdateVolumeText();
    }

    // Increase the volume (called from the + button)
    public void IncreaseVolume()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume + volumeStep, minVolume, maxVolume);
        UpdateVolumeText();
    }

    // Decrease volume (call from the - button)
    public void DecreaseVolume()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume - volumeStep, minVolume, maxVolume);
        UpdateVolumeText();
    }

    // Update display text
    private void UpdateVolumeText()
    {
        volumeText.text = "Volume: " + (AudioListener.volume * 100f).ToString("F0") + "%";
    }
}

