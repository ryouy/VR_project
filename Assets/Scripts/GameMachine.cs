using UnityEngine;
using System.Collections;

public class GameMachine : MonoBehaviour
{
    private Renderer rend;
    private Material mat;

    private Coroutine rainbowCoroutine;

    private Color currentBaseColor;
    private Vector3 defaultPos;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;

        defaultPos = transform.position;

        SetIdleMode();
    }

    // =========================
    // 状態管理
    // =========================

    public void SetIdleMode()
    {
        StopRainbow();

        currentBaseColor = Color.cyan;
        mat.color = currentBaseColor;
    }

    public void SetPlayMode()
    {
        StopRainbow();

        currentBaseColor = Color.green;
        mat.color = currentBaseColor;
    }

    public void GameFinished()
    {
        StopRainbow();

        rainbowCoroutine = StartCoroutine(RainbowEffect());
    }

    // =========================
    // 成功演出
    // =========================

    public void OnCatch()
    {
        StartCoroutine(CatchEffect());
    }

    IEnumerator CatchEffect()
    {
        mat.color = Color.yellow;

        yield return StartCoroutine(SmallShake());

        yield return new WaitForSeconds(0.05f);

        mat.color = currentBaseColor;
    }

    // =========================
    // 失敗演出
    // =========================

    public void OnMiss()
    {
        StartCoroutine(MissEffect());
    }

    IEnumerator MissEffect()
    {
        mat.color = Color.red;

        yield return StartCoroutine(BigShake());

        yield return new WaitForSeconds(0.05f);

        mat.color = currentBaseColor;
    }

    // =========================
    // 小さい揺れ（成功）
    // =========================

    IEnumerator SmallShake()
    {
        Vector3 startPos = defaultPos;

        transform.position = startPos + Vector3.right * 1.5f;

        yield return new WaitForSeconds(0.06f);

        transform.position = startPos - Vector3.right * 1.0f;

        yield return new WaitForSeconds(0.06f);

        transform.position = startPos + Vector3.right * 0.5f;

        yield return new WaitForSeconds(0.04f);

        transform.position = startPos;
    }

    // =========================
    // 大きい揺れ（失敗）
    // =========================

    IEnumerator BigShake()
    {
        Vector3 startPos = defaultPos;

        transform.position = startPos + Vector3.left * 2.0f;

        yield return new WaitForSeconds(0.08f);

        transform.position = startPos + Vector3.right * 2.0f;

        yield return new WaitForSeconds(0.08f);

        transform.position = startPos;
    }

    // =========================
    // 終了演出
    // =========================

    IEnumerator RainbowEffect()
    {
        while (true)
        {
            mat.color = Color.HSVToRGB(
                Mathf.PingPong(Time.time * 0.5f, 1f),
                1f,
                1f);

            yield return null;
        }
    }

    // =========================
    // 共通
    // =========================

    void StopRainbow()
    {
        if (rainbowCoroutine != null)
        {
            StopCoroutine(rainbowCoroutine);
            rainbowCoroutine = null;
        }
    }
}