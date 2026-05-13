using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel; // Panell que controlem
    public GameObject mainMenuUI; // Botons del manú principal
    public GameObject optionsUI; // Botons de les opcions
    public GameObject controlsUI; // Esquema dels controls
    public GameObject quitConfirmUI; // Missatge de confirmació sortir

    void Start()
    {
        Debug.Log("Menu Tancat");
        // Assegurem que el menú està tancat per default
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false); // Deshabilita el panell del menú
            optionsUI.SetActive(false);
            controlsUI.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(mainMenuPanel.activeSelf) // Detectar si el menú ja està obert
            {
                CloseMainMenu();
                HideOptions(); // Si les opcions estan obertes, al tornar a obrir el menú no cal que tornin a obrir-se
                HideControls();
                Debug.Log("Menu Tancat");
            }
            else
            {
                OpenMainMenu();
                mainMenuPanel.SetActive(true);
                Debug.Log("Menu Obert");
            }
        }
    }

    /* Important que la interfície que volem que actui com a botó, tingui el component Button
    A l'apartat OnClick() d'aquest component, establim que MenuManager executi la següent funció */
    public void OpenMainMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true); // Habilita el menú
            optionsUI.SetActive(false);
            controlsUI.SetActive(false);
            Debug.Log("Menú obert");
        }
    }
    public void CloseMainMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false); // Deshabilita el menú
            Debug.Log("Menú tancat");

        }
    }

    public void ShowOptions()
    {
        mainMenuUI.SetActive(false);
        optionsUI.SetActive(true);

        Debug.Log("Opcions obertes");
    }
    public void HideOptions()
    {
        mainMenuUI.SetActive(true);
        optionsUI.SetActive(false);
        Debug.Log("Opcions tancades, menú principal obert");
    }

    public void ShowControls()
    {
        mainMenuUI.SetActive(false);
        controlsUI.SetActive(true);

        Debug.Log("Controls oberts");
    }
    public void HideControls()
    {
        mainMenuUI.SetActive(true);
        controlsUI.SetActive(false);

        Debug.Log("Controls tancats, menú principal obert");
    }

    public void ShowQuitConfirm()
    {
        quitConfirmUI.SetActive(true);

        Debug.Log("Vols sortir?");
    }
    public void HideQuitConfirm()
    {
        quitConfirmUI.SetActive(false);

        Debug.Log("No vol sortir");
    }
    public void QuitConfirmed()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Para pruebas en el Editor
        #else
        Application.Quit();  // Para la build final
        #endif
    }
}
