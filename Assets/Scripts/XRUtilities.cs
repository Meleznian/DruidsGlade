using Unity.XR.CoreUtils;
using UnityEngine;
public class VRUtilities
{
    public static void MatchTransform(Transform targetTransform, Transform player = null,
    bool ignorePlayerHeight = true, bool attachPlayerAsChild = false)
    {
        // Cache local values.
        Camera cam = Camera.main;
        XROrigin xrOrigin = GameObject.FindFirstObjectByType<XROrigin>();
        if (xrOrigin == null) return; // No XROrigin in the scene.
        if (player == null) player = xrOrigin.transform;
        player.localPosition = Vector3.zero;
    
        // Align player rotation with the target
        float targetYAngle = Quaternion.LookRotation(targetTransform.forward, Vector3.up).eulerAngles.y;
        float currentYAngle = cam.transform.rotation.eulerAngles.y;
        float yRotationDifference = targetYAngle - currentYAngle;
        xrOrigin.transform.RotateAround(cam.transform.position, Vector3.up, yRotationDifference);
    
        // Reposition the origin to align with the new transform
        Vector3 newOrigin = xrOrigin.transform.position - cam.transform.position;
        if (!ignorePlayerHeight) newOrigin.y = 0;
        xrOrigin.transform.position = newOrigin + targetTransform.position;
    
        // Attach or detach the player to/from the new transform
        player.SetParent(attachPlayerAsChild ? targetTransform : null);
    }
}

