using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public MenuAudioManager audioManager;
    private int sceneIndex = 1; // �ndex escena (Escena nivells)
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<MenuAudioManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detectar el clic esquerre (0) del ratol�
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Alternativament, podem utilitzar la barra espaciadora per a continuar
        {
            LoadNextScene();
        }
    }
    private void LoadNextScene()
    {
        audioManager.PlayClickSound(audioManager.clickSFX);

        StartCoroutine(waitAndPlaySound(0.5f));

        IEnumerator waitAndPlaySound(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
