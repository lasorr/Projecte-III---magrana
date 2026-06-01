using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip musicClip;
    public AudioClip clickSFX;
    private void Start()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }
    public void PlayClickSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
        Debug.Log("Reproduciendo sonido de clic: ");
    }
}
