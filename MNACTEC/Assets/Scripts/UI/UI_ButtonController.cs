using UnityEngine;
using UnityEngine.UI; // Necessari per al component Button
using System.Collections;

public class UI_ButtonController : MonoBehaviour
{
    public Button botoContainer;      // L'objecte del Botó a la UI
    public GameObject objecteTrigger; // L'objecte que actua com a "clau" (si està actiu, es veu el botó)
    public int tempsEspera = 5; // Temps en segons abans d'obrir-se
    
    private float distanciaActiva = 4f; // Distància a la qual el trigger s'activa
    private float distanciaActual; // Per mostrar la distància actual a l'Editor

    private GameObject player;
    private int opcioTriada; // Aquí es guardarà la selecció final
    private bool jaSeleccionat = false; // Control per no llançar la espera molts cops

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (botoContainer != null) botoContainer.gameObject.SetActive(false);

        // BUSQUEM AUTOMÀTICAMENT ELS BOTONS FILLS
        UI_ButtonReturn[] botons = GetComponentsInChildren<UI_ButtonReturn>(true);
        foreach (UI_ButtonReturn b in botons)
        {
            // Ens subscrivim a l'esdeveniment de cada botó
            b.OnClickAmbValor += RebreResposta;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // distanciaActual = Vector3.Distance(player.transform.position, objecteTrigger.transform.position);

        if (player == null || objecteTrigger == null || jaSeleccionat) return;

        distanciaActual = Vector3.Distance(player.transform.position, objecteTrigger.transform.position);
        
        if (objecteTrigger.activeSelf && distanciaActual <= distanciaActiva)
        {
            if (!botoContainer.gameObject.activeSelf)
            {
                StartCoroutine(EsperarIObrir());
                // botoContainer.gameObject.SetActive(true);
                // Cursor.lockState = CursorLockMode.None;
                // Cursor.visible = true;
            }
        }
        else if (botoContainer.gameObject.activeSelf)
        {
            TancarMenu();
        }
    }

    IEnumerator EsperarIObrir()
    {
        // esperantPerObrir = true; // Marquem que ja estem esperant
        
        yield return new WaitForSeconds(tempsEspera); // Aturem l'execució aquí X segons

        // Un cop passat el temps, si l'objecte segueix actiu, obrim
       if (objecteTrigger.activeSelf && distanciaActual <= distanciaActiva && !jaSeleccionat)
        {
            botoContainer.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void RebreResposta(int valor) 
    {
        opcioTriada = valor;
        jaSeleccionat = true;
        // Debug.Log(gameObject.name + " ha rebut el valor: " + valor);
        TancarMenu();
    }
    void TancarMenu() 
    {
        botoContainer.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public int GetOpcioTriada() => opcioTriada;
}
