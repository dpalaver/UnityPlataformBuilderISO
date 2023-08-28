using UnityEngine;

public class MoveLevel : MonoBehaviour
{
    public float moveSpeed = 1000f; // Velocidad de movimiento del carrusel
    private RectTransform textTransform;
    private float screenWidth;
    private int direction = 0; // Dirección del movimiento (0: detenido, -1: izquierda, 1: derecha)
    private bool desactivar = true;

    private void Start()
    {
        if (desactivar) {
            textTransform = GetComponent<RectTransform>();
            screenWidth = Screen.width;
        }
    }

    private void Update()
    {
        if (desactivar) {
            //float direction = Input.GetAxis("Horizontal"); // Recibe el input horizontal

            if (direction != 0)
            {
                float deltaX = moveSpeed * direction * Time.deltaTime;

                textTransform.anchoredPosition += new Vector2(deltaX, 0f);

                // Verificar si el objeto de texto salió completamente de la pantalla
                if (direction == -1 && textTransform.anchoredPosition.x < -screenWidth / 2)
                {
                    textTransform.anchoredPosition = new Vector2(screenWidth / 2, textTransform.anchoredPosition.y);
                    direction = 0; // Detener el movimiento
                }
                else if (direction == 1 && textTransform.anchoredPosition.x > screenWidth / 2)
                {
                    textTransform.anchoredPosition = new Vector2(-screenWidth / 2, textTransform.anchoredPosition.y);
                    direction = 0; // Detener el movimiento
                }
            }
        }
    }
}
