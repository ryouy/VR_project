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

    public GameMachine gameMachine;

    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
{
    UpdateScoreUI();

    gameMachine.SetIdleMode();
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
    gameMachine.GameFinished();

    yield return new WaitForSeconds(1.5f);

    finalScoreSE.Play();

    yield return new WaitForSeconds(1.0f);

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

    gameMachine.SetIdleMode();
}
}