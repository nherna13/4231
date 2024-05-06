using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;
    string newGameScene = "SampleScene";
    string controlScene = "Control";

    string mainMenu = "MainMenu";
    


    void Start()
    {
        int highScore = SaveLoadManager.Instance.LoadHighScore();
        highScoreUI.text = $"Top Wave Survived: {highScore}";
    }


    public void StartNewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

     public void StartControl()
    {
        SceneManager.LoadScene(controlScene);
    }

    public void StartMenu()
    {
      SceneManager.LoadScene(mainMenu);  
    }


    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
    Application.Quit();
#endif
    }


}
