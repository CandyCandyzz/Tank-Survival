using UnityEngine;

public class AudioSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] clipSFX;
    [SerializeField] private AudioSource audioSource;

    public void PlaySFX(int index)
    {
        audioSource.PlayOneShot(clipSFX[index]);
    }
}
