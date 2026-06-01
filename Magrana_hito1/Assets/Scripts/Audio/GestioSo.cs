using UnityEngine;

public class GestioSo : MonoBehaviour
{
    public AudioSource clipAudio;
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
        AudioSource audioSource = Instantiate(clipAudio, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, clip.length);
    }
}
