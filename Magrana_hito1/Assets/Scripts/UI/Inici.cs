using UnityEngine;
using UnityEngine.SceneManagement;

public class Inici : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("SeleccioNivells"); //POSAR NOM ESCENA MENU NIVELLS
        }
    }
}