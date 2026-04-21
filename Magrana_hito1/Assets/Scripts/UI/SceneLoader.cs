using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string sceneToLoad = "SampleScene"; // Nom de l'escena a carregar
    private int sceneIndex = 1; // Índex d'aquesta (Apareix a la Scene List del Build Profile)
    [SerializeField] private bool useIndexInsteadOfName = false; // Escollim si treballar amb el nom o amb l'índex


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detectar el clic esquerre (0) del ratolí
        {
            LoadNextScene(); // Executa la funció
        }
    }
    private void LoadNextScene()
    {
        // Puedes añadir un efecto de sonido aquí si quieres
        // AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        /* Es carrega la següent escena, per índex o títol */
        if (useIndexInsteadOfName)
            SceneManager.LoadScene(sceneIndex);
        else
            SceneManager.LoadScene(sceneToLoad);
    }
}
