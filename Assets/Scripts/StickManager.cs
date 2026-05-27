using UnityEngine;

public class StickManager : MonoBehaviour
{
    public Stick[] sticks;

    private bool gameStarted = false;

    public void StartGame()
    {
        if (gameStarted) return;

        gameStarted = true;

        InvokeRepeating(nameof(DropRandomStick), 1f, 2f);
    }

    public void StopGame()
    {
        gameStarted = false;

        CancelInvoke(nameof(DropRandomStick));
    }

    void DropRandomStick()
{
    // 未使用棒を数える
    int availableCount = 0;

    foreach (Stick stick in sticks)
    {
        if (!stick.hasDropped)
        {
            availableCount++;
        }
    }

    // 全部使ったら停止
    if (availableCount == 0)
{
    StopGame();

    GameManager.Instance.ShowFinalScore();

    return;
}

    // ランダム選択
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