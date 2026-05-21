using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLevelSelect : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("Seleccio_Aleatori");
    }
}