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
        audioManager.PlayClickSound(audioManager.clickSFX);
        StartCoroutine(waitAndPlaySound(0.5f));
        SceneManager.LoadScene(tutorialLevelIndex);
                    Destroy(GameObject.FindGameObjectWithTag("Audio")); // Destruye el audio manager para evitar que siga sonando al cargar el nivel

    }
    IEnumerator waitAndPlaySound(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
        }
}
