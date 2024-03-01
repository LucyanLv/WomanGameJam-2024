using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTrabajadores : MonoBehaviour
{
    public Animator[] trabajadores;
    public Animator[] mensajes;
    public Animator fondo;
    public float tiempoEspera;
    public float tiempoTotal;

    //Cronometro
    [SerializeField] private float tiempoActual;
    [SerializeField] private bool tiempoActivado = false;
    [SerializeField] private Slider slider;
    // Boton
    public GameObject boton;

    void Start()
    {
        StartCoroutine(EjecutarTrabajadores());
    }

    private void Update()
    {
        if (tiempoActivado)
        {
            Contador();
        }
    }
    private void Contador()
    {
        tiempoActual -= Time.deltaTime;
        if (tiempoActual >= 0)
        {
            slider.value = tiempoActual;
        }
        if (tiempoActual <= 0)
        {
            Debug.Log("DEVUELVE");
            tiempoActual = tiempoEspera;
        }
    }
    IEnumerator EjecutarTrabajadores()
    {
        while (true)
        {
            // Recorrer todos los trabajadores
            for (int i = 0; i < trabajadores.Length; i++)
            {
                tiempoActivado = true;
                slider.maxValue = tiempoEspera;
                // Entrada del trabajador
                trabajadores[i].SetBool("Entrar", true);
                yield return new WaitForSeconds(tiempoEspera);

                // Salida del trabajador
                trabajadores[i].SetBool("Salir", true);
                tiempoActivado = false;
                yield return new WaitForSeconds(1);

                // Animación del mensaje
                fondo.SetBool("FondoEntrar", true);
                mensajes[i].SetBool("Entrar", true);
                yield return new WaitForSeconds(tiempoTotal);
                fondo.SetBool("FondoEntrar", false);
                mensajes[i].SetBool("Salir", true);
                yield return new WaitForSeconds(1.5f);
            }
            boton.SetActive(true);
        }
    }

}
