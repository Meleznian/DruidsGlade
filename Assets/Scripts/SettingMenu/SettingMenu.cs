using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEditor.XR.LegacyInputHelpers;

public class SettingMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Toggle muteToggle;
    private float currentVolume = 0.75f;

    [Header("Height Settings")]
    public Slider heightSlider;
    public Transform cameraOffset;
    //public CharacterController characterController;
    //public XROrigin xrOrigin;
    //public float minHeight = 1.1f;
    //public float maxHeight = 1.8f;

    [Header("Snap/Smooth Turn")]
    //public Toggle snapTurnToggle;
    //public Toggle smoothTurnToggle;

    public ContinuousTurnProvider continuousTurn;
    public SnapTurnProvider snapTurn;


    private bool isChangingToggleInternally = false;

    bool snap;

    void Start()
    {
        // volume initialization
        currentVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        if (volumeSlider != null)
        {
            volumeSlider.value = currentVolume;
            SetVolume(currentVolume);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (muteToggle != null)
        {
            muteToggle.isOn = false;
            muteToggle.onValueChanged.AddListener(SetMute);
        }

        // stature initialization
        //if (heightSlider != null && characterController != null)
        //{
        //    heightSlider.minValue = 0f;
        //    heightSlider.maxValue = 1f;
        //
        //    float normalized = Mathf.InverseLerp(minHeight, maxHeight, characterController.height);
        //    heightSlider.value = normalized;
        //    heightSlider.onValueChanged.AddListener(SetPlayerHeight);
        //}

        // Toggle Event Registration
        //if (snapTurnToggle != null)
        //    snapTurnToggle.onValueChanged.AddListener(OnSnapToggleChanged);
        //if (smoothTurnToggle != null)
        //    smoothTurnToggle.onValueChanged.AddListener(OnSmoothToggleChanged);

        // Initial state: Smooth ON
        //SetTurnObjects(snap: false, smooth: true);
        //SetToggleStates(snap: false, smooth: true);
    }

    public void SetVolume(float value)
    {
        currentVolume = value;
        if (muteToggle != null && muteToggle.isOn)
            return;

        float dB = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20;
        audioMixer.SetFloat("Master", dB);
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    public void SetMute(bool isMuted)
    {
        if (isMuted)
            audioMixer.SetFloat("Master", -80f);
        else
            SetVolume(currentVolume);
    }

    //public void SetPlayerHeight(float normalizedValue)
    //{
    //    float newHeight = Mathf.Lerp(minHeight, maxHeight, normalizedValue);
    //    if (characterController != null)
    //    {
    //        characterController.height = newHeight;
    //        characterController.center = new Vector3(0, newHeight / 2f, 0);
    //    }
    //
    //    if (xrOrigin != null)
    //    {
    //        xrOrigin.CameraYOffset = newHeight - 0.1f;
    //    }
    //}

    //private void OnSnapToggleChanged(bool isOn)
    //{
    //    if (isChangingToggleInternally) return;
    //
    //    if (isOn)
    //    {
    //        SetTurnObjects(snap: true, smooth: false);
    //        SetToggleStates(snap: true, smooth: false);
    //    }
    //    else if (!smoothTurnToggle.isOn)
    //    {
    //        RestoreToggle(snapTurnToggle);
    //    }
    //}

    //private void OnSmoothToggleChanged(bool isOn)
    //{
    //    if (isChangingToggleInternally) return;
    //
    //    if (isOn)
    //    {
    //        SetTurnObjects(snap: false, smooth: true);
    //        SetToggleStates(snap: false, smooth: true);
    //    }
    //    else if (!snapTurnToggle.isOn)
    //    {
    //        RestoreToggle(smoothTurnToggle);
    //    }
    //}

    //private void SetTurnObjects(bool snap, bool smooth)
    //{
    //    if (turn1SnapObject != null) turn1SnapObject.SetActive(snap);
    //    if (turn2SmoothObject != null) turn2SmoothObject.SetActive(smooth);
    //}

    //private void SetToggleStates(bool snap, bool smooth)
    //{
    //    isChangingToggleInternally = true;
    //
    //    if (snapTurnToggle != null) snapTurnToggle.isOn = snap;
    //    if (smoothTurnToggle != null) smoothTurnToggle.isOn = smooth;
    //
    //    isChangingToggleInternally = false;
    //}

    //private void RestoreToggle(Toggle toggle)
    //{
    //    isChangingToggleInternally = true;
    //    toggle.isOn = true;
    //    isChangingToggleInternally = false;
    //}

    public void SwitchToggle()
    {
        snap = !snap;

        if(snap == true)
        {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        }
        else
        {
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        }
    }
    public void UpdateHeight()
    {
        cameraOffset.position = new Vector3(cameraOffset.position.x, heightSlider.value, cameraOffset.position.z);
    }
}
