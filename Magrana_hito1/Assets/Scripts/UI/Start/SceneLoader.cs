using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int sceneIndex = 1; // Índex escena (Escena nivells)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detectar el clic esquerre (0) del ratolí
        {
            LoadNextScene(); // Executa la funció
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Alternativament, podem utilitzar la barra espaciadora per a continuar
        {
            LoadNextScene();
        }
    }
    private void LoadNextScene()
    {
        // Puedes añadir un efecto de sonido aquí si quieres
        // AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        /* Es carrega la següent escena, per índex o títol */
         SceneManager.LoadScene(sceneIndex);
    }
}
