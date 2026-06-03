using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccioNivells : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }

    public void CarergarTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void CarergarNivell1()
    {
        SceneManager.LoadScene("Nivell_1");
    }

    public void CarergarNivell2()
    {
        SceneManager.LoadScene("Nivell_2");
    }
}