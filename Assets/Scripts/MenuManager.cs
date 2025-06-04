using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string levelName;
    public void StartGame()
    {
        SFXManager.instance.Pitch(0);
        SceneManager.LoadScene(levelName);
        
    }
    public void QuitGame()
    {
        SFXManager.instance.Pitch(0);
        Application.Quit();
    }
}
