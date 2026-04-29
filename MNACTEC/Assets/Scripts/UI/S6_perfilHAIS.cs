using UnityEngine;
// using System.Collections.Generic;
using TMPro; // Necessari per al component TextMeshProUGUI
using UnityEngine.UI; // Necessari per al component Image

public class S6_perfilHAIS : MonoBehaviour
{
    private sala6assignarText gestorTextos;
    private bool textAssignat = false; //, textMostrat = false; // Per assegurar-nos que només calculem i mostrem el text una vegada
    private MeshRenderer mrenderer;
    
    [Header("UI Elements")]
    public Image fons1;
    public TextMeshProUGUI casHAIS1;
    public TextMeshProUGUI missatge1;
    public Image fons2;
    public TextMeshProUGUI casHAIS2;
    public TextMeshProUGUI missatge2;

    //[Header("Per seleccionar el text a mostrar")]
    private CalculadorPerfil scriptPerfilHAIS;
    public string HAIS;

    void Start()
    {
        gestorTextos = GetComponentInParent<sala6assignarText>();
        scriptPerfilHAIS = GameObject.Find("Resum").GetComponent<CalculadorPerfil>();
        mrenderer = GameObject.Find("PathSala6").GetComponent<MeshRenderer>();
        // Verificació de seguretat: si no el troba, ens avisarà amb un error clar
        if (gestorTextos == null) {
            Debug.LogError($"No s'ha trobat el script 'sala6assignarText' al pare de {gameObject.name}!");
        }

        // if (cas != null) cas.gameObject.SetActive(false);
        // if (missatge != null) missatge.gameObject.SetActive(false);
        if (casHAIS1 != null) casHAIS1.gameObject.SetActive(false);
        if (missatge1 != null) missatge1.gameObject.SetActive(false);
        if (casHAIS2 != null) casHAIS2.gameObject.SetActive(false);
        if (missatge2 != null) missatge2.gameObject.SetActive(false);

    }
    void Update()
    {
        if (casHAIS1 == null || casHAIS2 == null || missatge1 == null || missatge2 == null || fons1 == null || fons2 == null) return;

        // 1. CÀLCUL (Només un cop quan el render s'activa)
        if (!textAssignat && mrenderer.enabled)
        {
            HAIS = scriptPerfilHAIS.CalcularTot();
            textAssignat = true; 
            casPerfil();
        }

        // 2. CONTROL DE VISIBILITAT (Independent per a cada fons)
        // Control Fons 1
        if (fons1.gameObject.activeSelf) {
            if (!casHAIS1.gameObject.activeSelf) { // Si el fons està ON però el text OFF, l'activem
                casHAIS1.gameObject.SetActive(true);
                missatge1.gameObject.SetActive(true);
            }
        } else {
            if (casHAIS1.gameObject.activeSelf) { // Si el fons està OFF però el text ON, l'apaguem
                casHAIS1.gameObject.SetActive(false);
                missatge1.gameObject.SetActive(false);
            }
        }

        // Control Fons 2
        if (fons2.gameObject.activeSelf) {
            if (!casHAIS2.gameObject.activeSelf) {
                casHAIS2.gameObject.SetActive(true);
                missatge2.gameObject.SetActive(true);
            }
        } else {
            if (casHAIS2.gameObject.activeSelf) {
                casHAIS2.gameObject.SetActive(false);
                missatge2.gameObject.SetActive(false);
            }
        }
    }
    void casPerfil()
    {
        if (string.IsNullOrEmpty(HAIS)) return;

        // 1. Tallem el string per saber el prefix i quins perfils hi ha
        string[] parts = HAIS.Split('_');
        string tipus = parts[0]; // "pur", "barreja" o "empat"

        if (tipus == "pur")
        {
            // // TEXT 1: Títol genèric de "Perfil Pur"
            // AssignarText1("perfil_pur"); 
            // // TEXT 2: Contingut específic del perfil (ex: "pur_analitic")
            // AssignarText2($"pur_{parts[1]}");

            AssignarText("perfil_pur", $"pur_{parts[1]}"); 
        }
        else if (tipus == "compl")
        {
            // // TEXT 1: El perfil dominant (ex: "pur_analitic")
            // AssignarText1($"pur_{parts[1]}");
            // // TEXT 2: El perfil secundari que està a prop (ex: "pur_sensible")
            // AssignarText2($"compl_{parts[2]}");

            AssignarText($"pur_{parts[1]}", $"compl_{parts[2]}");
        }
        else if (tipus == "hib")
        {
            // // TEXT 1: Títol genèric de "Perfil Híbrid / Empat"
            // AssignarText1("perfil_hib");
            // // TEXT 2: Explicació de la combinació (ex: "hib_analitic_sensible")
            // AssignarText2(HAIS);

            AssignarText("perfil_hib", HAIS);
        }
    }
    void AssignarText(string idCerca1, string idCerca2)
    {
        textSala6 d1 = gestorTextos.ObtenirText(idCerca1);
        textSala6 d2 = gestorTextos.ObtenirText(idCerca2);
        if (d1 != null)
        {
            casHAIS1.text = d1.paragraf1;
            missatge1.text = d1.paragraf2;
        }
        if (d2 != null)
        {
            casHAIS2.text = d2.paragraf1;
            missatge2.text = d2.paragraf2;
        }
    }
}
