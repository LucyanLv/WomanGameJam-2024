using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalarObjeto : MonoBehaviour
{
    public float escalaMinima = 0.5f;
    public float escalaMaxima = 2.0f; 
    public float velocidadEscalado = 1.0f; 

    private bool colisionConLimite = false;

    void Update()
    {
        if (!colisionConLimite)
        {
            GameObject limite = GameObject.FindGameObjectWithTag("Limite");
            if (limite != null)
            {
                float distancia = Mathf.Abs(transform.position.x - limite.transform.position.x);
                float nuevaEscalaX = Mathf.Clamp(distancia * velocidadEscalado, escalaMinima, escalaMaxima);
                transform.localScale = new Vector3(nuevaEscalaX, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limite"))
        {
            colisionConLimite = true;
        }
    }
}
