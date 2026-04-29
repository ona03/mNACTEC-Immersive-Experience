using UnityEngine;
// using System.Collections.Generic;
using TMPro; // Necessari per al component TextMeshProUGUI
using UnityEngine.UI; // Necessari per al component Image

public class S6E_aspiracions : MonoBehaviour
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
    int superpoder;         // 1: Invisibilitat, 2: Telepatia, 3: Volar, 4: Domini del temps
    // Sala 3
    int cami;              // 1: Natural, 2: Futurista, 3: Mina
    // Sala 2
    int animals;           // 1: Llop, 2: Àguila, 3: Ós, 4: Guineu
    int mov_pictoric;       // 1: cubisme, 2: dadaisme, 3: surrealisme, 4: fauvisme

    // Protecció i observació
    // Connexió i empatia
    // Llibertat i exploració
    // Control i perfecció
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
            // superpoder = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[3];
            // cami = GameObject.Find("Resum").GetComponent<resumSala3>().cami;
            // animals = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[0];
            // mov_pictoric = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[1];

            resumSala2 scriptS2 = Resum.GetComponent<resumSala2>();
            resumSala3 scriptS3 = Resum.GetComponent<resumSala3>();

            superpoder = scriptS2.preferencies[3];
            cami = scriptS3.cami;
            animals = scriptS2.preferencies[0];
            mov_pictoric = scriptS2.preferencies[1];

            calcularDimensio();
            textMostrat = true; // Assegurem-nos que només es calcula i mostra el text una vegada
            // Debug.Log($"mostrat {textMostrat}");
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
        // if (superpoder == 0 || cami == 0 || animals == 0 || mov_pictoric == 0) Debug.Log("valor 0");; // Esperem a tenir totes les dades abans de mostrar res

        // Debug.Log($"VALORS: " +
        //   $"Superpoder: {superpoder} | " +
        //   $"Camí: {cami} | " +
        //   $"Animals: {animals} | " +
        //   $"Moviment Pictòric: {mov_pictoric}");
        
        float asp = 0f;

        // Superpoder (pes alt)
        if (superpoder == 1) asp += 1.5f; // Protecció i observació 
        else if (superpoder == 2) asp -= 1.5f; // Connexió i empatia 
        else if (superpoder == 3) asp -= 3; // o Llibertat i exploració
        else if (superpoder == 4) asp += 3; // o Control i perfecció
        
        // Camí
        if (cami == 1) asp -= -1; // Natural → connexió
        else if (cami == 2) asp += 2; // Futurista → llibertat
        else if (cami == 3) asp -= 2; // Mina → equilibri

        // Animals
        if (animals == 1) asp += 1; // Llop 
        else if (animals == 2) asp -= 1.5f; // Àguila 
        else if (animals == 3) asp -= 1; // Ós 
        else if (animals == 4) asp += 1.5f; // Guineu 

        // Moviment pictòric
        if (mov_pictoric == 1) asp += 1; // Cubisme → connexió
        else if (mov_pictoric == 2) asp -= 1; // Dadaisme → llibertat
        else if (mov_pictoric == 3) asp += 0.5f; // Surrealisme → equilibri
        else if (mov_pictoric == 4) asp -= 0.5f; // Fauvisme → protecció

        // [-6.5, 6.5]

        if (asp >= 3.25f)
        {
            assignacioText(1);
        }
        else if (asp >= 0)
        {
            assignacioText(2);
        }
        else if (asp <= -3.25)
        {
            assignacioText(4);
        }
        else
        {
            assignacioText(3);
        }
    }
    void assignacioText(int id)
    {
        textSala6 d = gestorTextos.ObtenirText($"aspiracions_{id}");
        if (d != null)
        {
            cas.text = d.paragraf1;
            missatge.text = d.paragraf2;
            // Debug.Log($"Text assignat per asp_{id}: {d.paragraf1} | {d.paragraf2}");
        }
        if (cas != null) cas.gameObject.SetActive(true);
        if (missatge != null) missatge.gameObject.SetActive(true);
        // Debug.Log($"Text mostrat per asp_{id}");
    }
}
