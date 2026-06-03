using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccioNivells : MonoBehaviour
{
    public float alphaThreshold = 0.1f;

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }

    public void CarregarTutorial()
    {
        Debug.Log("Carregant Tutorial...");
        SceneManager.LoadScene("Tutorial");
    }

    public void CarregarNivell1()
    {
        Debug.Log("Carregant Nivell 1...");
        SceneManager.LoadScene("Nivell_1");
    }

    public void CarregarNivell2()
    {
        Debug.Log("Carregant Nivell 2...");
        SceneManager.LoadScene("Nivell_2");
    }
}