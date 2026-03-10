using UnityEngine;

public class Moviment_jugadora_1 : MonoBehaviour
{
    public float velocitat = 5f;

    void Update()
    {
        float movimentHorizontal = Input.GetAxis("Horizontal");
        float movimentVertical = Input.GetAxis("Vertical");

        Vector3 moviment = new Vector3(movimentHorizontal, 0, movimentVertical) * velocitat * Time.deltaTime;
        
        transform.Translate(moviment);
    }
}