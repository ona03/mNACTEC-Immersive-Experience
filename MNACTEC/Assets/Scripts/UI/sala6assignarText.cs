using UnityEngine;
using System.Collections.Generic;

public class sala6assignarText : MonoBehaviour
{
    [Header("Tots els Textos del Joc")]
    public List<textSala6> bibliotecaGlobal;
    private Dictionary<string, textSala6> diccionariGlobal = new Dictionary<string, textSala6>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Omplim el diccionari una sola vegada per a tot el joc
        foreach (var dada in bibliotecaGlobal)
        {
            if (!string.IsNullOrEmpty(dada.idCombinacio) && !diccionariGlobal.ContainsKey(dada.idCombinacio))
            {
                diccionariGlobal.Add(dada.idCombinacio, dada);
            }
        }
    }
    // Funció universal que qualsevol fill pot cridar
    public textSala6 ObtenirText(string clau)
    {
        if (diccionariGlobal.TryGetValue(clau, out textSala6 data))
        {
            return data;
        }
        Debug.LogWarning($"La clau {clau} no s'ha trobat al Gestor Global.");
        return null;
    }
}
