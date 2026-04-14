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
            Destroy(collision.gameObject);
            Instantiate(Dragg, collision.transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Cafeteria"))
        {
            Destroy(collision.gameObject);
            Instantiate(Verduleria, collision.transform.position, Quaternion.identity);
        }
    }
}