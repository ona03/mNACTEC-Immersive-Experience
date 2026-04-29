using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UI_ImageController : MonoBehaviour
{
    // [SerializeField]
    public Image imageContainer; // L'objecte de la UI que conté la imatge
    public GameObject objecteTrigger; // L'objecte que actua com a "clau" (si està actiu, es veu la imatge)
    // private bool esperantPerObrir = true; // Control per no llançar l'espera molts cops
    // public int tempsEspera = 0; // Temps en segons abans d'obrir-se

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Comencem amb la imatge oculta
        if (imageContainer != null)
            imageContainer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Si el trigger s'activa i encara no s'ha mostrat la imatge
        if (objecteTrigger.gameObject.activeSelf)
        {
            if (!imageContainer.gameObject.activeSelf) // && esperantPerObrir)
            {
                // StartCoroutine(EsperarIObrir());
                imageContainer.gameObject.SetActive(true);
            }
        }
        else
        {
            imageContainer.gameObject.SetActive(false);
        }
    }

    // IEnumerator EsperarIObrir()
    // {
    //     esperantPerObrir = true; // Marquem que ja estem esperant
        
    //     yield return new WaitForSeconds(tempsEspera); // Aturem l'execució aquí X segons

    //     // Un cop passat el temps, si l'objecte segueix actiu, obrim
    //    if (objecteTrigger.GetComponent<MeshRenderer>().enabled)
    //     {
    //         imageContainer.gameObject.SetActive(true);
    //     }
        
    //     esperantPerObrir = false; 
    // }
}
