using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarParticulas : MonoBehaviour
{
    public GameObject particula1;
    public GameObject particula2;
    public GameObject particula3;
    public GameObject particula4;
    public GameObject particula5;
    public GameObject particula6;
    public GameObject particula7;
    public GameObject particula8;
    public GameObject particula9;
    public GameObject particula10;
    public GameObject particula11;
    public GameObject particula12;
    public GameObject particula13;
    public GameObject particula14;
    public GameObject particula15;
    public GameObject particula16;
    public GameObject particula17;
    public GameObject particula18;
    public GameObject particula19;
    public GameObject particula20;

    void Start()
    {
        StartCoroutine(activarParticulas());
    }
    IEnumerator activarParticulas()
    {
        yield return new WaitForSeconds(0.25f);
        particula1.SetActive(true);
        particula11.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        particula2.SetActive(true);
        particula12.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        particula3.SetActive(true);
        particula13.SetActive(true);
        yield return new WaitForSeconds(1f);
        particula4.SetActive(true);
        particula14.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        particula5.SetActive(true);
        particula15.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        particula6.SetActive(true);
        particula16.SetActive(true);
        yield return new WaitForSeconds(1.75f);
        particula17.SetActive(true);
        particula7.SetActive(true);
        yield return new WaitForSeconds(2f);
        particula18.SetActive(true);
        particula8.SetActive(true);
        yield return new WaitForSeconds(2.25f);
        particula19.SetActive(true);
        particula9.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        particula20.SetActive(true);
        particula10.SetActive(true);
    }
}
