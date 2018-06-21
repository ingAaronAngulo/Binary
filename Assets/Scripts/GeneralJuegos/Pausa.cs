//Código creado por Aarón Angulo

using UnityEngine;

public class Pausa : MonoBehaviour
{
    private GameObject controles;
    private GameObject BtnPausa;
    private Temporizador temporizador;
    private AnimPausa animaciones;

    void Awake()
    {
        animaciones = GetComponent<AnimPausa>();
        controles = GameObject.Find("ControlesPausa");
        temporizador = GameObject.Find("Temporizador").GetComponent<Temporizador>();
        BtnPausa = GameObject.Find("PausaBtn");
        new Dialogos().Pausa();
    }
    
    void Start()
    {

    }

    public void Pausar()
    {
        animaciones.Mostrar();
        BtnPausa.SetActive(false);
        temporizador.Detener();
    }

    public void Resumir()
    {
        animaciones.Ocultar();
        temporizador.Resumir();
        BtnPausa.SetActive(true);
    }

    public void Reiniciar()
    {
        Global.Reiniciar();
    }

    public void Salir()
    {
        Destroy(Global.juego);
        Global.CrearMenu();
    }
}
