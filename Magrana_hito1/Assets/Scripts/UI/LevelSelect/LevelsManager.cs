using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
    public MenuAudioManager audioManager;

    private int tutorialLevelIndex = 3; // sample scene
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<MenuAudioManager>();
    }

    public void LoadTutorialLevel()
    {
        audioManager.PlayFocusedSound(audioManager.focusedSFX);
        StartCoroutine(waitAndPlaySound(0.5f));
        SceneManager.LoadScene(tutorialLevelIndex);
    }
    IEnumerator waitAndPlaySound(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
        }
}
