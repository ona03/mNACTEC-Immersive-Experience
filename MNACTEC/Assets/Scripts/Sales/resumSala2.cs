using UnityEngine;

public class resumSala2 : MonoBehaviour
{
    private GameObject infoSala2, path2;
    private MeshRenderer mRenderer;
    [Header("SALA 2")]
    // public GameObject path2;
    private UI_ButtonController[] btnControllers = new UI_ButtonController[5];
    public int[] preferencies = new int[5]; 
    // [0] = animal, [1] = mov_pictòric, [2] = planta, [3] = superpoder, [4] = estètica

    // public int H, A, I, S;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infoSala2 = GameObject.Find("canvasSala2");
        path2 = GameObject.Find("PathSala2");
        mRenderer = path2.GetComponent<MeshRenderer>();

        if (infoSala2 != null)
        {
            for (int i = 0; i < preferencies.Length; i++)
            {
                Transform tP = infoSala2.transform.Find("Preferència " + (i + 1));
                if (tP != null)
                {
                    btnControllers[i] = tP.GetComponent<UI_ButtonController>();
                    // Debug.Log($"UI_ButtonController asignat a Preferència {i+1} en {btnControllers[i].gameObject.name}. Pregunta: {preferencies[i]}");
                }
                
                if (btnControllers[i] == null)
                    Debug.LogWarning($"No s'ha trobat UI_ButtonController a Preferència {i+1}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path2 == null || infoSala2 == null || !path2.activeSelf) return;

        // Debug.Log($"Mesh renderer {mRenderer.enabled}");

        if (path2.gameObject.activeSelf)
        {
            if (mRenderer.enabled)
            {
                for (int i = 0; i < preferencies.Length; i++)
                {
                    // Si la preferència ja està guardada (diferent de 0), podem saltar-la (Opcional)
                    if (preferencies[i] != 0) continue; 

                    if (btnControllers[i] != null)
                    {
                        int opcioSeleccionada = btnControllers[i].GetOpcioTriada();
                        // Debug.Log($"Preferència {i + 1} - Opció seleccionada: {opcioSeleccionada}");

                        if (opcioSeleccionada != 0)
                        {
                            preferencies[i] = opcioSeleccionada;
                            // Debug.Log($"Preferència {i + 1} guardada: {preferencies[i]}");
                        }
                    }
                }
            }
        }
    }
}
