using UnityEngine;

public class NoRotation : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        // Ottieni il Rigidbody e blocca ogni rotazione
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Blocca rotazioni fisiche
        }
    }

    void Update()
    {
        // Forza la rotazione a rimanere fissa
        transform.rotation = Quaternion.identity;

        // Assicura che anche la velocità angolare sia zero
        if (rb != null)
        {
            rb.angularVelocity = Vector3.zero; // Ferma qualsiasi rotazione
        }
    }
}
