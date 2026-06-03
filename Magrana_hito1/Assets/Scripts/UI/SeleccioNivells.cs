using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

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