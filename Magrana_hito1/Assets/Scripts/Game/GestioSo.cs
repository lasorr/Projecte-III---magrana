using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestioSo : MonoBehaviour
{
    public static GestioSo instance;

    [SerializeField] private AudioSource clipAudio;

    private void Awake()
    {
        instance = this;
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
