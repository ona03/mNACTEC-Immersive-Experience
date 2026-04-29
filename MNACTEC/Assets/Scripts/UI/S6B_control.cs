using UnityEngine;
using TMPro; // Necessari per al component TextMeshProUGUI
using UnityEngine.UI; // Necessari per al component Imag

public class S6B_control : MonoBehaviour
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
    int rutina;        // Prefereixes la rutina al canvi?1 = Sí (rutina), 2 = No (canvi)
    int improvisar;    // T’agrada improvisar?1 = Sí, 2 = No
    int moment;        // Et deixes portar pel moment? 1 = Sí, 2 = No
    int opinio;        // Canvies d’opinió fàcilment? 1 = Sí, 2 = No
    float tempsResposta; // temps mitjà
    // Sala 3
    int cami;          // 1 = Natural, 2 = Futurista, 3 = Mina
    // Sala 2
    int planta;        // 1 = Rosa, 2 = Cactus, 3 = Bambú, 4 = Heura
    int estetica;      // 1 = Geometria, 2 = Formes, 3 = Minimalisme, 4 = Mosaic
    // Sala 5
    float tempsSala5;  // temps total
    // Alt control (planificació)
    // Equilibri control–espontaneïtat
    // Baix control (impulsivitat)
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
            // rutina = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[5].op;
            // improvisar = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[7].op;
            // moment = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[6].op;
            // opinio = GameObject.Find("Resum").GetComponent<resumSala4>().preguntesSala4[9].op;
            // tempsResposta = GameObject.Find("Resum").GetComponent<resumSala4>().MitjaTempsResposta; 
            // cami = GameObject.Find("Resum").GetComponent<resumSala3>().cami;
            // planta = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[2];
            // estetica = GameObject.Find("Resum").GetComponent<resumSala2>().preferencies[4];
            // tempsSala5 = GameObject.Find("Resum").GetComponent<resumSala5>().tempsTotalSala5;

            resumSala2 scriptS2 = Resum.GetComponent<resumSala2>();
            resumSala3 scriptS3 = Resum.GetComponent<resumSala3>();
            resumSala4 scriptS4 = Resum.GetComponent<resumSala4>();
            resumSala5 scriptS5 = Resum.GetComponent<resumSala5>();

            // 2. Assignació de dades optimitzada
            rutina = scriptS4.preguntesSala4[5].op;
            improvisar = scriptS4.preguntesSala4[7].op;
            moment = scriptS4.preguntesSala4[6].op;
            opinio = scriptS4.preguntesSala4[9].op;
            tempsResposta = scriptS4.MitjaTempsResposta;
            cami = scriptS3.cami;
            planta = scriptS2.preferencies[2];
            estetica = scriptS2.preferencies[4];
            tempsSala5 = scriptS5.tempsTotalSala5;

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
        // if (rutina == 0 || improvisar == 0 || moment == 0 || 
        // opinio == 0 || tempsResposta == 0 || cami == 0 || planta == 0 || 
        // estetica == 0 || tempsSala5 == 0) Debug.Log("valor 0");; // Esperem a tenir totes les dades abans de mostrar res

        // Debug.Log("VALORS ASSIGNATS: " + 
        //   "Rutina: " + rutina + 
        //   " | Improvisar: " + improvisar + 
        //   " | Moment: " + moment + 
        //   " | Opinio: " + opinio + 
        //   " | Temps Mig: " + tempsResposta + 
        //   " | Cami: " + cami + 
        //   " | Planta: " + planta + 
        //   " | Estetica: " + estetica + 
        //   " | Temps S5: " + tempsSala5);
        
        float control = 0f;

        // SALA 4 — ALTA RELLEVÀNCIA

        // Temps de resposta (ajusta llindars segons el teu sistema)
        if (tempsResposta > 3.5f) control += 2;         // lent → control
        else if (tempsResposta > 2.5f) control += 1;    // lent moderat
        else if (tempsResposta > 1.5f) control -= 1;      // ràpid moderat
        else control -= 2;                              // ràpid → impulsivitat

        // Rutina vs canvi
        if (rutina == 1) control += 2;            // rutina → control
        else control -= 2;                        // canvi → impuls

        // Improvisar
        if (improvisar == 1) control -= 2;        // improvisar → impuls
        else control += 2;                        // no → planificació

        // Deixar-se portar
        if (moment == 1) control -= 2;            // sí → impuls
        else control += 2;                        // no → control

        // Canviar opinió (mitjana)
        if (opinio == 1) control -= 1;            // sí → flexibilitat/impuls
        else control += 1;                        // no → fermesa


        // SALA 3 — RELLEVÀNCIA MITJANA
        if (cami == 3) control -= 1;              // mina → poc control, incertesa
        else if (cami == 2) control += 1;         // futurista → control estructurat
        // else if (cami == 1) control -= 0;         // natural → neutre (estabilitat)


        // SALA 2 — RELLEVÀNCIA BAIXA

        // Plantes
        if (planta == 1) control += 0.5f;            // rosa → entorn controlat
        // else if (planta == 2) control += 1;       // cactus → autosuficiència
        // else if (planta == 3) control -= 1;       // bambú → adaptabilitat
        else if (planta == 4) control -= 0.5f;       // heura → expansió

        // Estètica
        if (estetica == 1) control += 0.5f;          // geometria → control
        // else if (estetica == 3) control += 1;     // minimalisme → control
        else if (estetica == 2) control -= 0.5f;     // formes → fluïdesa
        // else if (estetica == 4) control -= 1;     // mosaic → complexitat


        // SALA 5 — RELLEVÀNCIA BAIXA

        if (tempsSala5 > 90f) control += 0.5f;       // més temps → més reflexió
        else control -= 0.5f;                        // ràpid → impuls

        // [-11.5, 11.5]
        if (control >= 4)
        {
            assignacioText(1);
        }
        else if (control <= -4)
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
        textSala6 d = gestorTextos.ObtenirText($"control_{id}");
        if (d != null)
        {
            cas.text = d.paragraf1;
            missatge.text = d.paragraf2;
            // Debug.Log($"Text assignat per control_{id}: {d.paragraf1} | {d.paragraf2}");            
        }
        if (cas != null) cas.gameObject.SetActive(true);
        if (missatge != null) missatge.gameObject.SetActive(true);
        // Debug.Log($"Text mostrat per control?{id}");
    }
}
