using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;

    public Stick[] sticks;

    public StickManager stickManager;

    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore()
    {
        score++;

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "SCORE : " + score;
    }

    public void ShowFinalScore()
    {
        scoreText.text = "FINAL SCORE : " + score;
    }

    public void ResetGame()
    {
        score = 0;

        UpdateScoreUI();

        stickManager.StopGame();

        foreach (Stick stick in sticks)
        {
            stick.ResetStick();
        }
    }
}