using UnityEngine;
using System.Collections;

public class StickManager : MonoBehaviour
{
    public Stick[] sticks;

    private bool gameStarted = false;

    // ゲーム開始
    public void StartGame()
    {
        if (gameStarted) return;

        gameStarted = true;

        StartCoroutine(DropLoop());
    }

    // ゲーム停止
    public void StopGame()
    {
        gameStarted = false;

        StopAllCoroutines();
    }

    // ランダム落下ループ
    IEnumerator DropLoop()
    {
        while (gameStarted)
        {
            // 1〜3秒ランダム待機
            float waitTime = Random.Range(1f, 3f);

            yield return new WaitForSeconds(waitTime);

            DropRandomStick();
        }
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