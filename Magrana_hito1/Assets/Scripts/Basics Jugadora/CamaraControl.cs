using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    public Transform jugadora;
    public Vector3 offset; //la distancia entre la jugadora i la camera
    public float velocidadCam = 0.5f;
    //public Vector3 desplazamiento;

    private void LateUpdate()
    { 
        transform.position = jugadora.position + offset;

        /*Vector3 posicionDeseada = objetivo.position + desplazamiento;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadCam);
        transform.position = posicionSuavizada;*/
    }
}
