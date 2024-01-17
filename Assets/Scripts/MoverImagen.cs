using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoverImagen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Image image;
    public bool pressed;
    private Vector3 initialPosition;
    public bool colisiono;

    private void Awake()
    {
        image = GetComponent<Image>();
        initialPosition = transform.position;
    }

    // Cuando dejo de pulsar 
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        // Restaurar la posición inicial al soltar el objeto
        transform.position = initialPosition;
    }

    // Cuando estoy pulsando 
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    // Cuando estamos arrastrando la imagen 
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // Cuando empezamos a arrastrar
    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    // Cuando dejamos de arrastrar 
    public void OnEndDrag(PointerEventData eventData)
    {
    }
}