/*using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}*/

using UnityEngine;
using UnityEngine.InputSystem; // Necessari per al nou sistema

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = .5f; // Atenció: El nou sistema dona valors més alts, baixa la sensibilitat
    public float smoothing = 3f;

    Vector2 velocity;
    Vector2 frameVelocity;

    void Reset()
    {
        // Nota: Assegura't que el pare tingui el component de moviment o assigna-ho manualment
        var movement = GetComponentInParent<MonoBehaviour>(); 
        if (movement != null) character = movement.transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Verifiquem si el ratolí existeix
        if (Mouse.current == null) return;

        // if (Cursor.lockState != CursorLockMode.Locked) //return;
        // {
        //     frameVelocity = Vector2.zero; // Netegem la inèrcia
        //     return;
        // }
        
        // NOU SISTEMA: Llegim el moviment del ratolí (delta)
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        // Escalem el moviment
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        
        // Apliquem el suavitzat (smoothing)
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        
        // Limitem la rotació vertical (no fer la volta a la pinça)
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Apliquem les rotacions
        // Rotació local de la càmera (Amunt/Avall)
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        
        // Rotació del personatge (Esquerra/Dreta)
        if (character != null)
        {
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }
    }

    void OnDisable()
    {
        // Resetegem el suavitzat perquè no hi hagi inèrcia acumulada
        frameVelocity = Vector2.zero;
        // Opcional: Si vols que quan tornis al joc la càmera no faci un salt, 
        // NO resetegis 'velocity', només 'frameVelocity'.
        // Però per aturar el gir immediat, frameVelocity ha de ser zero.
    }
    void OnEnable()
    {
        frameVelocity = Vector2.zero;
    }
}