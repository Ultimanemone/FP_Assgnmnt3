using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum UIState
{
    Playing,
    GameOver
}

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        UpdateScore(0);
    }

    public void UpdateScore(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }

    public void TriggerGameOver(int finalScore)
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "SCORE: " + finalScore.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}