using UnityEngine;

public class Moneta : MonoBehaviour
{
    public static int moneteRaccolte = 0;  // Contatore statico
    public int totalMonete = 2;  // Numero totale di monete nella scena

    public GameObject victoryImage;  // Riferimento all'immagine della vittoria
    public Transform player;  // Aggiungi questa variabile per il riferimento al giocatore (Player)

    void Start()
    {
        // Assicurati che l'immagine sia nascosta all'inizio
        if (victoryImage != null)
        {
            victoryImage.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            moneteRaccolte++;
            Debug.Log("Moneta raccolta! Totale: " + moneteRaccolte);

            if (moneteRaccolte == totalMonete)
            {
                Debug.Log("Tutte le monete raccolte! Mostra immagine.");
                
                // Mostra l'immagine di vittoria
                if (victoryImage != null)
                {
                    victoryImage.SetActive(true);
                }

                // Sposta la telecamera sopra il giocatore
                CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
                if (cameraFollow != null && player != null)
                {
                    cameraFollow.LockCamera(new Vector3(player.position.x, player.position.y + 200, Camera.main.transform.position.z));
                }
            }

            // Distruggi la moneta raccolta
            Destroy(gameObject);
        }
    }
}
