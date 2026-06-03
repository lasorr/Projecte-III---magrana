using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public MenuAudioManager audioManager;
    private int sceneHistoria = 1; // �ndex escena (Escena nivells)
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Para pruebas en el Editor
        #else
        Application.Quit();  // Para la build final
        #endif
        }
    }
    private void LoadNextScene()
    {
        audioManager.PlayClickSound(audioManager.clickSFX);

        StartCoroutine(waitAndPlaySound(0.5f));

        IEnumerator waitAndPlaySound(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(sceneHistoria);
        }
    }
}
