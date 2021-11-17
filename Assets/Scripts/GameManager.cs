using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    [HideInInspector] public Highscore Player = new Highscore();

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

    public bool UpdateHighscores()
    {
        var isNewHighscore = false;
        
        for (var i = 0; i < Highscores.Length; i++)
        {
            if (Player.Score > Highscores[i].Score)
            {
                for (var j = Highscores.Length - 2; j >= i; j--)
                {
                    Highscores[j+1].Name = Highscores[j].Name;
                    Highscores[j+1].Score = Highscores[j].Score;
                }
                
                Highscores[i].Name = Player.Name;
                Highscores[i].Score = Player.Score;
                isNewHighscore = true;
                break;
            }
        }

        return isNewHighscore;
    }

    public void LoadHighscores()
    {
        var filepath = Application.persistentDataPath + "/highscores.json";
        if (!File.Exists(filepath)) return;
        
        var json = File.ReadAllText(filepath);
        var save = JsonUtility.FromJson<SaveData>(json);
        
        if (save != null)
        {
            Highscores = save.Highscores;
        }
    }

    public void SaveHighscores()
    {
        var save = new SaveData()
        {
            Highscores = this.Highscores
        };

        var json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }
}
