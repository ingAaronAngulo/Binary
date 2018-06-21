//Código creado por Aarón Angulo

using UnityEngine;

/**
 * @author Penchi 
 * @mainpage
 * Maneja la logica en la pantalla de selección de modo de juego
 */
public class Menu : MonoBehaviour
{
    public Modos animacionesModo;
    public Animaciones animacionesVariantes;

    private bool crearJuego;

    public void Start()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft)
            Screen.orientation = ScreenOrientation.Portrait;
        crearJuego = false;
        animacionesModo = GameObject.Find("Modos").GetComponentInChildren<Modos>();
        animacionesVariantes = GameObject.Find("Variantes").GetComponentInChildren<Animaciones>();
        new Dialogos().Menu();
        animacionesModo.Aparicion();
    }

    public void Update()
    {
        if (crearJuego)
            if (!animacionesVariantes.visible)
            {
                Global.CrearJuego();
                Destroy(gameObject);
            }
    }
    /**
     * Asigna el modo de juego y despliega las variantes de juego
     * Se ejecuta cuando el jugador selecciona un modo de juego y está listo para escoger la variante
     * @param El modo de juego que seleccionó el jugador
     */
    public void AsignarModo(string modoSelec)
    {
        Global.modoJuego = modoSelec;
    }

    /**
     * Activa los botones de las variantes de juego para poder seleccionarlas
     */
    public void ActivarVariantes()
    {
        animacionesModo.Desaparicion();
        animacionesVariantes.RepAparicion();
    }
    
    public void DesActivarVariantes()
    {
        animacionesVariantes.RepDesaparicion();
        animacionesModo.Aparicion();
    }

    public void CrearJuego(string varianteSelec)
    {
        animacionesVariantes.RepDesaparicion();
        Global.varianteJuego = varianteSelec;
        crearJuego = true;
    }
    /**
     * Llama al método para crear la pantalla de ajustes
     */
     public void LlamarAjustes()
    {
        animacionesModo.Desaparicion();
    }
}
