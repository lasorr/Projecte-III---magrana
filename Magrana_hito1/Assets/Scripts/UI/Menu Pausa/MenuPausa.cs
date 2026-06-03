using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject pausaMenuUI;

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

    void Pausa()
    {
        pausaMenuUI.SetActive(true);
        Time.timeScale = 0f;
        estaPausat = true;
    }

    public void escenaSeleccioNivells()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection"); //POSAR NOM ESCENA MENU NIVELLS
    }
}