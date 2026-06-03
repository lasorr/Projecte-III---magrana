using UnityEngine;

public class OnAwakeMorir : MonoBehaviour
{
    void Awake()
    {
        Invoke("Morir", 3f);
    }

    void Morir()
    {
        Destroy(gameObject);
    }
}