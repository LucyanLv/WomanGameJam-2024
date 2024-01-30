using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlatillos : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;
    private SpriteRenderer platoSpriteRenderer; // Referencia al SpriteRenderer del objeto "Plato"

    public Sprite nuevoSprite; // Variable para asignar el nuevo sprite por el Inspector

    private void Start()
    {
        initialPosition = transform.position;
        platoSpriteRenderer = GameObject.FindGameObjectWithTag("Plato").GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = initialPosition.z; // Mantener la misma profundidad z
            transform.position = newPosition;

            // Detectar el objeto "Plato" al mover y cambiar su sprite si se detecta
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Plato"))
                {
                    platoSpriteRenderer.sprite = nuevoSprite; // Usar el nuevo sprite asignado por el Inspector
                }
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        transform.position = initialPosition;
    }
}
