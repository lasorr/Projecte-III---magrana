using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccioNivellAleatori : MonoBehaviour
{
    public string[] escenes;

    private static string ultimaEscena = "";

    public void CarregaNivellAleatori()
    {
        if (escenes.Length == 0) return;

        string NomEscena;
        int seguretat = 0;

        do
        {
            int randomIndex = Random.Range(0, escenes.Length);
            NomEscena = escenes[randomIndex];
            seguretat++;
        }
        while (NomEscena == ultimaEscena && seguretat < 10);

        ultimaEscena = NomEscena;

        SceneManager.LoadScene(NomEscena);

        Debug.Log("Carregant escena: " + NomEscena);
    }
}