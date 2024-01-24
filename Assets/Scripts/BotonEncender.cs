using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonEncender : MonoBehaviour
{
    public MoverBombillo moverBombillo1;
    public MoverBombillo moverBombillo2;
    public MoverBombillo moverBombillo3;
    public MoverBombillo moverBombillo4;
    public float desactivarJuego;

    public bool terminoJuego = false;

    void Update()
    {
        // Verificar si se ha hecho clic en el objeto
        if (Input.GetMouseButtonDown(0))
        {
            // Obtener la posición del ratón en el mundo
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Obtener el collider del objeto
            Collider2D collider = GetComponent<Collider2D>();

            // Verificar si la posición del ratón está dentro del collider del objeto
            if (collider.OverlapPoint(mousePosition))
            {
                // El objeto ha sido clickeado
                Debug.Log("Objeto clickeado: " + gameObject.name);
                if (moverBombillo1.detecto && moverBombillo1.objetoEnContacto == moverBombillo1.id)
                {
                    if (moverBombillo2.detecto && moverBombillo2.objetoEnContacto == moverBombillo2.id)
                    {
                        if (moverBombillo3.detecto && moverBombillo3.objetoEnContacto == moverBombillo3.id)
                        {
                            if (moverBombillo4.detecto && moverBombillo4.objetoEnContacto == moverBombillo4.id)
                            {
                                Debug.Log("Ganaste mini juego");
                                terminoJuego = true;
                            }
                            else
                            {
                                Debug.Log("Aun no puedes salir");
                            }
                        }
                        else
                        {
                            Debug.Log("Aun no puedes salir");
                        }
                    }
                    else
                    {
                        Debug.Log("Aun no puedes salir");
                    }
                }
                else
                {
                    Debug.Log("Aun no puedes salir");
                }
            }
        }
    }
}