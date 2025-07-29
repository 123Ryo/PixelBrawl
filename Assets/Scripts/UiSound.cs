using UnityEngine;

public class UIButtonSFX : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GameObject.Find("UIAudioPlayer")?.GetComponent<AudioSource>();

        if (audioSource == null)
            Debug.LogWarning("❗ 無法找到 UIAudioPlayer 的 AudioSource");
    }

    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
