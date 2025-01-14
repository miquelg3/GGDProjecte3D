using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfiguracionJuego : MonoBehaviour
{
    #region Variables

    public static ConfiguracionJuego instance;

    // Para MovimientoJugador.cs
    [Header("MovimientoJugador.cs")]
    [SerializeField] private Camera camaraPrincipal;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private TextMeshProUGUI nombreObjetoTexto;
    [SerializeField] private GameObject pausa;
    [SerializeField] private GameObject muerto;
    [SerializeField] private GameObject inventarioMenu;
    [SerializeField] private GameObject inventarioMenuExterno;
    [SerializeField] private Transform panelInventario;
    [SerializeField] private Transform panelInventarioExterno;

    [Header("Movimiento jugador")]
    [SerializeField] private GameObject jugador;
    [SerializeField] private float velocidad = 5.0f;
    [SerializeField] private float multiplicadorSprint = 2.0f;
    [SerializeField] private float velocidadH = 3;
    [SerializeField] private float velocidadV = 3;

    [Header("Cabeceo Jugador")]
    [SerializeField] private float bobbingSpeed = 10f;
    [SerializeField] private float bobbingAmount = 0.5f;
    [SerializeField] private float midPoint = 0.5f;
    [SerializeField] private float timer = 0;

    [Header("Fov y audicion del Enemigo ")]
    [SerializeField] private float rangoMaximo = 10f;
    [SerializeField] private float anguloVision = 45f;
    [SerializeField] private LayerMask layermaskObjeto;
    [SerializeField] private LayerMask layermaskJugador;
    [SerializeField] private float rangoAudiocion;

    [Header("Inclinacion Jugador")]
    [SerializeField] private float velocidadPeek = 40f;
    [SerializeField] private float anguloMaximo = 20f;

    // Im�genes inventario
    [Header("Imagenes Inventario")]
    [SerializeField] private Sprite espadaImg;
    [SerializeField] private Sprite arcoImg;
    [SerializeField] private Sprite pistaImg;

    // Ambiente
    [Header("Ambiente")]
    [SerializeField] private float gravedad = -9.81f;
    [SerializeField] private float alturaSalto = 1f;

    //Sonido
    [Header("Musica y Efectos de Sonido Configuracion")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider sliderMusica;
    [SerializeField] private Slider sliderSFX;

    // Armas
    [Header("Armas equipables")]
    [SerializeField] private GameObject espadaFPS;
    [SerializeField] private GameObject arcoFPS;

    public GameObject linternaJugador;

    #endregion

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (sliderMusica != null || sliderSFX != null)
        {
            sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
            sliderSFX.onValueChanged.AddListener(CambiarVolumenSFX);
        }
    }

    private void CambiarVolumenMusica(float valor)
    {
        mixer.SetFloat("MixerMusica", Mathf.Log10(valor) * 20);
    }

    private void CambiarVolumenSFX(float valor)
    {
        mixer.SetFloat("MixerSFX", Mathf.Log10(valor) * 20);
    }


    // Para MovimientoJugador.cs
    // Descomentar si queremos que las variables sean privadas
    /*public Transform CameraTransform()
    {
        return cameraTransform;
    }

    public TextMeshProUGUI NombreObjetoTexto()
    {
        return nombreObjetoTexto;
    }

    public GameObject Pausa()
    {
        return pausa;
    }

    public GameObject InventarioMenu()
    {
        return inventarioMenu;
    }*/


    // Im�genes del inventario
    public List<Sprite> ImagenesInventario()
    {
        List<Sprite> imagenes = new List<Sprite>();
        imagenes.Add(espadaImg);
        imagenes.Add(arcoImg);
        imagenes.Add(pistaImg);
        return imagenes;
    }

    #region Getter and Setters

    public Camera CamaraPrincipal
    {
        get { return camaraPrincipal; }
    }
    public GameObject Jugador
    {
        get { return jugador; }
    }

    public Transform CamaraTransform
    {
        get { return cameraTransform; }
    }

    public TextMeshProUGUI NombreObjetoTexto
    {
        get {return nombreObjetoTexto;} 
    }

    public GameObject PanelPausa
    {
        get { return pausa;}
    }

    public GameObject PanelMuerto
    {
        get { return muerto; }
    }

    public GameObject InventarioMenu
    {
        get { return inventarioMenu;}
    }

    public GameObject InventarioMenuExterno
    {
        get { return inventarioMenuExterno; }
    }

    public Transform TransformPanelInventario
    {
        get { return panelInventario; }
    }

    public Transform TransformPanelInventarioExterno
    {
        get { return panelInventarioExterno;}
    }

    public float Gravedad
    {
        get { return gravedad; }
        set { gravedad = value; }
    }

    public float AlturaSalto
    {
        get { return alturaSalto; }
        set { alturaSalto = value; }
    }

    public Sprite EspadaImg 
    { 
        get { return espadaImg; } 
    }

    public Sprite ArcoImg
    {
        get { return arcoImg; }
    }

    public Sprite PistaImg
    {
        get { return pistaImg; }
    }

    public float Velocidad
    {
        get { return velocidad; }
        set {  velocidad = value;}
    }

    public float MultiplicadorSprint
    {
        get { return multiplicadorSprint; }
        set { multiplicadorSprint = value; }
    }

    public float VelocidadH
    {
        get { return velocidadH; }
        set {  velocidadH = value; }
    }

    public float VelocidadV
    {
        get { return velocidadV; }
        set { velocidadV = value; }
    }

    public float BobbingSpeed
    {
        get { return bobbingSpeed; }
        set { bobbingSpeed = value; }
    }

    public float BobbingAmount
    {
        get { return bobbingAmount; }
        set { bobbingAmount = value; }
    }

    public float MidPoint
    {
        get { return midPoint; }
        set { midPoint = value; }
    }

    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }

    public float VelocidadPeek
    {
        get { return velocidadPeek; }
        set { velocidadPeek = value;}
    }

    public float AnguloMaximo
    {
        get { return anguloMaximo; }
        set { anguloMaximo = value; }
    }

    public float RangoMaximo
    {
        get { return rangoMaximo; }
        set {  rangoMaximo = value; }
    }

    public float AnguloVision
    {
        get { return anguloVision; }
        set { anguloVision = value; }
    }

    public LayerMask LayermaskObjeto
    {
        get { return layermaskObjeto; }
        set { layermaskObjeto = value; }
    }

    public LayerMask LayermaskJugador
    {
        get { return layermaskJugador; }
        set { layermaskJugador = value; }
    }

    public float RangoAudicion
    {
        get { return rangoAudiocion; }
        set { rangoAudiocion = value; }
    }

    public GameObject EspadaFPS
    {
        get { return espadaFPS; }
    }

    public GameObject ArcoFPS
    {
        get { return arcoFPS; }
    }

    #endregion

}
