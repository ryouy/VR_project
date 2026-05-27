// ButtonSE.cs

using UnityEngine;

public class ButtonSE : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySE()
    {
        audioSource.Play();
    }
}