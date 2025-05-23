using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHitDropper : SpawnerBase
{
    [Header("Drop Settings")]
    public GameObject stonePrefab;
    public GameObject pickaxePrefab;
    public GameObject SpawnEffect;
    public GameObject Sparks;
    public Transform spawnPoint;

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
        AudioManager.instance.PlayAudio("MineStone");
        DropStone();
        lastDropTime = Time.time;
    }

    private void DropStone()
    {
        Vector3 spawnPos = (dropPoint != null) ? dropPoint.position : transform.position + Vector3.up * 0.7f;

        if (active)
        {
            Instantiate(stonePrefab, spawnPos, Quaternion.identity);
            Instantiate(Sparks, spawnPos, Quaternion.identity);
        }

        Debug.Log("Rock hit! Dropping a stone.");
    }

    public override void ActivateSpawner()
    {
        active = true;
        GetComponent<Animator>().SetTrigger("Emerge");
    }

    public void SpawnPickaxe()
    {
        Instantiate(SpawnEffect, spawnPoint.position, spawnPoint.rotation);
        GameObject pickaxe = Instantiate(pickaxePrefab, spawnPoint.position, spawnPoint.rotation);
        pickaxe.GetComponent<Rigidbody>().AddTorque(Vector3.left, ForceMode.Impulse);
        pickaxe.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
        //dropPoint = pickaxe.transform.Find("DropPoint").transform;
        Squirrel.instance.GetDialogue("Pickaxe");
    }
}
