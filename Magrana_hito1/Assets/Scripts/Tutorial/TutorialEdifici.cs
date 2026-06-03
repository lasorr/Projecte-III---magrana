using UnityEngine;

public class TutorialEdifici : MonoBehaviour
{
    public GameObject edificiCapitalista;
    public GameObject edificiComunista;

    public GameObject imagen1Golpe;
    public GameObject imagen2Golpe;
    public GameObject imagen3Golpe;

    public GameObject particulasBrillo;

    private int contadorGolpes = 0;
    private const int golpesNecesarios = 3;


    void OnCollisionEnter(Collision collision)
    {
        // Comprobar si colisiona con "Arma_1" o "Arma_2" (como en KeyTutorial)
        if (collision.gameObject.CompareTag("Arma_1") || collision.gameObject.CompareTag("Arma_2"))
        {
            RecibirGolpe();
        }
    }

    void RecibirGolpe()
    {
        contadorGolpes++;
        MostrarImagenGolpe();

        // Si llegamos a 3 golpes, transformar el edificio
        if (contadorGolpes >= golpesNecesarios)
        {
            TransformarEdificio();
        }
    }

    void MostrarImagenGolpe()
    {
        Vector3 posicion = transform.position + new Vector3(0, 3f, 0);
        GameObject imagenAMostrar = null;

        switch (contadorGolpes)
        {
            case 1:
                imagenAMostrar = imagen1Golpe;
                break;
            case 2:
                imagenAMostrar = imagen2Golpe;
                break;
            case 3:
                imagenAMostrar = imagen3Golpe;
                break;
        }

        if (imagenAMostrar != null)
        {
            GameObject img = Instantiate(imagenAMostrar, posicion, Quaternion.identity);
            Destroy(img, 1f);
        }
    }

    void TransformarEdificio()
    {
        GameObject brillli = Instantiate(particulasBrillo, transform.position, Quaternion.identity);
        Destroy(brilli, 2f);
        Debug.Log("¡Edificio transformado a bueno!");

        Vector3 pos = edificiCapitalista.transform.position;
        Quaternion rot = edificiCapitalista.transform.rotation;

        Instantiate(edificiComunista, pos, rot);
        Destroy(edificiCapitalista);
        Destroy(gameObject);
    }
}