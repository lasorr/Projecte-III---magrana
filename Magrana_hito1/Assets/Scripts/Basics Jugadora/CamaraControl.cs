using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    public Transform jugador;
    public Vector3 offset = new Vector3(0, 5, -8);
    public float suavizado = 5f;

    private void LateUpdate()
    {
        Vector3 posicionDeseada = jugador.position + offset;
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
    }
}