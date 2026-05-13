
using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel; // Panell que controlem
    public GameObject[] mainMenuUI;
    void Start()
    {
        Debug.Log("Menu Tancat");
        // Assegurem que el menú està tancat per default
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false); // Deshabilita el panell del menú
        mainMenuUI = GameObject.FindGameObjectsWithTag("MenuUI");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(mainMenuPanel.activeSelf) // Detectar si el menú ja està obert
            {
                CloseMainMenu();
                Debug.Log("Menu Tancat");
            }
            else
            {
                OpenMainMenu();
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

    }
}
