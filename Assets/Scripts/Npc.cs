using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string name = "Wolfgang";

    public string[][] conversacion;
    public string[] presentacion;
    public string[] misionNoCompletada;
    public string[] mision1;
    public string[] mision2;
    public string[] mision3;
    public string[] despedida;
    public string[] noHayMisiones;

    public bool[] dialogos;

    void Start()
    {
        CargarDialogos();
    }

    void CargarDialogos()
    {
        if (name.Equals("Wolfgang"))
        {
            CargarPresentacion();
            CargarMision1();
            CargarMision2();
            CargarMision3();
            CargarDespedida();
            CargarNoHayMisiones();
            CargarMisionNoCompletada();
            CargarConversacion();
        }
    }

    void CargarPresentacion()
    {
        presentacion = new string[]
            { 
                "Oh, eres tu otra vez. Asi que te has decidido a aceptar mi mision.", 
                "En ese caso, voy a tener que ponerte a prueba para saber de que estas hecho.",
                "Estamos en el bosque de Malarcier, la zona mas al sur del reino de Symphonia. Ten cuidado, hay muchos enemigos rondando por aqui.",
                "Habla conmigo cuando quieras empezar."
            };
    }

    void CargarMisionNoCompletada()
    {
        misionNoCompletada = new string[]
            {
                "Parece que todavia no has completado la mision que te mande. Vuelve cuando lo hayas hecho."
            };
    }

    void CargarMision1()
    {
        mision1 = new string[]
            {
                "Preparado para empezar? Fenomenal!",
                "Primero, me gustaria que consiguieras algunas provisiones. Te facilitaran mucho las cosas.",
                "Sal ahi fuera y recoge al menos tres objetos. Cuando los tengas, vuelve a hablar conmigo."
            };
    }

    void CargarMision2()
    {
        mision2 = new string[]
            {
                "A ver que tenemos aqui...",
                "Estupendo! Has recogido los objetos que te pedi. Puedes acceder a tu inventario para usarlos con la tecla I.",
                "Ahora estas listo para la siguiente mision. Unos Champimudos mas fuertes de lo normal han aparecido en esta zona.",
                "Me gustaria que eliminases a algunos. Si acabas con 5 de ellos, creo que esta zona sera bastante mas segura.",
                "Ven a hablar conmigo entoces."
            };
    }

    void CargarMision3()
    {
        mision3 = new string[]
            {
                "Si no me equivoco, has acabado con todos los Champimudos que te pedi! Seguro que ahora eres mucho mas fuerte.",
                "Puedes comprobar tu nivel en la esquina superior izquierda de la pantalla, o accediendo a tu menu con la tecla C.",
                "Dicho esto, voy a encomendarte la ultima mision. Quiero que encuentres y derrotes a la Trifauces que vive en este bosque.",
                "Cuando lo hayas conseguido, ven a verme."
            };
    }

    void CargarDespedida()
    {
        despedida = new string[]
            {
                "Parece que has completado con exito la tarea que te encomende. Estas hecho todo un valiente!",
                "Despues de tan estelar actuacion, creo que estas preparado para realizar ese trabajo del que hablamos antes.",
                "Podras derrotar a la maldicion y devolverle el sonido al reino? Yo no tengo la menor duda!",
                "Para llegar hasta la capital, primero tendras que atravesar el bosque de Malarcier.",
                "Pero ve con cuidado, dicen que un feroz enemigo habita en lo mas profundo del bosque.",
                "Aunque visto lo visto, seguro que consigues salir victorioso. Yo seguire por aqui un rato mas. Buena suerte!"
            };
    }

    void CargarNoHayMisiones()
    {
        noHayMisiones = new string[]
            {
                "No tengo mas misiones que encomendarte. Buena suerte!"
            };
    }

    void CargarConversacion()
    {
        conversacion = new string[][]
            {
                presentacion, mision1, mision2, mision3, despedida, noHayMisiones
            };
    }
}
