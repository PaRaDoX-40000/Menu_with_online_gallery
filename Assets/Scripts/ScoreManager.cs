using UnityEngine;
using TMPro; // Если используете TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private int currentScore = 0;
    private int bestScore = 0;

    private const string BEST_SCORE_KEY = "BestScore";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        bestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        UpdateUI();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver.AddListener(SaveScroe);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOver.RemoveListener(SaveScroe);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    private void SaveScroe()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt(BEST_SCORE_KEY, bestScore);
            PlayerPrefs.Save();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore;
        bestScoreText.text = "Best: " + bestScore;
    }

    // Метод для вызова при смерти (показать финальный счет)
    public int GetCurrentScore() => currentScore;
}