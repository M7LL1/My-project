using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;         // Il riferimento al personaggio
    public float smoothSpeed = 0.125f; // Velocità con cui la telecamera segue
    public Vector3 defaultOffset;   // Offset iniziale (posizione normale della telecamera)
    public float horizontalMovementFactor = 2f; // Fattore di spostamento orizzontale della telecamera

    private Vector3 currentOffset;  // Offset attuale della telecamera
    private Vector3 targetOffset;   // Offset di destinazione per la transizione
    private Vector3 lockedPosition; // Posizione "bloccata" della telecamera, quando il gioco è vinto

    private bool isCameraLocked = false; // Flag per sapere se la telecamera è bloccata

    void Start()
    {
        // Imposta l'offset iniziale
        currentOffset = defaultOffset;
        targetOffset = defaultOffset;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        if (!isCameraLocked) // La telecamera segue il giocatore normalmente se non è bloccata
        {
            // Calcola l'offset orizzontale in base alla posizione del giocatore lungo l'asse X
            float horizontalOffset = player.position.x * horizontalMovementFactor;

            // Crea un target offset combinando la posizione orizzontale e l'offset verticale
            targetOffset = new Vector3(-horizontalOffset, defaultOffset.y, defaultOffset.z);

            // Applica una transizione morbida tra il vecchio offset e quello nuovo
            currentOffset = Vector3.Lerp(currentOffset, targetOffset, smoothSpeed);

            // Calcola la posizione desiderata della telecamera
            Vector3 desiredPosition = player.position + currentOffset;

            // Muove la telecamera verso la posizione desiderata con una transizione morbida
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
        else // Se la telecamera è bloccata, si sposta nella posizione fissa
        {
            transform.position = Vector3.Lerp(transform.position, lockedPosition, smoothSpeed);
        }
    }

    // Metodo per bloccare la telecamera in una posizione specifica (ad esempio sopra il giocatore)
    public void LockCamera(Vector3 newPosition)
    {
        lockedPosition = newPosition;
        isCameraLocked = true;
    }

    // Metodo per sbloccare la telecamera, riprendendo il movimento normale
    public void UnlockCamera()
    {
        isCameraLocked = false;
    }
}
