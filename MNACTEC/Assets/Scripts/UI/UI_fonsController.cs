using UnityEngine;
using UnityEngine.UI; // Necessari per al component Image
using System.Collections;

public class UI_fonsController : MonoBehaviour
{
    public Image imageContainer;
    public GameObject objecteTrigger; // L'objecte que actua com a "clau" (si està actiu, es veu la imatge)
    private GameObject player;
    public float distanciaActiva = 5f; // Distància a la qual el trigger s'activa
    private float distanciaActual; // Per mostrar la distància actual a l'Editor

    private bool fonsActiu = false; // Control per no llançar l'espera molts cops
    public int tempsEspera = 1; // Temps en segons que espera per obrir-se
    public int tempsObert = 5; // Temps en segons que ha d'estar obert abans de tancar-se automàticament


    private System.DateTime time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // Comencem amb la imatge oculta
        if (imageContainer != null) imageContainer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        distanciaActual = Vector3.Distance(player.transform.position, objecteTrigger.transform.position);
        // obrirFons(distanciaActual <= distanciaActiva);

        if (objecteTrigger.GetComponent<MeshRenderer>().enabled && !fonsActiu && distanciaActual <= distanciaActiva)
        {
            if (!imageContainer.gameObject.activeSelf)
             {
                StartCoroutine(EsperarIObrir());
                // Debug.Log(fonsActiu + ". Fons activat a les " + time.ToString("hh:mm:ss") + " per estar a una distància de " + distanciaActual.ToString("F2") + " del trigger.");
             }
        }

        System.TimeSpan dif = System.DateTime.Now - time;
        
        if (dif.TotalSeconds > tempsObert && fonsActiu)
        {
            imageContainer.gameObject.SetActive(false);
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
            // fonsActiu = false;
            // Debug.Log(fonsActiu + ". Fons desactivat a les " + System.DateTime.Now.ToString("hh:mm:ss") + " després de " + dif.TotalSeconds.ToString("F2") + " segons.");
        }
    }

    IEnumerator EsperarIObrir()
    {        
        yield return new WaitForSeconds(tempsEspera); // Aturem l'execució aquí X segons

        // Un cop passat el temps, si l'objecte segueix actiu, obrim
       if (objecteTrigger.GetComponent<MeshRenderer>().enabled && !fonsActiu && distanciaActual <= distanciaActiva)
        {
            imageContainer.gameObject.SetActive(true);
            time = System.DateTime.Now;
            fonsActiu = true;
            // Cursor.lockState = CursorLockMode.None;
            // Cursor.visible = true;
            // Debug.Log(fonsActiu + ". Fons activat a les " + time.ToString("hh:mm:ss") + " per estar a una distància de " + distanciaActual.ToString("F2") + " del trigger.");
        }
    }
}
