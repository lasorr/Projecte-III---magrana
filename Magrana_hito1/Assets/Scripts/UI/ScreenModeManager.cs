using UnityEngine;
using UnityEngine.UI;

public class ScreenModeManager : MonoBehaviour
{
    public Button fullScreenButton;
    public Button windowedButton;
    private Color selectedColor = new Color(242,238,139);
    private Color colorNoSeleccionado = Color.white;
   
    void Start()
    { /* Afegim un listener que estigui pendent de quin botó es selecciona */
        fullScreenButton.onClick.AddListener(() => Select(true));
        windowedButton.onClick.AddListener(() => Select(false));

        updateSelected();
    }

    void Select(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        updateSelected();
        if (isFullScreen)
        {
            Debug.Log("Full Screen");
        }else
            Debug.Log("Windowed");
    }

    void updateSelected()
    {
        fullScreenButton.GetComponent<Image>().color = Screen.fullScreen ? selectedColor : colorNoSeleccionado;
        windowedButton.GetComponent<Image>().color = Screen.fullScreen ? colorNoSeleccionado : selectedColor;
    }
}
