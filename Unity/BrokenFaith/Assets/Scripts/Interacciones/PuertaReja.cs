using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaReja : MonoBehaviour
{
    [SerializeField] private GameObject Tabla;
    public float anguloApertura = 90f;
    public float velocidadApertura = 2f;

    private bool abriendo = false;
    private bool abierta;
    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;
    // Start is called before the first frame update
    void Start()
    {
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
        MovimientoJugador.PasarReja += Pasar;
    }
    private void OnDisable()
    {
        MovimientoJugador.PasarReja -= Pasar;
    }
    private void Pasar(bool puedePasar)
    {
        if (puedePasar)
        {
            if(Tabla != null && Tabla.GetComponent<Rigidbody>().isKinematic == true)
            Tabla.GetComponent<Rigidbody>().isKinematic = false;


            if (!abriendo && !abierta)
            {
                StopAllCoroutines();
                StartCoroutine(AbrirCerrarPuerta(rotacionAbierta));
                abierta = true;
            }
            else if (!abriendo && abierta)
            {
                StartCoroutine(AbrirCerrarPuerta(rotacionCerrada));
                abierta = false;
            }
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
        if (Tabla != null)
        {
            Destroy(Tabla);
        }
        transform.rotation = rotacionObjetivo;
        abriendo = false;
    }
}
