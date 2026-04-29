using UnityEngine;

public class setActiveLightPath : MonoBehaviour
{
    public GameObject objecteARevisar; // L'objecte que s'ha de mostrar
    private Light llumComponent;
    private MeshRenderer rendererARevisar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Guardem el component de la llum d'aquest objecte
        llumComponent = GetComponent<Light>();

        // Obtenim el renderer de l'altre objecte per vigilar-lo
        if (objecteARevisar != null)
        {
            rendererARevisar = objecteARevisar.GetComponent<MeshRenderer>();
        }

        // Comencem amb la llum apagada per seguretat
        if (llumComponent != null)
            llumComponent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (llumComponent != null && rendererARevisar != null)
        {
            // La llum s'activa si el renderer de l'altre objecte està encès
            llumComponent.enabled = rendererARevisar.enabled;

            // float intensitatObjectiu = rendererARevisar.enabled ? 1.5f : 0f; // 1.5 és la potència de la llum
            // llumComponent.intensity = Mathf.MoveTowards(llumComponent.intensity, intensitatObjectiu, Time.deltaTime * 2f);
        }
    }
}
