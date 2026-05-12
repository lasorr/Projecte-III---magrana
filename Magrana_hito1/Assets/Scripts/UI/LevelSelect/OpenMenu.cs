using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject pausePanel; // Panell que controlem
    void Start()
    {
        // Assegurem que el menú està tancat per default
        if (pausePanel != null)
            pausePanel.SetActive(false); // Deshabilita el panell del menú
    }

    public void OpenMainMenu()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true); // Habilita el menú
        }
    }
}
