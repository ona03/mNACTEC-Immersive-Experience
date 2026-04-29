using UnityEngine;
// using System.Collections.Generic;
using TMPro; // Necessari per al component TextMeshProUGUI
using UnityEngine.UI; // Necessari per al component Image

public class S6A_sociabilitat : MonoBehaviour
{
    private sala6assignarText gestorTextos;
    private bool textMostrat = false; // Per assegurar-nos que només calculem i mostrem el text una vegada
    
    [Header("UI Elements")]
    public Image fons;
    public TextMeshProUGUI cas;
    public TextMeshProUGUI missatge;
    public GameObject Resum;

    //[Header("Per seleccionar el text a mostrar")]
    int animal;     // 1: Llop, 2: Àguila, 3: Ós, 4: Guineu
    int dirNo;      // et costa dir que no? 
    int gos_gat;    // 1: Gos, 2: Gat
    int opinio;     // canvies d’opinió?
    
    // Alta sociabilitat (dependència social)
    // Cas equilibrat
    // Baixa sociabilitat (independència)
    void Start()
    {
        gestorTextos = GetComponentInParent<sala6assignarText>();
        // Verificació de seguretat: si no el troba, ens avisarà amb un error clar
        if (gestorTextos == null) {
            Debug.LogError($"No s'ha trobat el script 'sala6assignarText' al pare de {gameObject.name}!");
        }

        if (cas != null) cas.gameObject.SetActive(false);
        if (missatge != null) missatge.gameObject.SetActive(false);
        // Debug.Log("S6A_sociabilitat inicialitzat, UI amagada.");
    }
    void Update()
    {
        if (cas == null || missatge == null || fons == null) return; // Seguretat per evitar errors si no s'han assignat els TMP al Inspector

        // Debug.Log($"Update S6A_sociabilitat: fons activeSelf={fons.gameObject.activeSelf}, textMostrat={textMostrat}");

        if (fons.gameObject.activeSelf && !textMostrat)
        {
            resumSala2 scriptS2 = Resum.GetComponent<resumSala2>();
            resumSala4 scriptS4 = Resum.GetComponent<resumSala4>();
            animal = scriptS2.preferencies[0];
            dirNo  = scriptS4.preguntesSala4[8].op;
            gos_gat = scriptS4.preguntesSala4[0].op;
            opinio = scriptS4.preguntesSala4[9].op;
            // animal = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[0];
            // dirNo = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[8].op;
            // gos_gat = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[0].op;
            // opinio = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[9].op;

            calcularDimensio();
            textMostrat = true; // Assegurem-nos que només es calcula i mostra el text una vegada
            // Debug.Log($"Fons obert i text mostrat per primera vegada. TextMostrat set a {textMostrat}.");
        }
        else if (!fons.gameObject.activeSelf && textMostrat)
        {
            // Si el fons es tanca, resetejem el control per permetre que es torni a calcular i mostrar el text quan s'obri de nou
            textMostrat = false;
            if (cas != null) cas.gameObject.SetActive(false);
            if (missatge != null) missatge.gameObject.SetActive(false);
            // Debug.Log("Fons tancat, text amagat.");
        }
    }

    public void calcularDimensio()
    {
        // if (animal == 0 || dirNo == 0 || gos_gat == 0 || opinio == 0) Debug.Log("valor 0"); // Esperem a tenir totes les dades abans de mostrar res

        // Debug.Log($"VALORS funció:  " +
        //   $"Animal: {animal} | " +
        //   $"Dir No: {dirNo} | " +
        //   $"Gos/Gat: {gos_gat} | " +
        //   $"Opinió: {opinio}");
        
        int sociabilitat = 0;

        // Animal (pes alt)
        if (animal == 1) sociabilitat += 2;      // Llop → sociable
        else if (animal == 4) sociabilitat += 1; // Guineu → flexible
        else if (animal == 2) sociabilitat -= 2; // Àguila → independent
        else if (animal == 3) sociabilitat -= 1; // Ós → independent moderat
        // Debug.Log($"Sociabilitat després de l'animal: {sociabilitat}");

        // Et costa dir que no?
        if (dirNo == 1) sociabilitat += 1;       // Sí → dependència
        else sociabilitat -= 1;                  // No → independència
        // Debug.Log($"Sociabilitat després de dirNo: {sociabilitat}");

        // Gos vs gat
        if (gos_gat == 1) sociabilitat += 1;     // Gos → sociable
        else sociabilitat -= 1;                  // Gat → independent
        // Debug.Log($"Sociabilitat després de gos_gat: {sociabilitat}");

        // Canvies d’opinió
        if (opinio == 1) sociabilitat += 1;      // Sí → influenciable
        else sociabilitat -= 1;                  // No → ferm
        // Debug.Log($"Sociabilitat després de opinio: {sociabilitat}");

        // [-5, 5]
        if (sociabilitat >= 2)
        {
            assignacioText(1);
        }
        else if (sociabilitat <= -2)
        {
            assignacioText(3);
        }
        else
        {
            assignacioText(2);
        }
    }
    void assignacioText(int id)
    {
        textSala6 d = gestorTextos.ObtenirText($"social_{id}");
        if (d != null)
        {
            cas.text = d.paragraf1;
            missatge.text = d.paragraf2;
            // Debug.Log($"Text assignat per social_{id}: {d.paragraf1} | {d.paragraf2}");
        }
        if (cas != null) cas.gameObject.SetActive(true);
        if (missatge != null) missatge.gameObject.SetActive(true);
        // Debug.Log($"Text mostrat per social_{id}");
    }
}
