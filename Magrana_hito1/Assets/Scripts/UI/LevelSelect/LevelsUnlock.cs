using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button tutorialButton;
    public Button nivell1Button;
    public Button nivell2Button;
    public Button aleatoriButton;

    void Start()
    {   
        // Cadascun dels scripts de cada nivell, enviarà un missatge conforme aquest ha estat completat
        // PlayerPrefs es la memòria del joc
        if (PlayerPrefs.GetInt("TutorialComplet", 0) == 1) /* Es comprovarà si existeix aquest missatge,*/
            nivell1Button.interactable = true; /* si no existeix, el valor per defecte serà 0, 
            i si existeix, 1, i el button serà interactable */
            
        if (PlayerPrefs.GetInt("Nivell1Complet", 0) == 1)
            nivell2Button.interactable = true;
            
        if (PlayerPrefs.GetInt("Nivell2Complet", 0) == 1)
            aleatoriButton.interactable = true;
    }
}