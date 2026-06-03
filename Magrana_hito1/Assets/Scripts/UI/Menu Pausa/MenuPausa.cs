using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject pausaMenuUI;

    public GameObject menuOpcionsUI;

    private bool estaPausat = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausat)
            {
                Continua();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Continua()
    {
        pausaMenuUI.SetActive(false);
        Time.timeScale = 1f;
        estaPausat = false;
    }

    public void ObrirOpcions()
    {
        menuOpcionsUI.SetActive(true);
        pausaMenuUI.SetActive(false);
    }

    public void TancarOpcions()
    {
        menuOpcionsUI.SetActive(false);
        pausaMenuUI.SetActive(true );
    }

    void Pausa()
    {
        pausaMenuUI.SetActive(true);
        Time.timeScale = 0f;
        estaPausat = true;
    }

    public void EscenaSeleccioNivells()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection"); //POSAR NOM ESCENA MENU NIVELLS
    }
}