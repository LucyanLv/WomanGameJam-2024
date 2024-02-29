using System.Collections;
using UnityEngine;

public class ManagerTrabajadores : MonoBehaviour
{
    public Animator trabajador1;
    public Animator mensaje1;
    public Animator trabajador2;
    public Animator mensaje2;
    public Animator trabajador3;
    public Animator mensaje3;
    public Animator trabajador4;
    public Animator mensaje4;
    public Animator trabajador5;
    public Animator mensaje5;
    public Animator trabajador6;
    public Animator mensaje6;
    public Animator trabajador7;
    public Animator mensaje7;
    public Animator trabajador8;
    public Animator mensaje8;
    public Animator trabajador9;
    public Animator mensaje9;
    public Animator trabajador10;
    public Animator mensaje10;
    public Animator fondo;

    public float tiempoEspera;
    void Start()
    {
        StartCoroutine(Trabajadores());
    }
    IEnumerator Trabajadores()
    {
        //**TRABAJADOR #1**//
        yield return new WaitForSeconds(1F);
        trabajador1.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador1.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje1.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje1.SetBool("Salir", true);

        //**TRABAJADOR #2**//
        yield return new WaitForSeconds(1F);
        trabajador2.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador2.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje2.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje2.SetBool("Salir", true);

        //**TRABAJADOR #3**//
        yield return new WaitForSeconds(1f);
        trabajador3.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador3.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje3.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje3.SetBool("Salir", true);

        //**TRABAJADOR #4**//
        yield return new WaitForSeconds(1f);
        trabajador4.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador4.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje4.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje4.SetBool("Salir", true);

        //**TRABAJADOR #5**//
        yield return new WaitForSeconds(1f);
        trabajador5.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador5.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje5.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje5.SetBool("Salir", true);

        //**TRABAJADOR #6**//
        yield return new WaitForSeconds(1F);
        trabajador6.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador6.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje6.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje6.SetBool("Salir", true);

        //**TRABAJADOR #7**//
        yield return new WaitForSeconds(1f);
        trabajador7.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador7.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje7.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje7.SetBool("Salir", true);

        //**TRABAJADOR #8**//
        yield return new WaitForSeconds(1f);
        trabajador8.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador8.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje8.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje8.SetBool("Salir", true);

        //**TRABAJADOR #9**//
        yield return new WaitForSeconds(1F);
        trabajador9.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador9.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje9.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje9.SetBool("Salir", true);

        //**TRABAJADOR #10**//
        yield return new WaitForSeconds(1F);
        trabajador10.SetBool("Entrar", true);
        yield return new WaitForSeconds(tiempoEspera);
        trabajador10.SetBool("Salir", true);
        yield return new WaitForSeconds(1f);
        fondo.SetBool("FondoEntrar", true);
        mensaje10.SetBool("Entrar", true);
        yield return new WaitForSeconds(8);
        fondo.SetBool("FondoEntrar", false);
        mensaje10.SetBool("Salir", true);
    }
}
