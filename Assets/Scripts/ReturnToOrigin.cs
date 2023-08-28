using UnityEngine;

public class ReturnToOrigin : MonoBehaviour
{
    private Vector3 originalPosition; // Punto de origen del objeto
    public GameManager gameManager;

    private void Start()
    {
        originalPosition = transform.position; // Guardar el punto de origen del objeto al iniciar
    }

    private void Update()
    {
        // Verificar posición en eje Y y contar el tiempo si es menor que 1
        if (transform.position.y < 1f && gameManager.EstadoJuego == "Jugando")
        {
            gameManager.ModificarTexto("TextoDerrota","¡Al pozo!");
            gameManager.ModificarTexto("TextoConsejo","Si te caes al agua o al vacío vas a perder, usa los materiales para evitarlo.");

            gameManager.SetGameObjectVisibility("UIDerrota",true);

            gameManager.timer = 0f;
            gameManager.EstadoJuego = "Derrota";
        }
    }
}
