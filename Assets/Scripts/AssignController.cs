using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HandManager.instance.CheckHands();
    }
}
