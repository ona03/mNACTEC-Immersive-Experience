using UnityEngine;
using TMPro;
using System.Collections;

public class UI_InputFieldController : MonoBehaviour
{
    public TMP_InputField inputFieldContainer; // Referència específica al component
    public GameObject objecteTrigger;
    public int tempsEspera = 5; // Temps en segons abans d'obrir-se
    
    private float distanciaActiva = 4f; // Distància a la qual el trigger s'activa
    private float distanciaActual; // Per mostrar la distància actual a l'Editor

    private GameObject player;
    private string textFinal;
    private bool jaSHeEscrit = false;
    private bool shaObert = false; // Control per no llançar l'espera molts cops

    void Start()
    {
        // Trobem el jugador
        player = GameObject.FindWithTag("Player");

        // Per amagar-ho tot (fons, cursor i text), desactivem el GameObject sencer
        if (inputFieldContainer != null)
            inputFieldContainer.gameObject.SetActive(false);

        inputFieldContainer.onEndEdit.AddListener(GuardarText);
    }

    void Update()
    {
        // Calculem la distància entre el jugador i l'objecte
        distanciaActual = Vector3.Distance(player.transform.position, objecteTrigger.transform.position);
        
        // Condició per obrir: l'objecte trigger s'activa i l'usuari encara no ha escrit el nom
        if (objecteTrigger.gameObject.activeSelf && distanciaActual <= distanciaActiva && !shaObert && !jaSHeEscrit)
        {
            if (!inputFieldContainer.gameObject.activeSelf)
            {
                StartCoroutine(EsperarIObrir());
            }
        }

        if (!objecteTrigger.activeSelf && shaObert)
        {
            StopAllCoroutines();
            // shaObert = false;
        }
    }
    IEnumerator EsperarIObrir()
    {       
        yield return new WaitForSeconds(tempsEspera); // Aturem l'execució aquí X segons

        // Un cop passat el temps, si l'objecte segueix actiu, obrim
       if (objecteTrigger.GetComponent<MeshRenderer>().enabled && distanciaActual <= distanciaActiva && !jaSHeEscrit)
        {
            ObrirInput();
        }
    }
    void ObrirInput()
    {
        shaObert = true; // Marquem que ja s'ha obert per evitar que es torni a obrir
        inputFieldContainer.gameObject.SetActive(true);
        inputFieldContainer.ActivateInputField();
        inputFieldContainer.text = ""; // Ens assegurem que estigui buit al principi
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Aquesta funció s'executa AUTOMÀTICAMENT quan l'usuari prem ENTER
    void GuardarText(string textEntrat)
    {
        // Només ho donem per vàlid si no està buit
        if (!string.IsNullOrWhiteSpace(textEntrat))
        {
            textFinal = textEntrat;
            // Debug.Log("Text guardat correctament: " + textFinal);
            
            jaSHeEscrit = true; // Això farà que no es torni a obrir
            TancarInput();
        }
    }

    void TancarInput()
    {
        inputFieldContainer.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public string GetTextFinal() { return textFinal; }
}