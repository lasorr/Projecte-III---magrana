using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    public Transform objetivo;
    public float velocidadCam = 0.025f;
    public Vector3 desplazamiento;

    private void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + desplazamiento;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCam);
        transform.position = posicionSuavizada;
    }
}
