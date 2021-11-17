using System.Collections.Generic;
using UnityEngine;
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
        // empty strings not allowed. Lenght is 1 because of end sequence '\n'.
        if (inputPlayerName.text.Trim().Length == 1)
        {
            return;
        }
        
        GameManager.Instance.player.name = inputPlayerName.text.Trim();
        GameManager.StartGame();
    }

    public void OnExitClicked()
    {
        GameManager.QuitGame();
    }

    private void UpdateHighscores()
    {
        var highscores = GameManager.Instance.highscores;
        for (var i = 0; i < highscores.Length; i++)
        {
            textHighscores[i].text = (highscores[i].name == "" ? "NoName" : highscores[i].name) + " >> " + highscores[i].score;
        }
    }
}
