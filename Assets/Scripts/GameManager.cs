using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public string EstadoJuego = "Inicio";

    public string Nivel = "Nivel1";

    public PlayerMovement playerMovement;

    public float zoomSpeed = 5f; // Velocidad de zoom
    public float minZoomSize = 8f; // Tamaño de zoom mínimo
    public float maxZoomSize = 11f; // Tamaño de zoom máximo
    public float smoothTime = 0.1f; // Tiempo de suavizado

    public int TablasCantidad = 0;
    public int AndamiosCantidad = 0;
    public int EscalerasCantidad = 0;

    private Camera mainCamera;
    private float currentVelocity = 0f;

    private float timeThreshold = 1.0f; // Umbral de tiempo para reiniciar en segundos
    public float timer = 0.0f; // Temporizador para contar el tiempo

    private RandomCameraMovement cameraMovementScript;

    public bool isMenuVisible = false;
    private bool isMousePressed = false;

    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.orthographicSize = minZoomSize; // Establecer el zoom inicial a 8
        ModificarTexto("TextoBtn1",TablasCantidad.ToString());
        ModificarTexto("TextoBtn2",AndamiosCantidad.ToString());
        ModificarTexto("TextoBtn3",EscalerasCantidad.ToString());
        SetGameObjectVisibility("UIVictoria",false);
        SetGameObjectVisibility("UIMover",false);
        SetGameObjectVisibility("UIConstruccion",false);
        SetGameObjectVisibility("UIDerrota",false);
        SetGameObjectVisibility("UIAcciones",false);
        SetGameObjectVisibility("UIMenuPartida",false);
        SetGameObjectVisibility("UIMenuConstruccion", false);

        // Obtener la referencia al componente RandomCameraMovement en el objeto de la cámara
        cameraMovementScript = FindObjectOfType<RandomCameraMovement>();
    }

    private void Update()
    {

        // Verificar si el botón izquierdo del mouse se está manteniendo presionado o no
        if (Input.GetMouseButtonDown(0)){isMousePressed = true;}
        if (Input.GetMouseButtonUp(0)){isMousePressed = false;}

        if (EstadoJuego == "Inicio") {
            Zoom(minZoomSize);
            InputJugador(false);
            cameraMovementScript.SetCameraMovementEnabled(true);
            isMenuVisible = false;
            SetGameObjectVisibility("UIConstruccion",false);
            SetGameObjectVisibility("UIVictoria",false);
            SetGameObjectVisibility("UIDerrota",false);
            SetGameObjectVisibility("UIInicio",true);
            SetGameObjectVisibility("UIMenuPartida",false);
            SetGameObjectVisibility("UIMenuConstruccion", false);
        } else if (EstadoJuego == "Construccion") {
            cameraMovementScript.SetCameraMovementEnabled(false);
            Zoom(maxZoomSize);
            InputJugador(false);
            SetGameObjectVisibility("UIVictoria",false);
            SetGameObjectVisibility("UIDerrota",false);
            SetGameObjectVisibility("UIInicio",false);
            SetGameObjectVisibility("UIMenuPartida",false);

            if (isMenuVisible) {
                SetGameObjectVisibility("UIConstruccion",false);
            } else {
                SetGameObjectVisibility("UIConstruccion",true);
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !isMenuVisible && !isMousePressed) {
                isMenuVisible = true;
                SetGameObjectVisibility("UIMenuConstruccion", isMenuVisible);
            } else if (Input.GetKeyDown(KeyCode.Escape) && isMenuVisible) {
                isMenuVisible = false;
                SetGameObjectVisibility("UIMenuConstruccion", isMenuVisible);
            }

        } else if (EstadoJuego == "Jugando") {
            
            SetGameObjectVisibility("UIConstruccion",false);
            SetGameObjectVisibility("UIVictoria",false);
            SetGameObjectVisibility("UIDerrota",false);

            if (isMenuVisible) {
                Zoom(maxZoomSize);
            } else {
                Zoom(minZoomSize);
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !isMenuVisible) {
                isMenuVisible = true;
                SetGameObjectVisibility("UIMenuPartida", isMenuVisible);
            } else if (Input.GetKeyDown(KeyCode.Escape) && isMenuVisible) {
                isMenuVisible = false;
                SetGameObjectVisibility("UIMenuPartida", isMenuVisible);
            }

        } else if (EstadoJuego == "Final") {
            Zoom(maxZoomSize);
            InputJugador(false);
            SetGameObjectVisibility("UIConstruccion",false);
            SetGameObjectVisibility("UIVictoria",true);

            timer += Time.deltaTime; // Incrementar el temporizador
            if (timer >= timeThreshold)
            {
                RestartScene("Reinicio");
            }

        } else if (EstadoJuego == "Derrota") {
            Zoom(maxZoomSize);
            InputJugador(false);
            SetGameObjectVisibility("UIConstruccion",false);
            SetGameObjectVisibility("UIMenuPartida",false);

            timer += Time.deltaTime; // Incrementar el temporizador
            if (timer >= timeThreshold)
            {
                if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
                {
                    RestartScene("Reintentar");
                    SetGameObjectVisibility("UIDerrota",false);
                }

                if (timer >= 6) {
                    RestartScene("Reintentar");
                    SetGameObjectVisibility("UIDerrota",false);
                }

            }

        }

    }

    public void RestartScene(string estado)
    {

        playerMovement.transform.position = playerMovement.initialPosition;

        if (estado == "Reinicio")
        {

            EstadoJuego = "Construccion";

            EliminarObjetosPorTag("Andamio");
            EliminarObjetosPorTag("Tabla");
            EliminarObjetosPorTag("Escalera");

            TablasCantidad = 8;
            AndamiosCantidad = 4;
            EscalerasCantidad = 4;

            ModificarTexto("TextoBtn1",TablasCantidad.ToString());
            ModificarTexto("TextoBtn2",AndamiosCantidad.ToString());
            ModificarTexto("TextoBtn3",EscalerasCantidad.ToString());

        } else if (estado == "Reintentar") {

            EstadoJuego = "Jugando";
            InputJugador(true);

        }

    }

    public void EliminarObjetosPorTag(string tag)
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject objeto in objetos)
        {
            // Obtener el componente VariablesGameObject del objeto
            VariablesGameObject variablesComponent = objeto.GetComponent<VariablesGameObject>();

            // Si el componente existe y su valor EnEscena es 'false', eliminar el objeto
            if (variablesComponent != null && !variablesComponent.EnEscena)
            {
                Destroy(objeto);
            }
        }
    }

    public void SetGameObjectVisibility(string objectName, bool isVisible)
    {
        GameObject gameObjectToToggle = GameObject.Find(objectName);

        if (gameObjectToToggle != null)
        {
            CanvasGroup canvasGroup = gameObjectToToggle.GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = isVisible ? 1f : 0f; // Set alpha to 1 for visible, 0 for invisible
                canvasGroup.interactable = isVisible;
                canvasGroup.blocksRaycasts = isVisible;
            }
        }
    }

    public void InputJugador(bool activado)
    {
        if (playerMovement != null)
        {
            if (activado) {
                playerMovement.movementEnabled = true; // Activar el movimiento del jugador
            } else {
                playerMovement.movementEnabled = false; // Desactivar el movimiento del jugador
            }
        }
    }
    private void Zoom(float targetZoomSize)
    {
        if (mainCamera != null) // Verifica si la cámara no es nula
        {
            // Calcular el nuevo tamaño de zoom suavizado
            float newZoomSize = Mathf.SmoothDamp(mainCamera.orthographicSize, targetZoomSize, ref currentVelocity, smoothTime);

            // Aplicar el nuevo tamaño de zoom a la cámara
            mainCamera.orthographicSize = newZoomSize;

            // Detener el suavizado si estamos cerca del valor objetivo
            if (Mathf.Abs(newZoomSize - targetZoomSize) < 0.05f)
            {
                currentVelocity = 0f;
                mainCamera.orthographicSize = targetZoomSize;
            }
        }
    }

    public void ModificarVariable(string nombre, int cantidad, string operacion)
    {
        if (nombre == "Tabla") {

            if (operacion == "sumar") {
                TablasCantidad = TablasCantidad + cantidad;
            } else if (operacion == "restar") {
                TablasCantidad = TablasCantidad - cantidad;
            }
            
            ModificarTexto("TextoBtn1",TablasCantidad.ToString());

        } else if (nombre == "Andamio") {

            if (operacion == "sumar") {
                AndamiosCantidad = AndamiosCantidad + cantidad;
            } else if (operacion == "restar") {
                AndamiosCantidad = AndamiosCantidad - cantidad;
            }
            
            ModificarTexto("TextoBtn2",AndamiosCantidad.ToString());

        } else if (nombre == "Escalera") {

            if (operacion == "sumar") {
                EscalerasCantidad = EscalerasCantidad + cantidad;
            } else if (operacion == "restar") {
                EscalerasCantidad = EscalerasCantidad - cantidad;
            }
            
            ModificarTexto("TextoBtn3",EscalerasCantidad.ToString());

        }
        
    }
    public void ModificarTexto(string nombre, string texto)
    {
        // Buscar el objeto por su nombre
        GameObject objeto = GameObject.Find(nombre);

        if (objeto != null)
        {
            // Obtener el componente TextMeshProUGUI del objeto
            TextMeshProUGUI textoComponent = objeto.GetComponent<TextMeshProUGUI>();

            if (textoComponent != null)
            {
                // Modificar el texto del componente TextMeshProUGUI
                textoComponent.text = texto;
            }
            else
            {
                Debug.LogWarning("El objeto no tiene un componente TextMeshProUGUI.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con el nombre proporcionado.");
        }
    }

    public void StartBlinking(string objectName, float duration, float interval)
    {
        StartCoroutine(BlinkObject(objectName, duration, interval));
    }

    private IEnumerator BlinkObject(string objectName, float duration, float interval)
    {
        GameObject obj = GameObject.Find(objectName);

        if (obj == null)
        {
            Debug.LogWarning("Object with name " + objectName + " not found.");
            yield break;
        }

        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            obj.SetActive(!obj.activeSelf);

            yield return new WaitForSeconds(interval);
        }

        // Ensure the object is left in a consistent state after blinking
        obj.SetActive(true);
    }

/*     public void ChangeScene(string nombre) {
        Nivel = nombre;
        SceneManager.LoadScene(nombre); // Carga la escena con el nombre proporcionado
    } */

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
