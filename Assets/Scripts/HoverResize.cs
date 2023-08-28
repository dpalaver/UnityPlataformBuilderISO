using UnityEngine;
using UnityEngine.EventSystems;

public class HoverResize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isHovered = false;
    public float smoothTime = 2f;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * 1.1f;
    }

    private void Update()
    {
        // Interpolar la escala actual hacia la escala objetivo suavemente
        transform.localScale = Vector3.Lerp(transform.localScale, isHovered ? targetScale : originalScale, smoothTime * Time.deltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // El cursor del mouse está sobre el objeto
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // El cursor del mouse ya no está sobre el objeto
        isHovered = false;
    }
}
