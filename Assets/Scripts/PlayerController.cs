using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Assicurati che ci sia un Rigidbody2D sul personaggio
    }

    private void Update()
    {
        // Rileva i movimenti del personaggio (se è abilitato)
        if (rb != null)
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.y = Input.GetAxis("Vertical");

            // Muove il personaggio
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }
}
