using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoBasico : MonoBehaviour
{
    #region Variables

    [Header("Movimiento")]
    [SerializeField] private float rangoMovimiento = 10f;
    [SerializeField] private bool porPuntos;
    [SerializeField] Vector3[] puntosNavegacion;
    [SerializeField] private float rangoAtaque = 3f;
    [SerializeField] private float tiempoEspera = 5f;

    [SerializeField] GameObject Patas;

    private Animator animator;

    private bool llegoADestino = false;
    private bool estaCaminando = false;
    private bool perseguir = false;

    private int indexPuntos = 0;

    private NavMeshAgent agente;

    private FovEnemigo fovEnemigo;

    private Transform jugador;
 
    #endregion

    void Start()
    {
        fovEnemigo = GetComponent<FovEnemigo>();
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        jugador = ConfiguracionJuego.instance.Jugador.transform;
        IniciarPatrullaje();
    }

    void Update()
    {
        if (fovEnemigo.GetDetectado())
        {
            perseguir = true;
            PerseguirJugador();
        }

        if (!fovEnemigo.GetDetectado() && !perseguir)
        {
            if (!llegoADestino)
            {
                if (porPuntos)
                    PatrullajePorPuntos();
                else
                    PatrullajeLibre();
            }

        }

        if (estaCaminando)
        {
            Patas.GetComponent<AudioSource>().Play();
        } else
        {
            Patas.GetComponent<AudioSource>().Stop();
        }

        animator.SetBool("caminando", estaCaminando);

    }

    private void PerseguirJugador()
    {
        Debug.Log("PERSIGUE");
        estaCaminando = true;

        Debug.Log(Vector3.Distance(transform.position, jugador.position));

        if (Vector3.Distance(transform.position, jugador.position) <= rangoAtaque)
        {
            Debug.Log("Ataca");
            animator.SetBool("atacando", true);
            estaCaminando = false;
        }
        else
        {
            estaCaminando = true;
            animator.SetBool("atacando", false);
            agente.SetDestination(jugador.position);
        }
        
    }

    private void PatrullajePorPuntos()
    {
        if (puntosNavegacion.Length == 0) return;

        if (Vector3.Distance(transform.position, puntosNavegacion[indexPuntos]) < 1f)
        {
            if (!llegoADestino)
            {
                llegoADestino = true;
                estaCaminando = false;
                Invoke("CambiarPuntoPatrullaje", tiempoEspera);
            }
        }
        else
        {
            agente.SetDestination(puntosNavegacion[indexPuntos]);
            estaCaminando = true;
        }
    }

    private void PatrullajeLibre()
    {
        if (Vector3.Distance(transform.position, agente.destination) < 1f)
        {
            if (!llegoADestino)
            {
                llegoADestino = true;
                estaCaminando = false;
                Invoke("BuscarNuevoDestino", tiempoEspera);
            }
        }
    }

    private void BuscarNuevoDestino()
    {
        llegoADestino = false;
        Vector3 randomDirection = Random.insideUnitSphere * rangoMovimiento;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, rangoMovimiento, 1);
        Vector3 finalPosition = hit.position;
        agente.SetDestination(finalPosition);
        estaCaminando = true;
    }

    private void IniciarPatrullaje()
    {
        estaCaminando = true;
        if (porPuntos && puntosNavegacion.Length > 0)
        {
            agente.SetDestination(puntosNavegacion[indexPuntos]);
        }
        else
        {
            BuscarNuevoDestino();
        }
    }

    private void CambiarPuntoPatrullaje()
    {
        llegoADestino = false;
        indexPuntos = (indexPuntos + 1) % puntosNavegacion.Length;
    }

}
