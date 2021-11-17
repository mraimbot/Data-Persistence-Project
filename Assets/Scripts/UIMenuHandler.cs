using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMenuHandler : MonoBehaviour
{
    [Header("UI - Components")]
    [SerializeField] private TextMeshProUGUI inputPlayerName;
    [SerializeField] private TextMeshProUGUI textTop1;
    [SerializeField] private TextMeshProUGUI textTop2;
    [SerializeField] private TextMeshProUGUI textTop3;
    [SerializeField] private TextMeshProUGUI textTop4;
    [SerializeField] private TextMeshProUGUI textTop5;
    [SerializeField] private TextMeshProUGUI textTop6;
    [SerializeField] private TextMeshProUGUI textTop7;
    [SerializeField] private TextMeshProUGUI textTop8;
    
    public void OnPlayClicked()
    {
        GameManager.Instance.StartGame();
    }

    public void OnExitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    public void UpdateHighscores()
    {
    }
}
