using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidarPlato : MonoBehaviour
{
    public string nombreSpriteValido;
    public bool platoCorrecto;
    private Sprite spriteAnterior; // Variable para almacenar el sprite anterior

    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            // Verificar si el sprite actual es diferente al sprite anterior
            if (spriteRenderer.sprite != spriteAnterior)
            {
                if (spriteRenderer.sprite.name == nombreSpriteValido)
                {
                    platoCorrecto = true;
                }
                else
                {
                    platoCorrecto = false;
                }

                // Actualizar el sprite anterior al sprite actual
                spriteAnterior = spriteRenderer.sprite;
            }
        }
    }
}
