using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalarObjeto : MonoBehaviour
{
    public float escalaMinima = 0.5f;
    public float escalaMaxima = 2.0f; 
    public float velocidadEscalado = 1.0f; 

    private bool colisionConLimite = false;

    public GameObject enchufe;

    void Update()
    {
        if (!colisionConLimite)
        {
            GameObject limite = GameObject.FindGameObjectWithTag("Limite");
            if (limite != null)
            {
                float distancia = Mathf.Abs(transform.position.x - enchufe.transform.position.x);
                //gameObject.GetComponent<SpriteRenderer>().size = new Vector2(distancia, gameObject.GetComponent<SpriteRenderer>().size.y);

                //float nuevaEscalaX = Mathf.Clamp(distancia * velocidadEscalado, escalaMinima, escalaMaxima);
                transform.localScale = new Vector3(distancia, transform.localScale.y, transform.localScale.z);
                if (enchufe != null)
                {
                    Vector3 direccion = enchufe.transform.position - transform.position;
                    float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
                }
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
