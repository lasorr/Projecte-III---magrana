using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int sceneIndex = 1; // �ndex escena (Escena nivells)

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
        // Puedes a�adir un efecto de sonido aqu� si quieres
        // AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        /* Es carrega la seg�ent escena, per �ndex o t�tol */
         SceneManager.LoadScene(sceneIndex);
    }
}
