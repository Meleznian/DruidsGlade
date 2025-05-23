using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingMenuToggle : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject settingsMenu;
    public Transform cameraTransform;

    [Header("Input Settings")]
    public InputActionReference toggleAction;

    private void Update()
    {
        if (toggleAction.action.triggered)
        {
            ToggleSettingsMenu();
        }
    }


    //private void OnEnable()
    //{
    //    if (toggleAction != null)
    //        toggleAction.action.performed += ToggleSettingsMenu;
    //}
    //
    //private void OnDisable()
    //{
    //    if (toggleAction != null)
    //        toggleAction.action.performed -= ToggleSettingsMenu;
    //}

    private void ToggleSettingsMenu()
    {
        if (settingsMenu == null || cameraTransform == null) return;

        bool isActive = settingsMenu.activeSelf;
        settingsMenu.SetActive(!isActive);

        if (!isActive)
        {
            PositionMenuInView();
        }
    }

    private void PositionMenuInView()
    {
        Vector3 forward = cameraTransform.forward.normalized;

        // 0.8m ahead in the player's line of sight (including up and down)
        Vector3 newPosition = cameraTransform.position + forward * 0.8f;

        settingsMenu.transform.position = newPosition;

        // Rotate to face the direction of gaze
        settingsMenu.transform.rotation = Quaternion.LookRotation(forward);
    }
}
