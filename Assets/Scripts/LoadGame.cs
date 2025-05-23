using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadGame : MonoBehaviour
{
    public string scene;
    public void LoadMenuScene()
    {
        GameObject.Find("ScreenFader GameObject").GetComponent<ScreenFader>().FadeAndLoadScene(scene);
    }

}
