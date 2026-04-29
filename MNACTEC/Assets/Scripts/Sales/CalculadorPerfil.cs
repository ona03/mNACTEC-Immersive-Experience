using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class CalculadorPerfil : MonoBehaviour
{
    [System.Serializable]
    public class PuntsPerfil {
        public int harmonic;
        public int analitic;
        public int impulsiu;
        public int sensible;
        public void Afegir(int h, int a, int i, int s) {
            harmonic += h; analitic += a; impulsiu += i; sensible += s;
        }
    }
    public PuntsPerfil resultatsTotals = new PuntsPerfil();
    // public string guanyador;
    // private meshRenderer mRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public string CalcularTot() {
        // Resetegem per si tornem a calcular
        resultatsTotals = new PuntsPerfil();

        // 1. RECOLLIR DADES DELS RESUMS
        var s2 = GameObject.Find("Resum").GetComponent<resumSala2>();
        var s3 = GameObject.Find("Resum").GetComponent<resumSala3>();
        var s4 = GameObject.Find("Resum").GetComponent<resumSala4>();
        var s5 = GameObject.Find("Resum").GetComponent<resumSala5>();

        // 2. PROCESSAR SALA 2 (preferencies[])
        ProcessarSala2(s2.preferencies);

        // 3. PROCESSAR SALA 3 (cami)
        ProcessarSala3(s3.cami);

        // 4. PROCESSAR SALA 4 (preguntesSala4[])
        ProcessarSala4(s4.preguntesSala4, s4.MitjaTempsResposta);

        // 5. PROCESSAR SALA 5 (estatFinal)
        ProcessarSala5(s5);

        return DeterminarPerfilGuanyador();
    }
    void ProcessarSala2(int[] p) {
        // Animals: 1:Llop, 2:Àguila, 3:Ós, 4:Guineu
        if (p[0] == 1) resultatsTotals.Afegir(3, 0, 0, 1);
        if (p[0] == 2) resultatsTotals.Afegir(0, 3, 2, 0);
        if (p[0] == 3) resultatsTotals.Afegir(2, 1, 0, 3);
        if (p[0] == 4) resultatsTotals.Afegir(0, 2, 3, 1);

        // Moviment: 1:Fauvisme, 2:Cubisme, 3:Dadaisme, 4:Surrealisme
        if (p[1] == 1) resultatsTotals.Afegir(0, 3, 0, 0);
        if (p[1] == 2) resultatsTotals.Afegir(0, 0, 3, 1);
        if (p[1] == 3) resultatsTotals.Afegir(2, 0, 0, 3);
        if (p[1] == 4) resultatsTotals.Afegir(3, 0, 2, 1);
        
        // p[2] (plantes)
        if (p[2] == 1) resultatsTotals.Afegir(2, 0, 0, 3);
        if (p[2] == 2) resultatsTotals.Afegir(0, 2, 0, 1);
        if (p[2] == 3) resultatsTotals.Afegir(3, 0, 1, 2);
        if (p[2] == 4) resultatsTotals.Afegir(0, 0, 3, 1);

        // p[3] (superpoders)
        if (p[3] == 1) resultatsTotals.Afegir(1, 2, 0, 3);
        if (p[3] == 2) resultatsTotals.Afegir(2, 0, 0, 3);
        if (p[3] == 3) resultatsTotals.Afegir(0, 0, 3, 1);
        if (p[3] == 4) resultatsTotals.Afegir(0, 3, 0, 1);

        // p[4] (estètica)
        if (p[4] == 1) resultatsTotals.Afegir(2, 3, 0, 0);
        if (p[4] == 2) resultatsTotals.Afegir(0, 0, 1, 2);
        if (p[4] == 3) resultatsTotals.Afegir(1, 2, 0, 1);
        if (p[4] == 4) resultatsTotals.Afegir(0, 0, 2, 3);
    }
    void ProcessarSala3(int cami) {
        // Camí: 1:Camí del mig, 2:Camí de l'esquerra, 3:Camí de la dreta
        if (cami == 1) resultatsTotals.Afegir(2, 0, 0, 1);
        if (cami == 2) resultatsTotals.Afegir(0, 2, 2, 0);
        if (cami == 3) resultatsTotals.Afegir(0, 0, 0, 3);
    }
    void ProcessarSala4(resumSala4.pSala4[] preguntes, float mitjaTemps) {      

        // T’agraden més els gossos que els gats?
        if (preguntes[0].op == 1) resultatsTotals.Afegir(1, 0, 1, 0); // Sí
        else resultatsTotals.Afegir(0, 1, 0, 1); // No

        // Tries la pizza abans que la pasta?
        if (preguntes[1].op == 1) resultatsTotals.Afegir(0, 0, 1, 1); // Sí
        else resultatsTotals.Afegir(1, 1, 0, 0); // No

        // Ets més de sèries que de pelis?
        if (preguntes[2].op == 1) resultatsTotals.Afegir(1, 1, 0, 0); // Sí
        else resultatsTotals.Afegir(0, 0, 1, 1); // No

        // Ets més de platja que de muntanya?
        if (preguntes[3].op == 1) resultatsTotals.Afegir(1, 0, 1, 0); // Sí
        else resultatsTotals.Afegir(0, 1, 0, 1); // No

        // Disfrutes provant esports nous?
        if (preguntes[4].op == 1) resultatsTotals.Afegir(0, 0, 1, 1); // Sí
        else resultatsTotals.Afegir(1, 1, 0, 0); // No

        // Prefereixes la rutina al canvi?
        if (preguntes[5].op == 1) resultatsTotals.Afegir(1, 1, 0, 0); // Sí
        else resultatsTotals.Afegir(0, 0, 1, 1); // No

        // Et deixes portar pel moment?
        if (preguntes[6].op == 1) resultatsTotals.Afegir(0, 0, 1, 1); // Sí
        else resultatsTotals.Afegir(1, 1, 0, 0); // No

        // T’agrada improvisar?
        if (preguntes[7].op == 1) resultatsTotals.Afegir(0, 0, 1, 1); // Sí
        else resultatsTotals.Afegir(1, 1, 0, 0); // No

        // Et costa dir que no?
        if (preguntes[8].op == 1) resultatsTotals.Afegir(1, 1, 0, 1); // Sí
        else resultatsTotals.Afegir(0, 0, 1, 0); // No

        // Canvies d’opinió fàcilment?
        if (preguntes[9].op == 1) resultatsTotals.Afegir(0, 0, 1, 1); // Sí
        else resultatsTotals.Afegir(1, 1, 0, 0); // No

        // Sobrepenses les coses?
        if (preguntes[10].op == 1) resultatsTotals.Afegir(0, 1, 0, 1); // Sí
        else resultatsTotals.Afegir(1, 0, 1, 0); // No

        // Ets més de dolç que de salat?
        if (preguntes[11].op == 1) resultatsTotals.Afegir(0, 0, 1, 1); // Sí
        else resultatsTotals.Afegir(1, 1, 0, 0); // No

        // Et fixes en els detalls?
        if (preguntes[12].op == 1) resultatsTotals.Afegir(1, 1, 0, 1); // Sí
        else resultatsTotals.Afegir(0, 0, 1, 0); // No

        // Et molesta el desordre?
        if (preguntes[13].op == 1) resultatsTotals.Afegir(1, 1, 0, 0); // Sí
        else resultatsTotals.Afegir(0, 0, 1, 1); // No

        // Temps de resposta
        if (mitjaTemps < 1.5f) resultatsTotals.Afegir(0, 0, 2, 0); // Ràpid
        else if (mitjaTemps > 3f) resultatsTotals.Afegir(0, 2, 0, 2); // Lent
        else resultatsTotals.Afegir(2, 0, 0, 2); // mitjana normal o respostes irregulars
    }

    void ProcessarSala5(resumSala5 s5) {
        // 1. Ens assegurem que la sala 5 hagi processat els seus objectes
        s5.ProcessarDadesSala5();

        // 2. Creem un diccionari temporal per comparar les mitjanes que JA estan calculades
        var mitjanes = new Dictionary<string, float>
        {
            { "A",  s5.tempsA },
            { "AB", s5.tempsAB },
            { "B",  s5.tempsB },
            { "BA", s5.tempsBA }
        };

        // 3. Trobem quina clau té el valor més alt
        string guanyador = "Cap";
        float valorMaxim = -1f;

        foreach (var entrada in mitjanes)
        {
            if (entrada.Value > valorMaxim)
            {
                valorMaxim = entrada.Value;
                guanyador = entrada.Key;
            }
        }

        // 4. Apliquem punts segons la teva taula
        switch (guanyador)
        {
            case "A":  resultatsTotals.Afegir(4, 0, 0, 0); break;
            case "AB": resultatsTotals.Afegir(0, 4, 0, 3); break;
            case "B":  resultatsTotals.Afegir(0, 0, 4, 0); break;
            case "BA": resultatsTotals.Afegir(0, 3, 3, 3); break;
        }
    }
    public string DeterminarPerfilGuanyador()
    {
        // 1. Creem la llista de puntuacions
        var llista = new List<KeyValuePair<string, int>>
        {
            new KeyValuePair<string, int>("h", resultatsTotals.harmonic),
            new KeyValuePair<string, int>("a", resultatsTotals.analitic),
            new KeyValuePair<string, int>("i", resultatsTotals.impulsiu),
            new KeyValuePair<string, int>("s", resultatsTotals.sensible)
        };

        // 2. Ordenem de major a menor
        llista = llista.OrderByDescending(x => x.Value).ToList();

        int maxPunts = llista[0].Value;

        // 3. Busquem els que tenen la puntuació màxima o com a màxim dos punts per sota
        // var guanyadors = llista.Where(x => x.Value == maxPunts).ToList();
        float llindar = maxPunts - 2;
        var guanyadors = llista.Where(x => x.Value >= llindar).ToList();

        // --- CAS A: EMPAT (2, 3 o 4 perfils amb la mateixa puntuació màxima) ---
        if (guanyadors.Count > 1)
        {
            // Ajuntem els noms amb un guionet, ex: "analitic_sensible" o "harmonic_impulsiu_sensible"
            string clauEmpat = string.Join("_", guanyadors.Select(x => x.Key));
            // Debug.Log("Resultat d'empat: " + clauEmpat);
            return $"hib_{clauEmpat}"; 
        }

        // --- CAS B: UN SOL GUANYADOR CLAR ---
        // Mirem el segon per si vols agafar els dos més alts (com demanaves abans)
        string primerID = llista[0].Key;
        int puntsPrimer = llista[0].Value;

        string segonID = llista[1].Key;
        int puntsSegon = llista[1].Value;

        if (puntsPrimer - puntsSegon < 6)
        {
            // Debug.Log($"Resultat complementari: {primerID} i {segonID}");
            return $"compl_{primerID}_{segonID}";
        }
        else 
        {
            return $"pur_{primerID}";
        }
    }
}
