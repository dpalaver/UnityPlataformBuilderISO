using UnityEngine;

public class DistanceComparator : MonoBehaviour
{
    public float distanceThreshold = 5.0f;
    public float timeThreshold = 5.0f;
    public float reducedMoveSpeedFactor = 0.5f;
    private bool enteredOnce = false;
    private bool inArea = false;
    private float elapsedTime = 0.0f;
    private float accionTiempo = 0f;
    private AccionesObjetosActivos accion; // Agrega una variable para almacenar la referencia al script
    public GameManager gameManager;

    private void Start()
    {
        GameObject miObjeto = GameObject.Find("Accion");

        // Obtén la referencia al script AccionesObjetosActivos
        accion = miObjeto.GetComponent<AccionesObjetosActivos>();

        if (accion != null)
        {
            // Puedes almacenar la referencia a 'accion' para usarla más adelante si es necesario.
        }
        else
        {
            Debug.LogWarning("Script AccionesObjetosActivos no encontrado en el objeto.");
        }
    }

    private void Update()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            float distance = Vector3.Distance(transform.position, playerObject.transform.position);

            PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                if (distance < distanceThreshold)
                {
                    if (!inArea)
                    {
                        inArea = true;
                        elapsedTime = 0.0f;
                        accionTiempo = timeThreshold;
                        playerMovement.SetMoveSpeed(playerMovement.moveSpeed * reducedMoveSpeedFactor);
                        gameManager.SetGameObjectVisibility("UIAcciones",true);
                    }

                    elapsedTime += Time.deltaTime;
                    accionTiempo -= Time.deltaTime;

                    if (gameObject.name == "Pala")
                    {
                        gameManager.ModificarTexto("TextoAccion","¡Sal de ahí! Vas a trabajar en " + accionTiempo.ToString("0"));
                    }
                    else if (gameObject.name == "Parrilla")
                    {
                        gameManager.ModificarTexto("TextoAccion","¡Sal de ahí! Vas a tentarte en " + accionTiempo.ToString("0"));
                    }
                    else if (gameObject.name == "Arquitecto")
                    {
                        gameManager.ModificarTexto("TextoAccion","¡Sal de ahí! Vas a ser regañado en " + accionTiempo.ToString("0"));
                    }

                    if (elapsedTime >= timeThreshold && !enteredOnce)
                    {
                        string gameObjectUID = GetComponent<GameObjectUID>().UID;
                        accion.Acciones(gameObjectUID, gameObject.name);
                        enteredOnce = true;
                        gameManager.SetGameObjectVisibility("UIAcciones",false);
                    }
                }
                else
                {
                    if (inArea)
                    {
                        inArea = false;
                        elapsedTime = 0.0f;
                        enteredOnce = false;
                        playerMovement.RestoreMoveSpeed();

                        gameManager.SetGameObjectVisibility("UIAcciones",false);
                    }
                }
            }
            else
            {
                Debug.LogWarning("El GameObject con la etiqueta 'Player' no tiene el componente PlayerMovement.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el GameObject con la etiqueta 'Player'.");
        }
    }
}
