using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovEnemigo : MonoBehaviour
{
    #region Variables


    private float nivelDeAlerta;
    private float anguloVision;
    private float rangoMaximo;

    private GameObject jugador;

    private bool detectado;

    #endregion

    void Start()
    {
        ConfiguracionJuego.instance.RangoAudicion = 1000;
        anguloVision = ConfiguracionJuego.instance.AnguloVision;
        rangoMaximo = ConfiguracionJuego.instance.RangoMaximo;
        nivelDeAlerta = 0;
        jugador = ConfiguracionJuego.instance.Jugador;
        detectado = false;
 
    }

    
    void Update()
    {
        if (jugador.GetComponent<MovimientoJugador>().agachado == true && ConfiguracionJuego.instance.RangoMaximo > 5f)
            ConfiguracionJuego.instance.RangoMaximo /= 2;
        else if (jugador.GetComponent<MovimientoJugador>().agachado == false 
            && ConfiguracionJuego.instance.RangoMaximo != 10f)
            ConfiguracionJuego.instance.RangoMaximo = 20f;

        detectado = RangoDeVision();
    }

    private bool RangoDeVision()
    {
        Vector3 direccionJugador = jugador.transform.position - transform.position;
        float anguloEnemigoJugador = Vector3.Angle(transform.forward, direccionJugador.normalized);

        if (anguloEnemigoJugador < ConfiguracionJuego.instance.AnguloVision * 0.5f 
            && direccionJugador.magnitude <= ConfiguracionJuego.instance.RangoMaximo)
        {
            int layerMask = LayerMask.GetMask("Objeto", "Player");

            if (Physics.Raycast(transform.position, direccionJugador.normalized, 
                out RaycastHit hit, ConfiguracionJuego.instance.RangoMaximo, layerMask))
            {
                Debug.Log("Entra persigue");
                if (hit.collider.gameObject == jugador)
                {
                    Debug.Log("Entra persigue");
                    nivelDeAlerta += Time.deltaTime;
                    return true;
                }
            }

        }
        if (nivelDeAlerta > 0) nivelDeAlerta -= Time.deltaTime;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float halfFOV = anguloVision * 0.5f;

        Vector3 principio = Quaternion.Euler(0, -halfFOV, 0) 
            * transform.forward * rangoMaximo;
        Vector3 final = Quaternion.Euler(0, halfFOV, 0) 
            * transform.forward * rangoMaximo;

        Gizmos.DrawLine(transform.position, transform.position + principio);
        Gizmos.DrawLine(transform.position, transform.position + final);

        Gizmos.DrawLine(transform.position + principio, transform.position + final);
    }

    public bool GetDetectado()
    {
        return detectado;
    }

    private void OnEnable()
    {
        SoundEventManager.OnSoundEvent += OnSoundEvent;
    }

    private void OnDisable()
    {
        SoundEventManager.OnSoundEvent -= OnSoundEvent;
    }

    private void OnSoundEvent(SoundEvent soundEvent)
    {
        if (Vector3.Distance(transform.position, soundEvent.soundPosition) 
            <= ConfiguracionJuego.instance.RangoAudicion)
        {
            Debug.Log("Enemigo escuch� un sonido proveniente de " + soundEvent.soundPosition);
        }
    }

}
