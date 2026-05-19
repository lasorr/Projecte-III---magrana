using UnityEngine;

public class PropietariaEdifici : MonoBehaviour
{
    public int Propietaria; // 0 = no és ni de J1 ni de J2, 1 = és de J1, 2 = és de J2
    
    void Start()
    {
        Debug.Log("Propietaria Edifici: " + Propietaria);
    }

}