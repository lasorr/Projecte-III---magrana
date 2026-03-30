using UnityEngine;

[ExecuteInEditMode] // Esto hará que funcione incluso sin darle al Play
public class FixCurvature : MonoBehaviour
{
    void OnEnable()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf != null && mf.sharedMesh != null)
        {
            // Creamos una caja invisible enorme (1000 unidades)
            // para que Unity crea que el objeto ocupa todo el horizonte
            mf.sharedMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
        }
    }
}