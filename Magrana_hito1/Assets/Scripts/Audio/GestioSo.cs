using UnityEngine;

public class GestioSo : MonoBehaviour
{
    public AudioSource BonkAudioSource;
    public static GestioSo instance;
    

 /*#region AUDIO CLIPS
    [Header("UI")]
    public AudioClip clickClip;
    public AudioClip focusedClip;
    public AudioClip initialCountdownClip;
    public AudioClip finalCountdownClip;

    [Header("Gameplay")]
    
    public AudioClip walkClip;
    public AudioClip wooshClip;
    public AudioClip stunnedClip;
    public AudioClip powerUpClip;
    public AudioClip powerDownClip;
    public AudioClip transformClip;
    public AudioClip unTransformClip;
    
    

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
    */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip clip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(BonkAudioSource, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, clip.length);
    }
    public void PlayRandomSound(AudioClip[] clip, Transform spawnTransform, float volume)
    {
        int rand = Random.Range(0, clip.Length);

        AudioSource audioSource = Instantiate(BonkAudioSource, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, clip[rand].length);
    }
    public AudioSource PlaySoundPersistent(AudioClip clip, Transform spawnTransform, float volume, bool loop = false)
    {
        if (clip == null) return null;
        
        AudioSource audioSource = Instantiate(BonkAudioSource, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
        return audioSource; // Devuelve referencia, NO se autodestruye
    }
    public void StopSound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Destroy(audioSource.gameObject);
        }
    }
}
