using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "3D Tour")
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        } 
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ChangeScene("Menu");
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://fondazionecesarepavese.it/cesare-pavese-vita-opere/");
    }
}
