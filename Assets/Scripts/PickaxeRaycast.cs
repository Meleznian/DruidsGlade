using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickaxeRaycast : MonoBehaviour
{
    public float hitDistance = 0.5f;
    public LayerMask rockLayer;
    public string rockTag = "Rock"; 
    public GameObject stonePrefab;
    public Transform dropPoint;
    public float dropCooldown = 0.5f;

    private float lastDropTime = -999f;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void Update()
    {
        if (!grab.isSelected) return; 

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, hitDistance, rockLayer))
        {
            if (hit.collider.CompareTag(rockTag))
            {
                if (Time.time - lastDropTime >= dropCooldown)
                {
                    DropStone(hit.point);
                    lastDropTime = Time.time;
                }
            }
        }
    }

    private void DropStone(Vector3 nearPosition)
    {
        Vector3 spawnPos = (dropPoint != null) ? dropPoint.position : nearPosition + Vector3.up * 0.5f;
        Instantiate(stonePrefab, spawnPos, Quaternion.identity);
        Debug.Log("Ray hit rock! Dropping stone.");
    }
}
