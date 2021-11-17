using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMenuHandler : MonoBehaviour
{
    [Header("UI - Components")]
    [SerializeField] private TextMeshProUGUI inputPlayerName;
    [SerializeField] private List<TextMeshProUGUI> textHighscores;

    private void Start()
    {
        GameManager.Instance.LoadHighscores();
        UpdateHighscores();
    }

    public void OnPlayClicked()
    {
        GameManager.Instance.Player.Name = inputPlayerName.text;
        GameManager.Instance.StartGame();
    }

    public void OnExitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    public void UpdateHighscores()
    {
        var highscores = GameManager.Instance.Highscores;
        for (int i = 0; i < highscores.Length; i++)
        {
            textHighscores[i].text = (highscores[i].Name == "" ? "NoName" : highscores[i].Name) + " >> " + highscores[i].Score;
        }
    }
}
