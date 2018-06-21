using UnityEngine;
using UnityEngine.UI;
using System;

public class Debugger : MonoBehaviour
{
    static private Text TxtDebugger;
    static private bool maximizado;
    static private Vector3 inicio;
    static private GameObject panelDebugger;
    static private GameObject panelFps;
    static private GameObject btnLimpiar;
    static private GameObject btnInicio;
    static private int NumLinea;

    // Use this for initialization
    void Awake ()
    {
        TxtDebugger = GameObject.Find("TxtDebugger").GetComponent<Text>();
        maximizado = true;
        NumLinea = 0;
        
    }

    void Start()
    {
        btnLimpiar = GameObject.Find("BtnLimpiar");
        btnInicio = GameObject.Find("BtnInicio");
        panelDebugger = GameObject.Find("PanelDebugger");
        panelFps = GameObject.Find("PanelFps");

        inicio = TxtDebugger.transform.position;
        Restaurar();
    }

    public static void Escribir(string texto)
    {
        try
        {
            TxtDebugger.text += "\n" + (++NumLinea) + ".-" + texto;
            
        }
        catch(Exception e)
        {
            Debug.Log("Puede que esté desactivado el Debugger");
        }
    }

    public void Limpiar()
    {
        TxtDebugger.text = "Debugger:";
        TxtDebugger.transform.position = inicio;
        NumLinea = 0;
    }

    public void Inicio()
    {
        TxtDebugger.transform.position = inicio;
    }

    public void Restaurar()
    {
        if (maximizado)
        {
            btnLimpiar.SetActive(false);
            btnInicio.SetActive(false);
            panelDebugger.SetActive(false);
            panelFps.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            btnLimpiar.SetActive(true);
            btnInicio.SetActive(true);
            panelDebugger.SetActive(true);
            panelFps.SetActive(true);
        }

        maximizado = !maximizado;
    }
    
}
