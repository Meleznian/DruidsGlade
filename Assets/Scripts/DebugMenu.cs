using UnityEngine;
using UnityEngine.SceneManagement;
public class DebugMenu : MonoBehaviour
{
    public Transform cameraOffset;
    public Transform originPosition;
    Transform player;

    public float moveAmount;
    bool pressed;
    bool up;

    private void Start()
    {
        player = cameraOffset.parent;
    }

    private void Update()
    {
        UpdateCamera();
    }

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

    public void UpPressed()
    {
        pressed = true;
        up = true;
    }

    public void DownPressed()
    {
        pressed = true;
        up = false;
    }

    public void UpLeft()
    {
        pressed = false;
    }

    public void DownLeft()
    {
        pressed = false;
    }


    void UpdateCamera()
    {
        if (pressed)
        {
            if (up)
            {
                cameraOffset.position = new Vector3(cameraOffset.position.x, cameraOffset.position.y + Time.deltaTime, cameraOffset.position.z);
            }
            else
            {
                cameraOffset.position = new Vector3(cameraOffset.position.x, cameraOffset.position.y - Time.deltaTime, cameraOffset.position.z);
            }
        }
    }

    public void ResetPosition()
    {
        player.position = new Vector3(originPosition.position.x, 0, originPosition.position.z);
    }

}