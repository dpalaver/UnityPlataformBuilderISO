using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public string EstadoJuego = "Construccion";

    public float zoomSpeed = 5f; // Velocidad de zoom
    public float minZoomSize = 8f; // Tamaño de zoom mínimo
    public float maxZoomSize = 11f; // Tamaño de zoom máximo
    public float smoothTime = 0.1f; // Tiempo de suavizado

    private Camera mainCamera;
    private float currentVelocity = 0f;

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

    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.orthographicSize = minZoomSize; // Establecer el zoom inicial a 8
    }

    private void Update()
    {
        if (EstadoJuego == "Construccion") {
            Zoom(maxZoomSize);
        } else if (EstadoJuego == "Jugando") {
            Zoom(minZoomSize);
        }
    }

    private void Zoom(float targetZoomSize)
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
