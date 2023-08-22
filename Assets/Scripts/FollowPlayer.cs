using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // El transform del jugador a seguir
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Desplazamiento de la cámara respecto al jugador

    private void Update()
    {
        if (target != null)
        {
            // Calcula la posición de la cámara sumando el desplazamiento al jugador
            Vector3 targetPosition = target.position + offset;
            transform.position = targetPosition;
        }
    }
}