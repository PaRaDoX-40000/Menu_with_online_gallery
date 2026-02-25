using UnityEngine;
using UnityEngine.SceneManagement; // Нужно для перезагрузки сцены
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnGameOver;

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    private bool isGameOver = false;

    void Awake()
    {       
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
     
        Time.timeScale = 1f;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;              
        Time.timeScale = 0f;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (ScoreManager.Instance != null)
            {
                OnGameOver?.Invoke();
                finalScoreText.text = "ФИНАЛЬНЫЙ СЧЕТ: " + ScoreManager.Instance.GetCurrentScore();
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
}