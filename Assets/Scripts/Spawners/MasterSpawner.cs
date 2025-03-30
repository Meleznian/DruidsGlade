using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    public static MasterSpawner instance = null;
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

    [Serializable]
    public class Spawner 
    {
        public string spawnerName;
        public SpawnerBase spawner;
    }

    public List<Spawner> spawners = new();
    
    public void ActivateSpawner(string name)
    {
        foreach (Spawner s in spawners)
        {
            if(s.spawnerName == name)
            {
                s.spawner.ActivateSpawner();
                break;
            }
        }
    }

    public void ActivateAllSpawners()
    {
        foreach(Spawner s in spawners)
        {
            s.spawner.ActivateSpawner();
        }
    }

    public void DeactivateAllSpawners()
    {
        foreach (Spawner s in spawners)
        {
            s.spawner.DeactivateSpawner();
        }
    }
}
