using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick brickPrefab;
    public int lineCount = 6;
    public Rigidbody ball;

    public Text textBestScore;
    public Text textPlayerScore;
    public GameObject textGameOver;
    
    private bool hasStarted;
    private bool isGameOver;
    
    private void Start()
    {
        SetBestScore();
        
        GameManager.Instance.player.score = 0;
        
        const float step = 0.6f;
        var perLine = Mathf.FloorToInt(4.0f / step);
        
        var pointCountArray = new [] {1,1,2,2,5,5};
        
        for (var i = 0; i < lineCount; ++i)
        {
            for (var x = 0; x < perLine; ++x)
            {
                var position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!hasStarted)
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            hasStarted = true;
            var randomDirection = Random.Range(-1.0f, 1.0f);
            var forceDir = new Vector3(randomDirection, 1, 0);
            forceDir.Normalize();

            ball.transform.SetParent(null);
            ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
        }
        else if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
#if UNITY_EDITOR
                if (GameManager.Instance == null) return;
#endif
                GameManager.StartGame();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
#if UNITY_EDITOR
                if (GameManager.Instance == null) return;
#endif
                GameManager.OpenMenu();
            }
        }
    }

    private void AddPoint(int point)
    {
        GameManager.Instance.player.score += point;
        textPlayerScore.text = $"Score : {GameManager.Instance.player.score}";
    }

    public void GameOver()
    {
        if (GameManager.Instance.UpdateHighscores())
        {
            GameManager.Instance.SaveHighscores();
            SetBestScore();
        }
        
        isGameOver = true;
        textGameOver.SetActive(true);
    }

    private void SetBestScore()
    {
        Debug.Log(GameManager.Instance.highscores.Length);
        var highscore = GameManager.Instance.highscores[0];
        textBestScore.text = "Best Highscore: " + highscore.name + " with " + highscore.score + " points.";
    }
}
