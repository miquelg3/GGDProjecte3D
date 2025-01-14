using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    public float anguloApertura = 90f; 
    public float velocidadApertura = 2f; 

    private bool abriendo = false;
    private bool abierta;
    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;
    private bool Cerrado;

    // Start is called before the first frame update
    void Start()
    {
        Cerrado = false;
        abierta = false;
        rotacionCerrada = transform.rotation;
        rotacionAbierta = rotacionCerrada * Quaternion.Euler(0, anguloApertura, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        MovimientoJugador.AbrirPuerta += Abrir;
        ActivarJefe.ActivarBoss += CerrarJefe;
    }
    private void OnDisable()
    {
        MovimientoJugador.AbrirPuerta -= Abrir;
        ActivarJefe.ActivarBoss -= CerrarJefe;
    }
    private void Abrir(Transform PosicionObjeto)
    {
        if (transform.position == PosicionObjeto.position)
        {
            Debug.Log(transform.rotation.y);
            if (!abriendo && !abierta)
            {
                StopAllCoroutines();
                StartCoroutine(AbrirCerrarPuerta(rotacionAbierta));
                abierta = true;
            }
            else if (!abriendo && abierta)
            {
                StopAllCoroutines();
                StartCoroutine(AbrirCerrarPuerta(rotacionCerrada));
                abierta = false;
            }
        }
        
    }
    private void CerrarJefe(bool HaEntrado)
    {
        if (HaEntrado)
        {
                Cerrado = true;
                StartCoroutine(AbrirCerrarPuerta(rotacionCerrada));
                abierta = false;
        }
    }
    private IEnumerator AbrirCerrarPuerta(Quaternion rotacionObjetivo)
    {
        abriendo = true;
        Quaternion rotacionInicial = transform.rotation;
        float tiempoTranscurrido = 0;

        while (tiempoTranscurrido < velocidadApertura)
        {
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionObjetivo, tiempoTranscurrido / velocidadApertura);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        transform.rotation = rotacionObjetivo;
        abriendo = false;
        abierta = rotacionObjetivo == rotacionAbierta;
        if (Cerrado == true)
        {
            GetComponent<Puertas>().enabled = false;
        }
    }

}
