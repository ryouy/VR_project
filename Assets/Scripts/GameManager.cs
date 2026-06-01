// GameManager.cs

using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;

    public Stick[] sticks;

    public StickManager stickManager;

    public AudioSource finalScoreSE;

    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
    }

    // スコア加算
    public void AddScore()
    {
        score++;

        UpdateScoreUI();
    }

    // UI更新
    void UpdateScoreUI()
    {
        scoreText.text = "SCORE : " + score;
    }

    // 最終スコア表示
    public void ShowFinalScore()
{
    Debug.Log("Final Score : " + score);

    StartCoroutine(ShowFinalScoreRoutine());
}

    IEnumerator ShowFinalScoreRoutine()
    {
        // 少し待つ
        yield return new WaitForSeconds(1.5f);

        // SE再生
        finalScoreSE.Play();

        // 少し待つ
        yield return new WaitForSeconds(1.0f);

        // 最終スコア表示
        scoreText.text = "FINAL SCORE : " + score;
    }

    // リセット
    public void ResetGame()
{
    Debug.Log("Game Reset");

    score = 0;

    UpdateScoreUI();

    stickManager.StopGame();

    foreach (Stick stick in sticks)
    {
        stick.ResetStick();
    }
}
}