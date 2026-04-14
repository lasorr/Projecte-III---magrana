using UnityEngine;
using UnityEngine.InputSystem;

public class Colpejar : MonoBehaviour
{
    public BoxCollider Arma;
    public InputActionReference cop;
    public Animator animator; 

    public prefab Dragg;
    public prefab Verduleria; 


    void Start()
    {
        Arma = GetComponent<BoxCollider>();
    }

    public void FixedUpdate()
    {
        //quan desde el input action detecta que selecciona la tecla amb el nom attack
        //activa la animacio amb nom colpejar que es un parametre bool llavors, l'activa - es desactiva 
        //quan esta activat crida a la void control_collider
    }

    public void control_collider( /*cal posar parametre¿¿*/ )
    {
        //BoxCollider - Arma s'activa 
        //s'espera 1,30 segon que tarda l'animacio
        //desactiva el BoxCollider
    }
    public void OnCollision (Collider other)
    {
        if (other tag == Monja)
        {
            transform = other.position.transfrom;
            destroy.other;

            // agafar transform del other
            //destroy other 
            //institate object name prefab DragQueen
        }
        else if (other tag == Cafeteria)
        {
            // agafar transform del other
            //destroy other 
            //institate object name prefab Fruiteria
        }
    }
}