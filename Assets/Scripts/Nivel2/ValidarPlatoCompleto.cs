using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidarPlatoCompleto : MonoBehaviour
{
    public ValidarPlato validarPlato1;
    public ValidarPlato validarPlato2;
    public ValidarPlato validarPlato3;
    public ValidarPlato validarPlato4;
    public ValidarPlato validarVaso;

    void Update()
    {
        if (validarPlato1.platoCorrecto)
        {
            if (validarPlato2.platoCorrecto)
            {
                if (validarPlato3.platoCorrecto)
                {
                    if (validarPlato4.platoCorrecto)
                    {
                        if (validarVaso.platoCorrecto)
                        {
                            Debug.Log("EL PLATO ESTA BIEN ARMADO");
                        }
                        else
                        {
                            Debug.Log("MAL");
                        }
                    }
                    else
                    {
                        Debug.Log("MAL");
                    }
                }
                else
                {
                    Debug.Log("MAL");
                }
            }
            else
            {
                Debug.Log("MAL");
            }
        }
        else
        {
            Debug.Log("MAL");
        }
    }
}
