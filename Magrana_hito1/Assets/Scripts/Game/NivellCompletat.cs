using UnityEngine;
using UnityEngine.SceneManagement;

public class NivellCompletat : MonoBehaviour
{
    public string nivellActual; // Nom del nivell
    public void DesbloquejarSeguent()
    {
        if (nivellActual == "Tutorial")
            PlayerPrefs.SetInt("TutorialComplet", 1);
            
        if (nivellActual == "Nivel1")
            PlayerPrefs.SetInt("Nivell1Complet", 1);
            
        if (nivellActual == "Nivel2")
            PlayerPrefs.SetInt("Nivell2Complet", 1);
        
        PlayerPrefs.Save();
        SceneManager.LoadScene("LevelSelect"); // Cambia al nombre de tu escena
    }
}