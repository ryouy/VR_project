using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Stick : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;
    private XRGrabInteractable grab;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool isCaught = false;
    private bool isDropping = false;

    public bool hasDropped = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        grab.enabled = false;

        grab.selectEntered.AddListener(OnGrabbed);
    }

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

    public void ResetStick()
{
    hasDropped = false;

    gameObject.SetActive(true);

    isDropping = false;

    grab.enabled = false;

    rb.isKinematic = false;

    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    transform.position = startPosition;
    transform.rotation = startRotation;

    rb.useGravity = false;
    rb.isKinematic = true;
}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Invoke(nameof(HideStick), 1f);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
{
    if (!isDropping) return;

    if (isCaught) return;

    isCaught = true;

    GameManager.Instance.AddScore();

    audioSource.Play();

    Invoke(nameof(HideStick), 0.3f);
}

    void HideStick()
{
    gameObject.SetActive(false);

    FindObjectOfType<StickManager>().CheckGameEnd();
}
}