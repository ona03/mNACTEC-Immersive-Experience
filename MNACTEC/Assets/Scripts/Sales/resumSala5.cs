using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Necessari per fer càlculs fàcilment

public class resumSala5 : MonoBehaviour
{
    // private GameObject infoSala5, path5;
    [Header("SALA 5")]
    private floatingObjRaycast[] totsElsObjectes;
    [System.Serializable]
    public class ResultatGrup
    {
        public float tempsTotal;
        public float mitjana;
        public int quantitatObjectes;
    }

    // Llista de categories permeses (pots omplir-la des de l'Inspector)
    // [Header("Configuració de Grups Permesos")]
    private string[] categoriesOficials = new string[4] { "A", "AB", "B", "BA" };

    // Diccionari per emmagatzemar la manipulació de dades
    public Dictionary<string, ResultatGrup> dadesProcessades = new Dictionary<string, ResultatGrup>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Busquem tots els objectes que tenen l'script de flotació a l'escena
        totsElsObjectes = Object.FindObjectsByType<floatingObjRaycast>(FindObjectsInactive.Exclude);

        foreach (string categoria in categoriesOficials)
        {
            if (!string.IsNullOrEmpty(categoria) && !dadesProcessades.ContainsKey(categoria))
            {
                dadesProcessades.Add(categoria, new ResultatGrup());
                // Debug.Log($"S'ha afegit la categoria '{categoria}' al diccionari de dades processades.");
            }
        }

        // Debug.Log($"S'han trobat {totsElsObjectes.Length} objectes flotants.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessarDadesSala5()
    {
        if (totsElsObjectes == null || totsElsObjectes.Length == 0) return;
        
        // 2. Resetegem els valors dels grups existents (sense esborrar les claus)
        foreach (var grup in dadesProcessades.Values)
        {
            grup.tempsTotal = 0;
            grup.quantitatObjectes = 0;
            grup.mitjana = 0;
        }

        foreach (var obj in totsElsObjectes)
        {
            if (obj != null && obj.CompareTag("panell sala5"))
            {
                string grupDeLObjecte = obj.nomGrup;

                // 3. COMPROVACIÓ CRÍTICA: Només si la categoria ja existeix al diccionari
                if (dadesProcessades.ContainsKey(grupDeLObjecte))
                {
                    dadesProcessades[grupDeLObjecte].tempsTotal += obj.GetTempsSentApuntat();
                    dadesProcessades[grupDeLObjecte].quantitatObjectes++;
                    // Debug.Log($"Objecte del grup '{grupDeLObjecte}' processat: Temps {obj.GetTempsSentApuntat():F2}s, Total {dadesProcessades[grupDeLObjecte].tempsTotal:F2}s, Quantitat {dadesProcessades[grupDeLObjecte].quantitatObjectes}");
                }
                // Si no existeix, el codi no fa res (obvia l'objecte)
            }
        }

        // 4. Càlcul de mitjanes final
        foreach (var entrada in dadesProcessades)
        {
            if (entrada.Value.quantitatObjectes > 0)
            {
                entrada.Value.mitjana = entrada.Value.tempsTotal / entrada.Value.quantitatObjectes;
                // Debug.Log($"<b>Grup {entrada.Key}:</b> Mitjana {entrada.Value.mitjana:F2}s");
                Debug.Log($"Grup {entrada.Key}: Temps Total {entrada.Value.tempsTotal:F2}s, Quantitat {entrada.Value.quantitatObjectes}, Mitjana {entrada.Value.mitjana:F2}s");
            }
        }
        // Debug.Log("temps total sala 5: " + tempsTotalSala5);
    }

    public float tempsTotalSala5
    {
        get
        {
            return dadesProcessades.Values.Sum(g => g.tempsTotal);
        }
    }
    public float MitjaTempsSala5
    {
        get
        {
            var grupsAmbObjectes = dadesProcessades.Values.Where(g => g.quantitatObjectes > 0);
            if (!grupsAmbObjectes.Any()) return 0;
            return grupsAmbObjectes.Average(g => g.mitjana);
        }
    }
    public float tempsA
    {
        get
        {
            return dadesProcessades.ContainsKey("A") ? dadesProcessades["A"].mitjana : 0;
        }
    }
    public float tempsAB
    {
        get
        {
            return dadesProcessades.ContainsKey("AB") ? dadesProcessades["AB"].mitjana : 0;
        }
    }
    public float tempsB
    {
        get
        {
            return dadesProcessades.ContainsKey("B") ? dadesProcessades["B"].mitjana : 0;
        }
    }
    public float tempsBA
    {
        get
        {
            return dadesProcessades.ContainsKey("BA") ? dadesProcessades["BA"].mitjana : 0;
        }
    }
}
