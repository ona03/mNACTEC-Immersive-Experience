using UnityEngine;
using System;
using System.Collections; // Necessari per a Coroutines

public class resumSala4 : MonoBehaviour
{
    private GameObject infoSala4;
    [Header("SALA 4")]
    public int preguntesActivesCount = 1;
    [Serializable]
    public class pSala4 
    { 
        public int op;
        public float temps;
        public pSala4(int opcio, float tempsCrono) 
            { op = opcio; temps = tempsCrono; } 
    }
    public pSala4[] preguntesSala4 = new pSala4[14]; 
    // [0] gos_gat, [1] pizza_pasta, [2] serie_peli, [3] platja_muntanya, 
    // [4] esports_nous, [5] rutina_canvi, [6] moment_planificat, 
    // [7] improvisar_no, [8] costa_dir_no, [9] canvi_opinio, 
    // [10] sobrepensa_no, [11] paper_ebook, [12] detalls_no, 
    // [13] ordre_desordre
    void Awake()
    {
        // Assegura't que l'array està inicialitzat abans de qualsevol ús
        for (int i = 0; i < preguntesSala4.Length; i++)
        {
            preguntesSala4[i] = new pSala4(0, 0f);
            // Debug.Log($"Inicialitzada preguntesSala4[{i}] amb op: {preguntesSala4[i].op}, temps: {preguntesSala4[i].temps}");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infoSala4 = GameObject.Find("canvasSala4");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NotificarPreguntaActivada(string nomDelFill)
    {
        // Debug.Log($"Notificació rebuda per pregunta: {nomDelFill}. Preguntes actives actuals: {preguntesActivesCount}");

        if (preguntesActivesCount > 0 && preguntesActivesCount <= preguntesSala4.Length)
        {
            int indexAnterior = preguntesActivesCount - 1;
            // Busquem el fill anterior pel seu nom logic
            // GameObject preguntaAnterior = infoSala4.transform.Find("Pregunta 4_" + (indexAnterior + 1))?.gameObject;

            if (indexAnterior < preguntesSala4.Length)
            {
                GameObject preguntaAnterior = infoSala4.transform.Find("Pregunta 4_" + (indexAnterior + 1))?.gameObject;
                if (preguntaAnterior != null)
                {
                    // Iniciem l'espera asíncrona
                    StartCoroutine(EsperarIActualitzarDades(indexAnterior, preguntaAnterior));
                }
                // Només sumem si encara no hem arribat a l'última
                preguntesActivesCount++;
            }            
        }
    }
    IEnumerator EsperarIActualitzarDades(int index, GameObject objPregunta)
    {
        var bCtrl = objPregunta.GetComponent<UI_ButtonController>();
        var cCtrl = objPregunta.GetComponent<UI_CronoController>();

        if (bCtrl == null || cCtrl == null) yield break;

        // "Mentre l'opció sigui 0, espera al següent frame"
        while (bCtrl.GetOpcioTriada() == 0)
        {
            yield return null; 
        }

        // Un cop el valor ja no és 0, guardem
        preguntesSala4[index].op = bCtrl.GetOpcioTriada();
        preguntesSala4[index].temps = cCtrl.GetTempsResposta();
        
        // Debug.Log($"Guardada Pregunta {index + 1}: Op {preguntesSala4[index].op}, Temps {preguntesSala4[index].temps}");
    }

    public float MitjaTempsResposta
    {
        get
        {
            float sumaTemps = 0f;
            int count = 0;

            for (int i = 0; i < preguntesSala4.Length; i++)
            {
                if (preguntesSala4[i].op != 0) // Comptem només les preguntes que s'han respost
                {
                    sumaTemps += preguntesSala4[i].temps;
                    count++;
                }
            }

            return count > 0 ? sumaTemps / count : 0f; // Evitem divisió per zero
        }
    }
    // int op()
    // {
    //     return op;
    // }
}
