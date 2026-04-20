using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string sceneToLoad = "SampleScene";
    private int sceneIndex = 1;
    private bool useIndexInsteadOfName = false;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }
    private void LoadNextScene()
    {
        // Puedes añadir un efecto de sonido aquí si quieres
        // AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);

        if (useIndexInsteadOfName)
            SceneManager.LoadScene(sceneIndex);
        else
            SceneManager.LoadScene(sceneToLoad);
    }
}
