using UnityEngine;

public class PropietariaEdifici : MonoBehaviour
{
    public int Propietaria; // 0 = no és ni de J1 ni de J2, 1 = és de J1, 2 = és de J2

    public int punts;
    
    public GameObject edificiCapitalistaAssociat;
    public GameObject edificiBoAssociat;

    public bool edificiTransformat = false;

    public TimeManager TimeManager;
    
    void Update ()
    {
        if (edificiTransformat)
        {
            Debug.Log("Transformant edifici a capitalista");

            Vector3 pos = edificiBoAssociat.transform.position;
            Quaternion rot = edificiBoAssociat.transform.rotation;
            
            if (Propietaria == 1)
            {
                TimeManager.Instance.RestaPunts(1, punts);
                Debug.Log("Edifici de J1 transformat a capitalista");
            }
            else if (Propietaria == 2)
            {
                TimeManager.Instance.RestaPunts(2, punts);
                Debug.Log("Edifici de J2 transformat a capitalista");
            }

            Destroy(edificiBoAssociat);

            GameObject nouEdifici = Instantiate(
                edificiCapitalistaAssociat,
                pos,
                rot
            );
        }
    }
        
    public void SetPropietari(int valor, int valorEdifici)
    {
        Propietaria = valor;

        punts = valorEdifici;
    }

}