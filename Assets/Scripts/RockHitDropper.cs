using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHitDropper : MonoBehaviour
{
    [Header("Drop Settings")]
    public GameObject stonePrefab;
    public Transform dropPoint; // Option: Empty is OK
    public string pickaxeTag = "Pickaxe";

    public float dropCooldown = 0.5f; // Minimum Interval
    private float lastDropTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger hit by: {other.gameObject.name} with tag {other.tag}");

        // Ignore everything but the nickel.
        if (!other.CompareTag(pickaxeTag)) return;

        // Prevent consecutive hits within a certain interval
        if (Time.time - lastDropTime < dropCooldown) return;

        DropStone();
        lastDropTime = Time.time;
    }

    private void DropStone()
    {
        Vector3 spawnPos = (dropPoint != null) ? dropPoint.position : transform.position + Vector3.up * 0.7f;

        Instantiate(stonePrefab, spawnPos, Quaternion.identity);

        Debug.Log("Rock hit! Dropping a stone.");
    }
}
