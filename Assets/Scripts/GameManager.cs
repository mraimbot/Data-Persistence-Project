using System;
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
        public string name;
        public int score;
    }

    [Serializable]
    public class SaveData
    {
        public Highscore[] highscores;
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
    
    [HideInInspector] public Highscore[] highscores = new Highscore[8];
    [HideInInspector] public Highscore player;

    private const int SCENE_MENU_ID = 0;
    private const int SCENE_GAME_ID = 1;
    
    public static void OpenMenu()
    {
        SceneManager.LoadScene(SCENE_MENU_ID);
    }

    public static void StartGame()
    {
        SceneManager.LoadScene(SCENE_GAME_ID);
    }

    public static void QuitGame()
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
        
        for (var i = 0; i < highscores.Length; i++)
        {
            if (player.score <= highscores[i].score) continue;
            
            for (var j = highscores.Length - 2; j >= i; j--)
            {
                highscores[j+1].name = highscores[j].name;
                highscores[j+1].score = highscores[j].score;
            }
                
            highscores[i].name = player.name;
            highscores[i].score = player.score;
            isNewHighscore = true;
            break;
        }

        return isNewHighscore;
    }

    public void LoadHighscores()
    {
        var filepath = Application.persistentDataPath + "/highscores.json";
        
        if (!File.Exists(filepath)) return;
        
        var save = JsonUtility.FromJson<SaveData>(File.ReadAllText(filepath));
        
        if (save != null)
        {
            highscores = save.highscores;
        }
    }

    public void SaveHighscores()
    {
        var save = new SaveData()
        {
            highscores = this.highscores
        };

        File.WriteAllText(Application.persistentDataPath + "/highscores.json", JsonUtility.ToJson(save));
    }
}
