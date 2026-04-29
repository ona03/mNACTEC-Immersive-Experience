/*using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded))
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
*/
using UnityEngine;
using UnityEngine.InputSystem; // Necessari per al nou sistema

public class Jump : MonoBehaviour
{
    Rigidbody rb;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;

    void Reset()
    {
        // Intenta trobar el GroundCheck automàticament
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Obtenim el Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Verifiquem si el teclat està actiu
        if (Keyboard.current == null) return;

        // NOU SISTEMA: Keyboard.current.spaceKey.wasPressedThisFrame 
        // Substitueix a Input.GetButtonDown("Jump")
        bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;

        if (jumpPressed && (!groundCheck || groundCheck.isGrounded))
        {
            // Apliquem la força cap amunt
            // He canviat 'rigidbody' per 'rb' i usat linearVelocity per compatibilitat moderna si fos necessari
            rb.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}