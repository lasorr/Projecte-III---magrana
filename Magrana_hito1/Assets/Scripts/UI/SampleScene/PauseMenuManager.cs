using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel; // Panell que controlem
    public GameObject mainMenuUI; // Botons del manú principal
    public GameObject optionsUI; // Botons de les opcions
    public GameObject controlsUI; // Esquema dels controls
    public GameObject exitConfirmUI; // Missatge de confirmació sortir
    public GameObject quitConfirmUI; // Missatge de confirmació sortir
    public TMP_Text countdownText; // Referència al text del compte enrere

    public float initialCountdown = 3f; // Durada del compte enrere en segons

     void Awake()
    {
        // Assegurem que el menú està tancat per default
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false); // Deshabilita el panell del menú
            optionsUI.SetActive(false);
            controlsUI.SetActive(false);
        }
    }

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
            Time.timeScale = 0f; // Pausa el joc
        }
    }
    public void CloseMainMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false); // Deshabilita el menú
            Debug.Log("Menú tancat");
            StartCoroutine(ResumeCountdownCoroutine());
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
        exitConfirmUI.SetActive(false);

        Debug.Log("HideQuitConfirm EJECUTADO");
    }
    public void QuitConfirmed()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Para pruebas en el Editor
        #else
        Application.Quit();  // Para la build final
        #endif
    }
    public void ShowExitConfirm()
    {
        exitConfirmUI.SetActive(true);
    }
    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Assegura que el temps està normal abans de canviar d'escena
        SceneManager.LoadScene("Start"); // Carrega l'escena del menú principal
    }
    public void HideExitConfirm()
    {
        exitConfirmUI.SetActive(false);
        Debug.Log("HideExitConfirm EJECUTADO");
    }
    IEnumerator ResumeCountdownCoroutine()
        {
            float remainingTime = initialCountdown;
        
            while (remainingTime > 0)
            {
                if (countdownText != null)
                    {
                        countdownText.text = Mathf.CeilToInt(remainingTime).ToString();
                        countdownText.gameObject.SetActive(true);
                    }
            
                    yield return new WaitForSecondsRealtime(1f);
                    remainingTime--;
            }
        
            if (countdownText != null)
            {
                countdownText.gameObject.SetActive(false);
            }
        
            Time.timeScale = 1f; // Continua el joc
        }
}
