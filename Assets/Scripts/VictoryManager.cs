using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject victoryImage; // L'immagine della vittoria
    public Transform victoryCameraPosition; // Punto di destinazione della telecamera
    public float cameraSpeed = 2f; // Velocità di movimento della telecamera

    private Camera mainCamera;
    private bool isVictory = false;

    void Start()
    {
        // Trova la telecamera principale
        mainCamera = Camera.main;

        // Disattiva l'immagine di vittoria all'inizio
        victoryImage.SetActive(false);
    }

    void Update()
    {
        // Se è stata vinta la partita, sposta la telecamera
        if (isVictory)
        {
            MoveCameraToVictoryPosition();
        }
    }

    public void PlayerWins()
    {
        // Mostra l'immagine della vittoria
        victoryImage.SetActive(true);

        // Attiva il movimento della telecamera
        isVictory = true;
    }

    void MoveCameraToVictoryPosition()
    {
        // Muove la telecamera verso la posizione della vittoria
        mainCamera.transform.position = Vector3.Lerp(
            mainCamera.transform.position,
            victoryCameraPosition.position,
            cameraSpeed * Time.deltaTime
        );

        // Optional: Assicurati che la telecamera non vada oltre
        if (Vector3.Distance(mainCamera.transform.position, victoryCameraPosition.position) < 0.01f)
        {
            isVictory = false; // Ferma il movimento quando arriva
        }
    }
}
