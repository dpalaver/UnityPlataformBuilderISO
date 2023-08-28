using UnityEngine;

public class AccionesObjetosActivos : MonoBehaviour
{
    PlayerMovement playerMovement;
    public GameManager gameManager;

    private void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void Acciones(string UID, string GameObjectName)
    {
        GameObject[] objectsWithUID = GameObject.FindGameObjectsWithTag(GameObjectName);
        
        foreach (GameObject obj in objectsWithUID)
        {
            GameObjectUID objUIDComponent = obj.GetComponent<GameObjectUID>();
            if (objUIDComponent != null && objUIDComponent.UID == UID)
            {
                gameManager.InputJugador(false);
                gameManager.SetGameObjectVisibility("UIDerrota", true);
                gameManager.EstadoJuego = "Derrota";
                gameManager.timer = 0f;

                if (GameObjectName == "Pala")
                {
                    gameManager.ModificarTexto("TextoDerrota", "¡A trabajar!");
                    gameManager.ModificarTexto("TextoConsejo", "Si te quedas lo suficiente cerca de una pala mejor prepárate para trabajar, evítalo.");
                }
                else if (GameObjectName == "Parrilla")
                {
                    gameManager.ModificarTexto("TextoDerrota", "¡No puedo evitarlo!");
                    gameManager.ModificarTexto("TextoConsejo", "Si te quedas lo suficiente cerca de una parrilla la tentación es mayor, evítalo.");
                }
                else if (GameObjectName == "Arquitecto")
                {
                    gameManager.ModificarTexto("TextoDerrota", "¡Regañado!");
                    gameManager.ModificarTexto("TextoConsejo", "Si te quedas lo suficiente cerca del arquitecto descubrirá que no estás trabajando, evítalo.");
                }

                break; // No es necesario seguir buscando después de encontrar el objeto correcto
            }
        }
    }
}
