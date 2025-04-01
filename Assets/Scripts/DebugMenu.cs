using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExamplePanelLogic : MonoBehaviour
{
    public Transform cameraOffset;
    public float moveAmount;
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ActivateAllSpawners()
    {
        MasterSpawner.instance.ActivateAllSpawners();
    }

    public void ToggleSquirrel()
    {
        Squirrel.instance.GetDialogue("Ambient");
    }

    public void TestAudio()
    {
        AudioManager.instance.PlayAudio("Debug");
    }

    public void CameraUp()
    {
        cameraOffset.position = new Vector3(cameraOffset.position.x, cameraOffset.position.y + moveAmount, cameraOffset.position.z);
    }

    public void CameraDown()
    {
        cameraOffset.position = new Vector3(cameraOffset.position.x, cameraOffset.position.y - moveAmount, cameraOffset.position.z);
    }

}