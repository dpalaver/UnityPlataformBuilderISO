using UnityEngine;

public class RandomCameraMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento de la cámara
    public float minMoveDistance = 3f; // Distancia mínima de movimiento aleatorio

    private Vector3 targetPosition; // Posición objetivo hacia la que se moverá la cámara
    private bool isEnabled = true; // Variable para habilitar/deshabilitar el movimiento
    public GameManager gameManager;

    private Terrain terrain;

    private void Start()
    {
        terrain = Terrain.activeTerrain;

        if (terrain != null)
        {
            // Inicializar la posición objetivo aleatoriamente dentro del terreno
            targetPosition = GenerateRandomPosition();
        }
        else
        {
            Debug.LogWarning("No se encontró un terreno en la escena.");
            isEnabled = false; // Deshabilitar el script si no se encuentra el terreno
        }
    }

    private void Update()
    {
        // Si no se encontró un terreno o el movimiento está deshabilitado, no se debe ejecutar el movimiento
        if (!isEnabled)
            return;

        // Calcular la distancia entre la posición actual y la posición objetivo
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Si la cámara está cerca de la posición objetivo, seleccionar una nueva posición aleatoria
        if (distanceToTarget < minMoveDistance)
        {
            // Seleccionar una nueva posición aleatoria dentro del terreno
            targetPosition = GenerateRandomPosition();
        }

        if (gameManager.EstadoJuego == "Inicio")
        {
            // Mover la cámara hacia la posición objetivo a una velocidad constante
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }


    }

    // Función para habilitar o deshabilitar el movimiento de la cámara desde otro script
    public void SetCameraMovementEnabled(bool enabled)
    {
        isEnabled = enabled;
    }

    // Generar una nueva posición aleatoria dentro del terreno
    private Vector3 GenerateRandomPosition()
    {
        Vector3 terrainSize = terrain.terrainData.size;
        return new Vector3(
            Random.Range(terrain.transform.position.x, terrain.transform.position.x + terrainSize.x),
            transform.position.y,
            Random.Range(terrain.transform.position.z, terrain.transform.position.z + terrainSize.z)
        );
    }
}
