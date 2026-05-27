// Stick.cs

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stick : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grab;

    private Renderer stickRenderer;
    private Material stickMaterial;

    private AudioSource audioSource;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool isCaught = false;
    private bool isDropping = false;

    public bool hasDropped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        grab = GetComponent<XRGrabInteractable>();

        stickRenderer = GetComponent<Renderer>();
        stickMaterial = stickRenderer.material;

        audioSource = GetComponent<AudioSource>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        grab.enabled = false;

        grab.selectEntered.AddListener(OnGrabbed);

        ResetEmission();
    }

    // 棒を落下
    public void Drop()
    {
        hasDropped = true;

        gameObject.SetActive(true);

        isCaught = false;
        isDropping = true;

        grab.enabled = true;

        rb.isKinematic = false;
        rb.useGravity = true;
    }

    // 棒を初期状態へ戻す
    public void ResetStick()
    {
        hasDropped = false;

        gameObject.SetActive(true);

        isDropping = false;

        grab.enabled = false;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = startPosition;
        transform.rotation = startRotation;

        rb.useGravity = false;
        rb.isKinematic = true;

        ResetEmission();
    }

    // 床へ落下
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Invoke(nameof(HideStick), 1f);
        }
    }

    // キャッチ成功
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!isDropping) return;

        if (isCaught) return;

        isCaught = true;

        GameManager.Instance.AddScore();

        audioSource.Play();

        Flash();

        Invoke(nameof(HideStick), 0.3f);
    }

    // 棒を消す
    void HideStick()
    {
        gameObject.SetActive(false);

        FindObjectOfType<StickManager>().CheckGameEnd();
    }

    // 発光
    void Flash()
    {
        stickMaterial.EnableKeyword("_EMISSION");

        stickMaterial.SetColor("_EmissionColor", Color.yellow * 5f);

        Invoke(nameof(ResetEmission), 0.2f);
    }

    // 発光リセット
    void ResetEmission()
    {
        stickMaterial.SetColor("_EmissionColor", Color.black);

        stickMaterial.DisableKeyword("_EMISSION");
    }
}