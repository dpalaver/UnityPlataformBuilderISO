using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform target; // El transform del jugador a seguir
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Desplazamiento de la cámara respecto al jugador
    public GameManager gameManager; // Referencia al script GameManager

    private void Start()
    {
        // Busca el jugador al inicio y almacena su transform
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (target == null)
        {
            Debug.LogWarning("No se encontró ningún objeto con la etiqueta 'Player'.");
        }
    }

    private void Update()
    {
        if (gameManager.EstadoJuego == "Jugando")
        {
            // Calcula la posición de la cámara sumando el desplazamiento al jugador
            Vector3 targetPosition = target.position + offset;
            transform.position = targetPosition;
        }
    }
}
