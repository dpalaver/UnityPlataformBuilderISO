using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AccionBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameManager gameManager; // Referencia al script GameManager

    public void OnPointerDown(PointerEventData eventData)
    {

        // Verifica si se hizo clic izquierdo
        if (eventData.button == PointerEventData.InputButton.Left)
        {

            if (gameManager.EstadoJuego == "Inicio") {

                //gameManager.ChangeScene(gameManager.Nivel);
                gameManager.EstadoJuego = "Construccion";

            } else if (gameManager.EstadoJuego == "Construccion") {

                if (gameObject.name == "btnInicio") {

                    gameManager.RestartScene("Reinicio");
                    gameManager.EstadoJuego = "Inicio";

                } else if (gameObject.name == "btnReiniciar") {

                    gameManager.RestartScene("Reinicio");

                } else if (gameObject.name == "btnJugar") {

                    gameManager.EstadoJuego = "Jugando";
                    gameManager.InputJugador(true);

                } else if (gameObject.name == "btnContinuar") {

                }

                gameManager.isMenuVisible = false;
                gameManager.SetGameObjectVisibility("UIMenuConstruccion", gameManager.isMenuVisible);

            } else if (gameManager.EstadoJuego == "Jugando") {

                if (gameObject.name == "btnInicio") {

                    gameManager.RestartScene("Reinicio");
                    gameManager.EstadoJuego = "Inicio";

                } else if (gameObject.name == "btnReiniciar") {

                    gameManager.RestartScene("Reinicio");

                } else if (gameObject.name == "btnReintentar") {

                    gameManager.RestartScene("Reintentar");

                } else if (gameObject.name == "btnContinuar") {


                }

                gameManager.isMenuVisible = false;
                gameManager.SetGameObjectVisibility("UIMenuPartida", gameManager.isMenuVisible);

            }

            

        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Aquí puedes implementar lógica adicional para el evento de soltar el clic, si es necesario
    }
}
