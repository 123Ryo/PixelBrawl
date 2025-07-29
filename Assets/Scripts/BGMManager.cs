using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public AudioClip bgmClip;
    [Range(0f, 1f)] public float volume = 0.5f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
