using UnityEngine;

public class RaycastLook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float range = 5f; // Distància màxima de la mirada
    public LayerMask layerInteractuable; // Capa on estaran els objectes
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Es crea el raig des del centre de la pantalla
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Es llança el raig físic
        if (Physics.Raycast(ray, out hit, range, layerInteractuable))
        {
            // S'intenta trobar el component floatingObj en l'objecte impactat
            floatingObjRaycast obj = hit.collider.GetComponent<floatingObjRaycast>();

            if (obj != null)
            {
                // S'avisa a l'objecte que s'està mirant aquest frame
                obj.ActivarMovimentPerAquestFrame();
            }
        }
    }
}
