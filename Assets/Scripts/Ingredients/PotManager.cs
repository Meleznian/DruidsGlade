using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotManager : MonoBehaviour
{
    public static PotManager instance = null;
    public Ingredient pot;

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

    public List<GameObject> potList = new();


    private void Update()
    {

    }

    public void RemovePot(GameObject p, bool isPR)
    {
        potList.Remove(p);

        if (potList.Count == 0 && isPR == false)
        {
            SpawnPot();
        }
    }

    void SpawnPot()
    {
        IngredientScript newPot = Instantiate(pot.prefab, transform.position, Quaternion.identity).GetComponent<IngredientScript>();
        newPot.fragile = false;
    }
}
