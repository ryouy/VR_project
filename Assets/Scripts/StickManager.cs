// StickManager.cs

using UnityEngine;

public class StickManager : MonoBehaviour
{
    public Stick[] sticks;

    private bool gameStarted = false;

    // ゲーム開始
    public void StartGame()
    {
        if (gameStarted) return;

        gameStarted = true;

        InvokeRepeating(nameof(DropRandomStick), 1f, 2f);
    }

    // ゲーム停止
    public void StopGame()
    {
        gameStarted = false;

        CancelInvoke(nameof(DropRandomStick));
    }

    // ランダム落下
    void DropRandomStick()
    {
        int availableCount = 0;

        foreach (Stick stick in sticks)
        {
            if (!stick.hasDropped)
            {
                availableCount++;
            }
        }

        // 全棒使用済み
        if (availableCount == 0)
        {
            return;
        }

        while (true)
        {
            int index = Random.Range(0, sticks.Length);

            if (!sticks[index].hasDropped)
            {
                sticks[index].Drop();
                break;
            }
        }
    }

    // 全棒終了確認
    public void CheckGameEnd()
    {
        foreach (Stick stick in sticks)
        {
            if (stick.gameObject.activeSelf)
            {
                return;
            }
        }

        StopGame();

        GameManager.Instance.ShowFinalScore();
    }
}