using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class Dialogos : MonoBehaviour
{
    private JsonData json;

    /**
     * Se carga el archivo json para su posterior uso
     * Dependiendo de si se está usando el editor o Android se desarrolla el path
     */
    public Dialogos()
    {
        string path;
        string jsonCrudo;
        #if UNITY_EDITOR
            path = Application.streamingAssetsPath + "/Texto/" + Idioma() + ".txt";
            jsonCrudo = File.ReadAllText(path);
        #endif

        #if UNITY_ANDROID && !UNITY_EDITOR
        
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/Texto/" + Idioma() + ".txt");  // this is the path to your StreamingAssets in android
            while (!loadDB.isDone);
            jsonCrudo = loadDB.text;
        #endif

        json = JsonMapper.ToObject(jsonCrudo);
    }
	
    /**
     * Reemplaza el texto de los campos de texto por su pertinente dialogo del menú
     */
    public void Menu()
    {
        
        BuscarTxt("Reloj_Text").text = json["menu"]["reloj"].ToString();
        BuscarTxt("Sumatoria_Text").text = json["menu"]["suma"].ToString();
        BuscarTxt("Conversor_Text").text = json["menu"]["conversor"].ToString();
        BuscarTxt("Ajustes_Text").text = json["menu"]["ajustes"].ToString();
        BuscarTxt("Normal_Text").text = json["menu"]["normal"].ToString();
        BuscarTxt("Survival_Text").text = json["menu"]["survival"].ToString();
        BuscarTxt("Contrareloj_Text").text = json["menu"]["contrareloj"].ToString();
        BuscarTxt("DescripcionNormal_Text").text = json["menu"]["descripNormal"].ToString();
        BuscarTxt("DescripcionSupervivencia_Text").text = json["menu"]["descripSupervivencia"].ToString();
        BuscarTxt("DescripcionContraReloj_Text").text = json["menu"]["descripContraReloj"].ToString();
    }

    public void Pausa()
    {
        BuscarTxt("TxtResumir").text = json["pausa"]["resumir"].ToString();
        BuscarTxt("TxtReiniciar").text = json["pausa"]["reiniciar"].ToString();
        BuscarTxt("TxtSalir").text = json["pausa"]["salir"].ToString();
    }
    /**
     * Reemplaza el texto de los campos de texto por su pertinente dialogo de la pantalla de ajustes
     */
    public void Ajustes()
    {
        BuscarTxt("BorrarPuntuaciones_Text").text = json["ajustes"]["borrar"].ToString();
    }

    public string[] AvisoFps()
    {
        string[] s = new string[2];
        s[0] = json["aviso"]["tituloFps"].ToString();
        s[1] = json["aviso"]["cuerpoFps"].ToString();
        return s;
    }

    /**
     * Reemplaza el texto de los campos de texto por su pertinente dialogo de las puntuaciones
     */
    public void Puntuaciones()
    {
        BuscarTxt("MejorPuntuacion_Text").text = json["puntuaciones"]["mejorPuntuacion"].ToString();
        BuscarTxt("EstaPartida_Text").text = json["puntuaciones"]["estaPartida"].ToString();
    }
    
    public string[] AvisoNoAds()
    {
        string[] s = new string[2];
        s[0] = json["aviso"]["tituloNoAds"].ToString();
        s[1] = json["aviso"]["cuerpoNoAds"].ToString();
        return s;
    }

    public string[] AvisoAds()
    {
        string[] s = new string[2];
        s[0] = json["aviso"]["tituloAds"].ToString();
        s[1] = json["aviso"]["cuerpoAds"].ToString();
        return s;
    }

    public string[] TutorialReloj()
    {
        string[] s = new string[6];
        s[0] = json["tutoriales"]["reloj1"].ToString();
        s[1] = json["tutoriales"]["reloj2"].ToString();
        s[2] = json["tutoriales"]["reloj3"].ToString();
        s[3] = json["tutoriales"]["reloj4"].ToString();
        s[4] = json["tutoriales"]["siguiente"].ToString();
        s[5] = json["tutoriales"]["cerrar"].ToString();

        return s;
    }

    public string[] TutorialSumatoria()
    {
        string[] s = new string[2];
        s[0] = json["tutoriales"]["sumatoria"].ToString();
        s[1] = json["tutoriales"]["cerrar"].ToString();

        return s;
    }

    public string[] TutorialConversor()
    {
        string[] s = new string[4];
        s[0] = json["tutoriales"]["conversor1"].ToString();
        s[1] = json["tutoriales"]["conversor2"].ToString();
        s[2] = json["tutoriales"]["siguiente"].ToString();
        s[3] = json["tutoriales"]["cerrar"].ToString();

        return s;
    }
    public string Comenzar()
    {
        return json["juegos"]["comenzar"].ToString();
    }

    /**
     * Carga el último idioma seleccionado por el usuario
     */
    public string Idioma()
    {
        if (!PlayerPrefs.HasKey("idioma"))
            PlayerPrefs.SetString("idioma", Application.systemLanguage.ToString());

        return PlayerPrefs.GetString("idioma");
    }

    /**
     * Busca el GameObject dado por el parametro y devuelve su componente UnityEngine.UI.Text
     * @param nombre Es el nombre del GameObject que se desea buscar
     * @return El componente UnityEngine.UI.Text del GameObject que se buscó
     */
    public Text BuscarTxt(string nombre)
    {
        return GameObject.Find(nombre).GetComponent<Text>();
    }
}