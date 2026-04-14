using UnityEngine;
using UnityEngine.InputSystem;

public class Colpejar : MonoBehaviour
{
    public BoxCollider Arma;
    public InputActionReference cop;
    public Animator animator;
    public GameObject Dragg;
    public GameObject Verduleria;

    void Start()
    {
        Arma.enabled = false;
    }

    void Update()
    {
        if (cop.action.WasPressedThisFrame())
        {
            animator.SetBool("Colpejar", true);
            Arma.enabled = true;
            Invoke("DesactivarArma", 1.0f); // 1 segon (ajusta si l'animació és més llarga)
        }
    }

    void DesactivarArma()
    {
        Arma.enabled = false;
        animator.SetBool("Colpejar", false);
    }

    void OnCollisionEnter(Collision collision)
{
    Debug.Log("HA ENTRAT AL COLISION");
    if (collision.gameObject.CompareTag("Monja"))
    {
        Vector3 pos = collision.transform.position; //guarda la posicio
        Quaternion rot = collision.transform.rotation;  // Guardem la rotació original
        Destroy(collision.gameObject);
        Instantiate(Dragg, pos, rot);
    }
    else if (collision.gameObject.CompareTag("Cafeteria"))
    {
        Vector3 pos = collision.transform.position;
        Quaternion rot = collision.transform.rotation;
        Destroy(collision.gameObject);
        Instantiate(Verduleria, pos, rot);
    }
}
}