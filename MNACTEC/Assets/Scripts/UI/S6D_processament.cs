using UnityEngine;
// using System.Collections.Generic;
using TMPro; // Necessari per al component TextMeshProUGUI
using UnityEngine.UI; // Necessari per al component Image

public class S6D_processament : MonoBehaviour
{
    private sala6assignarText gestorTextos;
    private bool textMostrat = false; // Per assegurar-nos que només calculem i mostrem el text una vegada
    
    [Header("UI Elements")]
    public Image fons;
    public TextMeshProUGUI cas;
    public TextMeshProUGUI missatge;
    public GameObject Resum;

    //[Header("Per seleccionar el text a mostrar")]
    // Sala 4
    float tempsResposta;   // Temps que trigues a respondre les preguntes (en segons)
    int sobrepensa;     // Sobrepenses les coses?
    int improvisar;    // T’agrada improvisar?
    int moment;        // Et deixes portar pel moment?
    // Sala 5
    float tempsMig;     // sala 5
    int cami;           // sala 3

    // Processament alt (analític)
    // Processament mixt
    // Processament immediat (intuïtiu)
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
            // tempsResposta = GameObject.Find("Resum").GetComponent<resumSala4>().MitjaTempsResposta;
            // sobrepensa = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[10].op;
            // improvisar = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[7].op;
            // moment = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[6].op;
            // tempsMig = GameObject.Find("Resum").GetComponent<resumSala5>().MitjaTempsSala5;
            // cami = GameObject.Find("Resum").GetComponent<resumSala3>().cami;

            resumSala3 scriptS3 = Resum.GetComponent<resumSala3>();
            resumSala4 scriptS4 = Resum.GetComponent<resumSala4>();
            resumSala5 scriptS5 = Resum.GetComponent<resumSala5>();

            tempsResposta = scriptS4.MitjaTempsResposta;
            sobrepensa = scriptS4.preguntesSala4[10].op;
            improvisar = scriptS4.preguntesSala4[7].op;
            moment = scriptS4.preguntesSala4[6].op;
            tempsMig = scriptS5.MitjaTempsSala5;
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
        // if (tempsResposta == 0 || sobrepensa == 0 || improvisar == 0 || moment == 0 ||
        //     tempsMig == 0) Debug.Log("valor 0");; // Esperem a tenir totes les dades abans de mostrar res

        // Debug.Log($"VALORS: " +
        //   $"Temps Resposta S4: {tempsResposta} | " +
        //   $"Sobrepensa: {sobrepensa} | " +
        //   $"Improvisar: {improvisar} | " +
        //   $"Moment: {moment} | " +
        //   $"Temps Mig S5: {tempsMig} | " +
        //   $"Camí: {cami}");
        
        float proc = 0f;

        // Temps de resposta (ajusta llindars segons el teu sistema)
        if (tempsResposta > 3.5f) proc += 3;   // lent → processament alt
        else if (tempsResposta > 2.5f) proc += 2; // lent moderat
        else if (tempsResposta > 1.5f) proc -= 2; // ràpid → processament immediat
        else proc -= 3;                        // ràpid moderat → processament mixt

        // Sobrepensa vs actuar
        if (sobrepensa == 1) proc += 2;        // Sí → processament alt
        else proc -= 2;                      // No → processament immediat 

        // Improvisar vs planificar
        if (improvisar == 1) proc -= 2;       // Sí → processament immediat
        else proc += 2;                      // No → processament alt

        // Deixar-se portar vs controlar
        if (moment == 1) proc -= 2;           // Sí → processament immediat
        else proc += 2;                      // No → processament alt

        // Temps a les tasques de la Sala 5 (ajusta llindars segons el teu sistema)
        if (tempsMig > 2f) proc += 1; // Processament alt
        else if (tempsMig < 1f) proc -= 1; // Processament immediat

        // camí escollit
        if (cami == 1) proc -= 1;
        else if (cami == 2) proc += 0.5f;
        else proc += 1;

        // [-11, 11]
        if (proc > 4)
        {
            assignacioText(1);
        }
        else if (proc <= -4)
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
        textSala6 d = gestorTextos.ObtenirText($"processament_{id}");
        if (d != null)
        {
            cas.text = d.paragraf1;
            missatge.text = d.paragraf2;
            // Debug.Log($"Text assignat per proc_{id}: {d.paragraf1} | {d.paragraf2}");
        }
        if (cas != null) cas.gameObject.SetActive(true);
        if (missatge != null) missatge.gameObject.SetActive(true);
        // Debug.Log($"Text mostrat per proc_{id}");
    }
}
