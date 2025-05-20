
using UnityEngine;
public class ResetTransform : MonoBehaviour
{
    void Start()
    {
        VRUtilities.MatchTransform(transform);
    }
}

