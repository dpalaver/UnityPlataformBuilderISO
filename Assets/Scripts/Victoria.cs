using UnityEngine;

public class Victoria : MonoBehaviour
{
    public GameManager gameManager;
    public string finalTagName = "Final"; // Etiqueta del objeto final
    public float delayInSeconds = 2.0f; // Tiempo de demora en segundos antes de ejecutar la acción

    private bool triggered = false;
    private float timer = 0.0f;

    private void Update()
    {
        if (triggered)
        {
            timer += Time.deltaTime;
            if (timer >= delayInSeconds)
            {
                gameManager.EstadoJuego = "Final";
                Debug.Log("Correcto.");
                triggered = false; // Reinicia la bandera para evitar ejecuciones repetidas
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(finalTagName))
        {
            triggered = true;
        }
    }
}
