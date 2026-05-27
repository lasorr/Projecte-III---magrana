using UnityEngine;

public class MapaUI : MonoBehaviour
{
    public Transform plane;
    public Vector3 offset = new Vector3(0, 5, -8);

    private void LateUpdate()
    {
        transform.position = plane.position + offset;
    }
}