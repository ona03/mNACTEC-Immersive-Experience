using UnityEngine;
using UnityEngine.UI; // Necessari per al component Button
using System;

public class UI_ButtonReturn : MonoBehaviour
{
    public int valor;
    public event Action<int> OnClickAmbValor;
    // private int opcioTriada; // Aquí es guardarà la selecció final
    // public static event Action<int> OnOpcioSeleccionada;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake() 
    {
        GetComponent<Button>().onClick.AddListener(() => {
            // Avisem a qui estigui escoltant que s'ha clicat aquest botó
            OnClickAmbValor?.Invoke(valor);
        });
    }
}
