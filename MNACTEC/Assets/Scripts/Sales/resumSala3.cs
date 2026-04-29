using UnityEngine;

public class resumSala3 : MonoBehaviour
{
    private GameObject infoSala3;
    [Header("SALA 3")]
    public int cami = 0;
    public GameObject[] path3 = new GameObject[3];

    // private bool sala3activa = false;
    private numPath ctrlCami;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infoSala3 = GameObject.Find("Sala3: El Camí");
        Transform tCami = infoSala3.transform.Find("Paths");
        if (tCami != null) ctrlCami = tCami.GetComponent<numPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cami != 0 || infoSala3 == null) return; // Si ja tenim camí triat, no cal seguir comprovant

        if (cami == 0)
        {
        bool sala3activa = false; // Reset cada frame!
        for (int i = 0; i < path3.Length; i++)
        {
            // Seguretat: comprova que l'element de l'array no estigui buit a l'inspector
            if (path3[i] != null && path3[i].activeSelf)
            {
                sala3activa = true;
                break; // Si un és actiu, ja no cal mirar els altres
            }
        }

        if (cami == 0 && ctrlCami != null && sala3activa)
        {
            int tria = ctrlCami.GetOpcio();
            if (tria != 0)            
            {
                cami = tria;
                // Debug.Log($"Cami triat: {cami}");
            }
        }
        }
        
    }
}
