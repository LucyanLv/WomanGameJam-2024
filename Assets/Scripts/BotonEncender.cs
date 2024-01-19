using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonEncender : MonoBehaviour
{
    public MoverBombillo moverBombillo;
    public IdRoceta idRoceta;

    void Update()
    {
        // Verificar si se ha hecho clic en el objeto
        if (Input.GetMouseButtonDown(0))
        {
            // Obtener la posici�n del rat�n en el mundo
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Obtener el collider del objeto
            Collider2D collider = GetComponent<Collider2D>();

            // Verificar si la posici�n del rat�n est� dentro del collider del objeto
            if (collider.OverlapPoint(mousePosition))
            {
                // El objeto ha sido clickeado
                Debug.Log("Objeto clickeado: " + gameObject.name);
                if (moverBombillo.EmparentadoARoseta && idRoceta.respuestaCorrecta)
                {
                    Debug.Log("Ganaste mini juego");
                }
                else
                {
                    Debug.Log("Aun no puedes salir");
                }
            }
        }
    }
}
