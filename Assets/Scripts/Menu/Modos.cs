//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

public class Modos : MonoBehaviour
{
    private Animaciones reloj;
    private Animaciones sumatoria;
    private Animaciones conversor;
    private Animaciones ajustes;
    private Animaciones ads;

    // Use this for initialization
    void Awake()
    {
        reloj = GameObject.Find("RelojMenu").GetComponent<Animaciones>();
        sumatoria = GameObject.Find("SumatoriaMenu").GetComponent<Animaciones>();
        conversor = GameObject.Find("ConversorMenu").GetComponent<Animaciones>();
        ajustes = GameObject.Find("AjustesMenu").GetComponent<Animaciones>();
        ads = GameObject.Find("ADSMenu").GetComponent<Animaciones>();
    }
    void Start ()
    {
        
    }

    public void Aparicion()
    {
        reloj.RepAparicion();
        sumatoria.RepAparicion();
        conversor.RepAparicion();
        ajustes.RepAparicion();
        ads.RepAparicion();
    }

    public void Desaparicion()
    {
        reloj.RepDesaparicion();
        sumatoria.RepDesaparicion();
        conversor.RepDesaparicion();
        ajustes.RepDesaparicion();
        ads.RepDesaparicion();
    }
}
