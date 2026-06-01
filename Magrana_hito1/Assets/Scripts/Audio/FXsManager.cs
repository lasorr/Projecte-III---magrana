using UnityEngine;

public class FXsManager : MonoBehaviour
{
    public AudioSource sfxSource;
    private static FXsManager instance;

 #region AUDIO CLIPS
    [Header("UI")]
    public AudioClip clickClip;
    public AudioClip focusedClip;
    public AudioClip initialCountdownClip;
    public AudioClip finalCountdownClip;

    [Header("Gameplay")]
    public AudioClip spawnClip;
    public AudioClip walkClip;
    public AudioClip wooshClip;
    public AudioClip stunnedClip;
    public AudioClip powerUpClip;
    public AudioClip powerDownClip;
    public AudioClip transformClip;
    public AudioClip unTransformClip;
    public AudioClip timeWarningClip;
    public AudioClip winnerClip;

    [Header("Characters")]
    public AudioClip porcliciesClip;
    public AudioClip[] mussolClips;
    public AudioClip[] monjaClips;
    public AudioClip[] iaiaClips;
    public AudioClip dragQueenClip;

    [Header("Environment")]
    public AudioClip officeClip;
    public AudioClip coffeeShopClip;
    public AudioClip desnonamentClip;
    public AudioClip edificiCatolicClip;
    #endregion
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
    void Start()
    {
        sfxSource.PlayOneShot(spawnClip);
    }
    public void PlayTimeWarning()
    {
        sfxSource.PlayOneShot(timeWarningClip);

    }
    public void PlayWalkSound()
    {
    // Solo reproduce si no está ya sonando el sonido de pasos
        if (!sfxSource.isPlaying && sfxSource.clip != walkClip)
        {
            sfxSource.clip = walkClip;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }
    public void StopWalkSound()
    {
        if (sfxSource.isPlaying && sfxSource.clip == walkClip)
        {
            sfxSource.Stop();
            sfxSource.loop = false;
        }
    }
    public void PlayStunnedSound()
    {
        sfxSource.loop = false;
        if (!sfxSource.isPlaying)
        {
            sfxSource.PlayOneShot(stunnedClip);

        }
    }
}
