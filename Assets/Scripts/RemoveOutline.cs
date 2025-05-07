using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOutline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Outline>() != null)
        {
            GetComponent<Outline>().OutlineWidth = 0;
            this.enabled = false;
        }
    }
}
