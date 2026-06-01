using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelsManager : MonoBehaviour
{
    public MenuAudioManager audioManager;

    private int historiaIndex = 2;
    private int tutorialLevelIndex = 3;
    private int level1Index = 4;
    private int level2Index = 5;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<MenuAudioManager>();
    }

    public void LoadMainMenu()
    {
        audioManager.PlayClickSound(audioManager.clickSFX);
        StartCoroutine(waitAndPlaySound(0.5f));
        SceneManager.LoadScene(historiaIndex); // Replace 0 with the actual index of the Main Menu scene
        Destroy(GameObject.FindGameObjectWithTag("Audio")); // Destruye el audio manager para evitar que siga sonando al cargar el nivel
    }
    
    public void LoadTutorialLevel()
    {
        audioManager.PlayClickSound(audioManager.clickSFX);
        StartCoroutine(waitAndPlaySound(0.5f));
        SceneManager.LoadScene(tutorialLevelIndex);
                    Destroy(GameObject.FindGameObjectWithTag("Audio")); // Destruye el audio manager para evitar que siga sonando al cargar el nivel

    }
    public void LoadLevel1()
    {
        audioManager.PlayClickSound(audioManager.clickSFX);
        StartCoroutine(waitAndPlaySound(0.5f));
        SceneManager.LoadScene(level1Index); // Replace 1 with the actual index of Level 1
        Destroy(GameObject.FindGameObjectWithTag("Audio"));
    }

    public void LoadLevel2()
    {
        audioManager.PlayClickSound(audioManager.clickSFX);
        StartCoroutine(waitAndPlaySound(0.5f));
        SceneManager.LoadScene(level2Index); // Replace 2 with the actual index of Level 2
        Destroy(GameObject.FindGameObjectWithTag("Audio"));
    }

    IEnumerator waitAndPlaySound(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
        }
}
