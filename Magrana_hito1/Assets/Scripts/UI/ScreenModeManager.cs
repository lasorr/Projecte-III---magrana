using UnityEngine;
using UnityEngine.UI;

public class ScreenModeManager : MonoBehaviour
{
    public Button fullScreenButton;
    public Button windowedButton;
    void Start()
    { /* Afegim un listener que estigui pendent de quin botó es selecciona */
        fullScreenButton.onClick.AddListener(() => Screen.fullScreen = true);
        windowedButton.onClick.AddListener(() => Screen.fullScreen = false);
    }
}
