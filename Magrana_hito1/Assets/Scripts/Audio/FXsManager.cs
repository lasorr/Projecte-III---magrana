using UnityEngine;

public class FXsManager : MonoBehaviour
{
    public AudioSource sfxSource;
 #region AUDIO CLIPS
    [Header("UI")]
    public AudioClip clickClip;
    public AudioClip focusedClip;
    public AudioClip initialCountdownClip;
    public AudioClip finalCountdownClip;
    public AudioClip endAlarmClip;

    [Header("Gameplay")]
    public AudioClip spawnClip;
    public AudioClip walkClip;
    public AudioClip wooshClip;
    public AudioClip stunnedClip;
    public AudioClip powerUpClip;
    public AudioClip powerDownClip;
    public AudioClip transformClip;
    public AudioClip unTransformClip;
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
