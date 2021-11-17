using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Datastructures

    [Serializable]
    public struct Highscore
    {
        public string Name;
        public int Score;
    }

    [Serializable]
    public class SaveData
    {
        public Highscore[] Highscores;
    }

    #endregion

    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    #endregion
    
    [HideInInspector] public Highscore[] Highscores = new Highscore[8];

    private const int SCENE_MENU_ID = 0;
    private const int SCENE_GAME_ID = 1;

    public void OpenMenu()
    {
        SceneManager.LoadScene(SCENE_MENU_ID);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SCENE_GAME_ID);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
