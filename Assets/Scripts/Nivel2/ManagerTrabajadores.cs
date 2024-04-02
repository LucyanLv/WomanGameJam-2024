using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerTrabajadores : MonoBehaviour
{
    public Animator[] trabajadores;
    public Animator[] mensajes;
    public Animator fondo;
    public float tiempoEspera;
    public float tiempoTotal;

    // Cronometro
    [SerializeField] private float tiempoActual;
    [SerializeField] private bool tiempoActivado = false;
    [SerializeField] private Slider slider;
    public GameObject sliderReloj;

    // Boton
    public GameObject boton;
    public Animator mensajeFinal;
    public Animator mensajeMenu;

    // Tutorial
    public Animator mensaje1;
    public Animator mensaje2;
    public Animator fondoGris;
    public Animator fondoNegro;

    public GameObject activarClientes;
    public TMP_Text clientes;
    public string[] nombresClientes; // Lista de nombres de clientes

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
        yield return new WaitForSeconds(1);
        fondoNegro.SetBool("FondoEntrar", true);
        yield return new WaitForSeconds(0.7f);
        mensaje1.SetBool("Entrar", true);
        yield return new WaitForSeconds(14);
        mensaje1.SetBool("Salir", true);
        yield return new WaitForSeconds(1.5f);
        mensaje2.SetBool("Entrar", true);
        yield return new WaitForSeconds(4);
        mensaje2.SetBool("Salir", true);
        fondoGris.SetBool("SalirGris", true);
        fondoNegro.SetBool("FondoEntrar", false);
        yield return new WaitForSeconds(2);
        activarClientes.SetActive(true);

        int clienteIndex = 0; // Índice del cliente actual
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

                // Actualizar el texto de los clientes
                if (clienteIndex < nombresClientes.Length)
                {
                    clientes.text = nombresClientes[clienteIndex];
                    clienteIndex++;
                }

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

            sliderReloj.SetActive(false);
            activarClientes.SetActive(false);
            fondoNegro.SetBool("FondoEntrar", true);
            mensajeFinal.SetBool("Entrar", true);
            yield return new WaitForSeconds(10f);
            mensajeFinal.SetBool("Salir", true);
            yield return new WaitForSeconds(1f);
            mensajeMenu.SetBool("Entrar", true);
            yield return new WaitForSeconds(1f);
            boton.SetActive(true);
        }
    }
}
