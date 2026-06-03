using UnityEngine;
using UnityEngine.UI;

public class MenuOpcions : MonoBehaviour
{
    public void PantallaCompleta()
    {
        Screen.fullScreen = true;
    }

    public void Finestra()
    {
        Screen.fullScreen = false;
    }
}