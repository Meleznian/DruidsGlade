using UnityEngine;
using UnityEngine.SceneManagement;
public class ExamplePanelLogic : MonoBehaviour
{
    private Light sun;
    void OnEnable()
    {
        sun = FindFirstObjectByType<Light>();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChangeLightIntensity(float intensity)
    {
        sun.intensity = intensity;
    }
}