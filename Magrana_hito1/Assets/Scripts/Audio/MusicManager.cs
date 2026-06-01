using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClip;
    private static MusicManager instance;

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
        if (musicSource != null)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
        }
    }
}
