using UnityEngine;
using UnityEngine.UI;

public class ButtonClickableArea : MonoBehaviour
{
    private void Start()
    {
        // Obtiene el componente Image y asigna el umbral
        Image image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.3f;
    }
}