using UnityEngine;
// using System.Collections.Generic;
using TMPro; // Necessari per al component TextMeshProUGUI
using UnityEngine.UI; // Necessari per al component Image

public class S6F_entorn : MonoBehaviour
{
    private sala6assignarText gestorTextos;
    private bool textMostrat = false; // Per assegurar-nos que només calculem i mostrem el text una vegada
    
    [Header("UI Elements")]
    public Image fons;
    public TextMeshProUGUI cas;
    public TextMeshProUGUI missatge;
    public GameObject Resum;

    //[Header("Per seleccionar el text a mostrar")]
    // Sala 5
    float tempsTotal5;
    // Sala 2
    int estetica;       // 1: Geometria Perfecta, 2: Formes Desiguals, 3: Minimalisme Buit, 4: Mosaic Complex
    int pictoric;       // 1: cubisme, 2: dadaisme, 3: surrealisme, 4: fauvisme
    // Sala 5
    float tempsA, tempsAB, tempsB, tempsBA; // Temps que trigues a respondre les preguntes en diferents moments de la sala 5

    // Interpretació estructurada
    // Interpretació reactiva
    // Interpretació ambigua
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
            // tempsTotal5 = GameObject.Find("Resum").GetComponent<resumSala5>().tempsTotalSala5;
            // estetica = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[4];
            // pictoric = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[1];
            // tempsA = GameObject.Find("Resum").GetComponent<resumSala5>().tempsA;
            // tempsAB = GameObject.Find("Resum").GetComponent<resumSala5>().tempsAB;
            // tempsB = GameObject.Find("Resum").GetComponent<resumSala5>().tempsB;
            // tempsBA = GameObject.Find("Resum").GetComponent<resumSala5>().tempsBA;

            resumSala2 scriptS2 = Resum.GetComponent<resumSala2>();
            resumSala5 scriptS5 = Resum.GetComponent<resumSala5>();

            tempsTotal5 = scriptS5.tempsTotalSala5;
            estetica = scriptS2.preferencies[4];
            pictoric = scriptS2.preferencies[1];
            tempsA = scriptS5.tempsA;
            tempsAB = scriptS5.tempsAB;
            tempsB = scriptS5.tempsB;
            tempsBA = scriptS5.tempsBA;
            
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
        // if (tempsTotal5 == 0 || estetica == 0 || pictoric == 0 || tempsA == 0 || tempsAB == 0 || 
        // tempsB == 0 || tempsBA == 0) Debug.Log("valor 0");; // Esperem a tenir totes les dades abans de mostrar res

        // Debug.Log($"VALORS: " +
        //   $"Temps Total S5: {tempsTotal5} | " +
        //   $"Estètica: {estetica} | " +
        //   $"Pictòric: {pictoric} | " +
        //   $"Temps A: {tempsA} | " +
        //   $"Temps AB: {tempsAB} | " +
        //   $"Temps B: {tempsB} | " +
        //   $"Temps BA: {tempsBA}");

        float entorn = 0f;

        // Temps blocs sala 5
        if (tempsA < tempsB && tempsA < tempsAB && tempsA < tempsBA) entorn += 3; // Respon més ràpid al bloc A → interpretació estructurada
        else if (tempsB < tempsA && tempsB < tempsAB && tempsB < tempsBA) entorn -= 3; // Respon més ràpid al bloc B → interpretació reactiva

        // Estètica
        if (estetica == 1) entorn += 2; // Geometria → interpretació estructurada
        else if (estetica == 2) entorn -= 2; // Desiguals → interpretació orgànica
        else if (estetica == 3) entorn += 1; // Minimalisme → interpretació reactiva
        else if (estetica == 4) entorn -= 1; // Mosaic → interpretació ambigua

        // Moviment pictòric
        if (pictoric == 1) entorn += 2; // Cubisme → interpretació estructurada
        else if (pictoric == 2) entorn -= 2; // Dadaisme → interpretació disruptiva
        else if (pictoric == 3) entorn -= 0.5f; // Surrealisme → interpretació intuitiva
        else if (pictoric == 4) entorn -= 1; // Fauvisme → interpretació emocional

        // temps total sala 5
        if (tempsTotal5 > 90f) entorn += 0.5f;
        else if (tempsTotal5 < 50f) entorn -= 0.5f;

        // [-7.5, 7.5]
        
        if (entorn >= 2.5f)
        {
            assignacioText(1);
        }
        else if (entorn <= -2.5f)
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
        textSala6 d = gestorTextos.ObtenirText($"entorn_{id}");
        if (d != null)
        {
            cas.text = d.paragraf1;
            missatge.text = d.paragraf2;
            // Debug.Log($"Text assignat per entorn_{id}: {d.paragraf1} | {d.paragraf2}");
        }
        if (cas != null) cas.gameObject.SetActive(true);
        if (missatge != null) missatge.gameObject.SetActive(true);
        // Debug.Log($"Text mostrat per entorn_{id}");
    }
}
