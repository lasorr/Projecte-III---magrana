using UnityEngine;
using UnityEngine.UI;

public class ScreenModeManager : MonoBehaviour
{
    public Button fullScreenButton;
    public Button windowedButton;
    public Color selectedColor = new Color(242,238,139);
    public Color colorNoSeleccionado = Color.white;
    void Start()
    { /* Afegim un listener que estigui pendent de quin botó es selecciona */
        fullScreenButton.onClick.AddListener(() => Screen.fullScreen = true);
        windowedButton.onClick.AddListener(() => Screen.fullScreen = false);
    }
}
