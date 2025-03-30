using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrowSpawner : SpawnerBase
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform growPoint;
        public GameObject attachedIngredient;
        public int pointID;
        public float timer;
    }

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    public float growTime;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(SpawnPoint s in spawnPoints)
        {
            s.pointID = i;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            RegrowLogic();
        }
    }


    void RegrowLogic()
    {
        foreach(SpawnPoint s in spawnPoints)
        {
            if(s.attachedIngredient == null)
            {
                s.timer += Time.deltaTime;

                if (s.timer >= growTime)
                {
                    s.attachedIngredient = Instantiate(ingredient.prefab, s.growPoint.position, s.growPoint.rotation, ingredientHolder);
                    PrepGrowable(s.attachedIngredient, s.pointID);
                    s.timer = 0;
                }
            }
        }
    }

    void PrepGrowable(GameObject Growable, int GrowableRef)
    {
        Growable.GetComponent<Rigidbody>().isKinematic = true;
        Growable GrowableScript = Growable.GetComponent<Growable>();

        GrowableScript.bushReference = GrowableRef;
        GrowableScript.spawner = this;
        GrowableScript.attachedToSpawner = true;
    }


    public void RemoveGrowable(int GrowableRef)
    {
        foreach(SpawnPoint s in spawnPoints)
        {
            if(GrowableRef == s.pointID)
            {
                s.attachedIngredient = null;
                break;
            }
        }
    }
}
