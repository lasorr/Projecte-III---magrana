using UnityEngine;
using UnityEngine.UI;

public class MenuOpcions : MonoBehaviour
{
    public Slider sliderMusica;
    public Slider sliderSFX;

    public void PantallaCompleta()
    {
        Screen.fullScreen = true;
    }

    public void Finestra()
    {
        Screen.fullScreen = false;
    }

    public void VolumMusica()
    {
        AudioListener.volume = sliderMusica.value;
    }

    public void VolumSFX()
    {
        Debug.Log("SFX Volume: " + sliderSFX.value);
    }
}