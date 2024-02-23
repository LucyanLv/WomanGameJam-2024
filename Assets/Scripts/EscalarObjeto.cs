using UnityEngine;

public class EscalarObjeto : MonoBehaviour
{
    public Transform objetoAEscalar;
    public float velocidadEscala = 1f;

    private void Update()
    {
        if (objetoAEscalar != null)
        {
            // Calculamos la diferencia en posici�n X entre este objeto y el objeto a escalar hacia �l
            float diferenciaX = objetoAEscalar.position.x - transform.position.x;

            // Calculamos la nueva escala en funci�n de la diferencia X
            float nuevaEscalaX = Mathf.Abs(diferenciaX);

            // Aplicamos la nueva escala con una velocidad gradual
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(nuevaEscalaX, transform.localScale.y, transform.localScale.z), velocidadEscala * Time.deltaTime);
        }
    }
}
