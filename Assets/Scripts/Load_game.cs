using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_game : MonoBehaviour
{
    void LoadStart()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
