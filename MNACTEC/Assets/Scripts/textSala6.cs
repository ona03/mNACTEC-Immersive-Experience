using UnityEngine;

[CreateAssetMenu(fileName = "textSala6", menuName = "Scriptable Objects/textSala6")]
public class textSala6 : ScriptableObject
{
    public string idCombinacio; 
    [TextArea(3, 10)] public string paragraf1;
    [TextArea(3, 10)] public string paragraf2;
}
