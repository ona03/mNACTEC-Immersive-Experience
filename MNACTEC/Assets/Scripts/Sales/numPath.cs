using UnityEngine;

public class numPath : MonoBehaviour
{
    private int opcio = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetOpcio(int op)
    {
        opcio = op;
    }
    public int GetOpcio()
    {
        return opcio;
    }
}
