using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    private static MenuAudioManager instance;
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip musicClip;
    public AudioClip clickSFX;
    public AudioClip focusedSFX;
    private void Awake()
    {
        if (instance == null) // Singleton: evita duplicats d'audio manager
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destruye el duplicado si ya existe uno
        }
    }
    private void Start()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }
    public void PlayClickSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
        // Debug.Log("Reproduciendo sonido de clic: ");
    }
    public void PlayFocusedSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
        Debug.Log("Reproduciendo sonido de enfoque: ");
    }
}
