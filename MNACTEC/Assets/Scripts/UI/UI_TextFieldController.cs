using UnityEngine;
using TMPro;
using System.Collections;

public class UI_TextFieldController : MonoBehaviour
{
    public TextMeshProUGUI textFieldContainer; // L'objecte de la UI que conté el text
    public GameObject objecteTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Comencem amb el text ocult
        if (textFieldContainer != null)
            textFieldContainer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Si el trigger s'activa i encara no s'ha mostrat el text
        if (objecteTrigger.gameObject.activeSelf)
        {
            if (!textFieldContainer.gameObject.activeSelf)
            {
                textFieldContainer.gameObject.SetActive(true);
            }
        }
        else 
        {
            textFieldContainer.gameObject.SetActive(false);
        }
    }
}