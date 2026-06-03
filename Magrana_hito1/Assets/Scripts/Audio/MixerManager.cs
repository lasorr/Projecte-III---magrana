using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetSFXVolume(float level)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
    }
    public void SetMusicVolume(float level)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }
}
