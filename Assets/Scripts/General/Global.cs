//Código creado por Aarón Angulo

using UnityEngine;
using ChartboostSDK;

/**
 * @author Angulo Armenta Aarón Orlando (Penchi)
 * @mainpage
 * Codigo en camara principal que inicia generando el menú para escoger un modo de juego 
*/
public class Global : MonoBehaviour
{
    static private GameObject menu;
    static private GameObject reloj;
    static private GameObject suma;
    static private GameObject con;
    static public GameObject juego;
    static public GameObject ajustes;
    static public GameObject puntuaciones;
    static private Fondo fondo;
    static public string modoJuego;
    static public string varianteJuego;
    static public int puntuacionParcial;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("fps"))
            PlayerPrefs.SetInt("fps", 60);
        if (!PlayerPrefs.HasKey("primeraVez"))
            PlayerPrefs.SetInt("primeraVez", 0);
        Application.targetFrameRate = PlayerPrefs.GetInt("fps");
    }

    /**
    *   Se inicia procesando el método Iniciar()
    */
    void Start()
    {
        menu = Resources.Load("Prefabs/Menu", typeof(GameObject)) as GameObject;
        reloj = Resources.Load("Prefabs/Manager", typeof(GameObject)) as GameObject;
        suma = Resources.Load("Prefabs/Suma", typeof(GameObject)) as GameObject;
        con = Resources.Load("Prefabs/Conversor", typeof(GameObject)) as GameObject;
        ajustes = Resources.Load("Prefabs/Ajustes", typeof(GameObject)) as GameObject;
        puntuaciones = Resources.Load("Prefabs/Puntuaciones", typeof(GameObject)) as GameObject;
        juego = null;

        if (!PlayerPrefs.HasKey("publicidad"))
            PlayerPrefs.SetInt("publicidad", 0);
        
        Chartboost.cacheInterstitial(CBLocation.HomeScreen);
        fondo = GameObject.Find("Fondo").GetComponent<Fondo>();
        CrearMenu();
    }

    /**
    *   Genera el menú de selección de juego y opciones.
    *   Si se sale de un juego que usa la orientación landScape, éste orienta la pantalla a portrait
    */
    static public void CrearMenu()
    {
        modoJuego = "menu";
        varianteJuego = "normal";
        juego = Instantiate(menu, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        fondo.RepCambioColor(0);
    }
    
    static public void CrearJuego()
    {
        switch (modoJuego)
        {
            case "reloj":
                BinaryWatch();
                break;
            case "suma":
                BinarySum();
                break;
            case "conversor":
                BinaryCon();
                break;
        }
    }

    static public void Reiniciar()
    {
        DestroyImmediate(juego);
        CrearJuego();
    }

    /**
    *   Se activa el juego BinaryWatch con la variante pertinente y se destruye el menú
    *   @param Variante de juego seleccionada por el jugador
    */
    static public void BinaryWatch()
    {
        juego = Instantiate(reloj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        fondo.RepCambioColor(1);
    }

    /**
    *   Se activa el juego Sumatoria con la variante pertinente y se destruye el menú
    *   @param Variante de juego seleccionada por el jugador
    */
    static public void BinarySum()
    {
        juego = Instantiate(suma, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        fondo.RepCambioColor(2);
    }

    /**
    *   Se activa el juego Conversor con la variante pertinente y se destruye el menú
    *   @param Variante de juego seleccionada por el jugador
    */
    static public void BinaryCon()
    {
        juego = Instantiate(con, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        fondo.RepCambioColor(3);
    }

    /**
     * Despliega el menú de ajustes
     */
    static public void Ajustes()
    {
        juego = Instantiate(ajustes, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        fondo.RepCambioColor(4);
    }

    static public void Puntuaciones()
    {
        juego = Instantiate(puntuaciones, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }
}
