using UnityEngine;
// using System.Collections.Generic;
using TMPro; // Necessari per al component TextMeshProUGUI
using UnityEngine.UI; // Necessari per al component Image

public class S6C_ordre : MonoBehaviour
{
    private sala6assignarText gestorTextos;
    private bool textMostrat = false; // Per assegurar-nos que només calculem i mostrem el text una vegada
    
    [Header("UI Elements")]
    public Image fons;
    public TextMeshProUGUI cas;
    public TextMeshProUGUI missatge;
    public GameObject Resum;

    //[Header("Per seleccionar el text a mostrar")]
    // Sala 2
    int estetica;       // 1: Geometria Perfecta, 2: Formes Desiguals, 3: Minimalisme Buit, 4: Mosaic Complex
    int pictoric;       // 1: cubisme, 2: dadaisme, 3: surrealisme, 4: fauvisme
    // Sala 4
    int desordre;          // Et molesta el desordre? 1: Sí, 2: No
    int detalls;        // Et fixes en els detalls? 1: Sí, 2: No
    int rutina;         // Prefereixes la rutina al canvi? 1: Sí, 2: No
    float tempsResposta;   // Temps que trigues a respondre les preguntes (en segons)
    // Sala 3
    int cami;           // 1: Natural, 2: Futurista, 3: Mina

    // Poca flexibilitat (necessitat d’ordre)
    // Estructura flexible
    // Flexibilitat (tolerància al caos)
    void Start()
    {
        gestorTextos = GetComponentInParent<sala6assignarText>();
        // Verificació de seguretat: si no el troba, ens avisarà amb un error clar
        if (gestorTextos == null) {
            Debug.LogError($"No s'ha trobat el script 'sala6assignarText' al pare de {gameObject.name}!");
        }

        if (cas != null) cas.gameObject.SetActive(false);
        if (missatge != null) missatge.gameObject.SetActive(false);
    }
    void Update()
    {
        if (cas == null || missatge == null || fons == null) return; // Seguretat per evitar errors si no s'han assignat els TMP al Inspector

        if (fons.gameObject.activeSelf && !textMostrat)
        {
            // estetica = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[4];
            // pictoric = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[1];
            // desordre = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[13].op;
            // detalls = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[12].op;
            // rutina = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[5].op;
            // tempsResposta = GameObject.Find("Resum").GetComponent<resumSala4>().MitjaTempsResposta;
            // cami = GameObject.Find("Resum").GetComponent<resumSala3>().cami;

            resumSala2 scriptS2 = Resum.GetComponent<resumSala2>();
            resumSala3 scriptS3 = Resum.GetComponent<resumSala3>();
            resumSala4 scriptS4 = Resum.GetComponent<resumSala4>();

            estetica = scriptS2.preferencies[4];
            pictoric = scriptS2.preferencies[1];
            desordre = scriptS4.preguntesSala4[13].op;
            detalls = scriptS4.preguntesSala4[12].op;
            rutina = scriptS4.preguntesSala4[5].op;
            tempsResposta = scriptS4.MitjaTempsResposta;
            cami = scriptS3.cami;
            
            calcularDimensio();
            textMostrat = true; // Assegurem-nos que només es calcula i mostra el text una vegada
        }
        else if (!fons.gameObject.activeSelf && textMostrat)
        {
            // Si el fons es tanca, resetejem el control per permetre que es torni a calcular i mostrar el text quan s'obri de nou
            textMostrat = false;
            if (cas != null) cas.gameObject.SetActive(false);
            if (missatge != null) missatge.gameObject.SetActive(false);
        }
    }

    public void calcularDimensio()
    {
        // if (estetica == 0 || pictoric == 0 || desordre == 0 || detalls == 0 
        // || rutina == 0 || tempsResposta == 0 || cami == 0) Debug.Log("valor 0");; // Esperem a tenir totes les dades abans de mostrar res
        
        // Debug.Log($"ALTRES VALORS: " +
        //   $"Estètica: {estetica} | " +
        //   $"Pictòric: {pictoric} | " +
        //   $"Desordre: {desordre} | " +
        //   $"Detalls: {detalls} | " +
        //   $"Rutina: {rutina} | " +
        //   $"Temps Resposta: {tempsResposta} | " +
        //   $"Camí: {cami}");

        float ordre = 0f;

        // Estètica (pes alt)
        if (estetica == 1) ordre += 2;      // Geometria Perfecta → necessitat d'ordre
        else if (estetica == 2) ordre -= 2; // Formes Desiguals → flexibilitat
        else if (estetica == 3) ordre += 1; // Minimalisme Buit → certa flexibilitat
        else if (estetica == 4) ordre -= 1; // Mosaic Complex → certa tolerància al caos
        
        // Pictòric
        if (pictoric == 1) ordre += 2;      // Cubisme → necessitat d'ordre
        else if (pictoric == 2) ordre -= 2; // Dadaisme → flexibilitat
        else if (pictoric == 3) ordre -= 1.5f; // Surrealisme → certa flexibilitat
        else if (pictoric == 4) ordre -= 1; // Fauvisme → certa tolerància al caos

        // Et molesta el desordre?
        if (ordre == 1) ordre += 2;       // Sí → necessitat d'ordre
        else ordre -= 2;                  // No → flexibilitat

        // Et fixes en els detalls?
        if (detalls == 1) ordre += 1;     // Sí → necessitat d'ordre
        else ordre -= 1;                  // No → flexibilitat

        // Prefereixes la rutina al canvi?
        if (rutina == 1) ordre += 1;      // Sí → necessitat d'ordre
        else ordre -= 1;                  // No → flexibilitat

        // Temps de resposta (ajusta llindars segons el teu sistema)
        if (tempsResposta > 3.5f) ordre += 1.5f;       // lent → necessitat d'ordre
        else if (tempsResposta > 2.5f) ordre += 1;  // lent moderat
        else if (tempsResposta > 1.5f) ordre -= 1;    // ràpid moderat
        else ordre -= 1.5f;                            // ràpid → flexibilitat 

        // Camí preferit
        if (cami == 2) ordre += 0.5f;            // Futurista → certa necessitat d'ordre
        else if (cami == 3) ordre -= 0.5f;       // Mina → flexibilitat

        // [-9, 9]
        if (ordre >= 3)
        {
            assignacioText(1);
        }
        else if (ordre <= -3)
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
        textSala6 d = gestorTextos.ObtenirText($"ordre_{id}");
        if (d != null)
        {
            cas.text = d.paragraf1;
            missatge.text = d.paragraf2;
            // Debug.Log($"Text assignat per ordre_{id}: {d.paragraf1} | {d.paragraf2}");
        }
        if (cas != null) cas.gameObject.SetActive(true);
        if (missatge != null) missatge.gameObject.SetActive(true);
        // Debug.Log($"Text mostrat per ordre_{id}");
    }
}
