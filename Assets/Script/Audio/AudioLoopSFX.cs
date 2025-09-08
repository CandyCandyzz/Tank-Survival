using UnityEngine;

public class AudioLoopSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] clip;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private bool playOnAwake;
    [SerializeField] private int clipOnAwake;

    private void Start()
    {
        if(!playOnAwake) { return; }
        audioSource.clip = clip[clipOnAwake];
        audioSource.Play();
    }

    public void PlaySFX(int index)
    {
        audioSource.clip = clip[index];
        audioSource.Play();
    }
}
