using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletarNivel : MonoBehaviour
{
    public string nivellActual; // Escribe aquí: "Tutorial", "Nivel1" o "Nivel2"

    // Llama a este método cuando el jugador complete el nivel
    public void NivellCompletat()
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