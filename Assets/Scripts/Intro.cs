using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{

    public Text texto;

    public GameObject imagenDormido;
    public GameObject imagenDespierto;

    public GameObject botonAceptar;
    public GameObject botonSi;
    public GameObject botonNo;

    public GameObject databaseObject;
    Database database;

    bool si;

    int counter;

    string nombre;

    void Start()
    {
        database = databaseObject.GetComponent<Database>();

        nombre = database.LoadPlayerName();

        si = true;
        texto.text = "Mmmmmm...";
        counter = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            BotonHandler();
            if (botonSi.active)
            {
                ButtonSi();
            }
        }
    }

    public void BotonHandler()
    {
        switch (counter)
        {
            case 1:
                texto.text = "Mmmmmmmmmmmmmmmmm...";
                break;
            case 2:
                imagenDormido.GetComponent<Image>().enabled = false;
                imagenDespierto.GetComponent<Image>().enabled = true;
                texto.text = "Ohhhhhhhh, parece que me he dormido.";
                break;
            case 3:
                texto.text = "No me des esos sustos, jovencito!";
                break;
            case 4:
                botonAceptar.GetComponent<Button>().enabled = false;
                botonAceptar.GetComponent<Image>().enabled = false;
                botonSi.GetComponent<Button>().enabled = true;
                botonSi.GetComponent<Image>().enabled = true;
                botonNo.GetComponent<Button>().enabled = true;
                botonNo.GetComponent<Image>().enabled = true;
                texto.text = "Oh, lo olvidaba. No puedes oirme, verdad?";
                break;
            case 5:
                botonAceptar.GetComponent<Button>().enabled = true;
                botonAceptar.GetComponent<Image>().enabled = true;
                botonSi.GetComponent<Button>().enabled = false;
                botonSi.GetComponent<Image>().enabled = false;
                botonNo.GetComponent<Button>().enabled = false;
                botonNo.GetComponent<Image>().enabled = false;
                if (si)
                {
                    texto.text = "Que sorpresa! Hacia mucho tiempo que nadie podia escuchar mi voz.";
                }
                else
                {
                    texto.text = "Me tomas por tonto? Se que acabas de escuchar lo que he dicho.";
                }
                break;
            case 6:
                texto.text = "Quien eres? Como te llamas?";
                break;
            case 7:
                texto.text = "Ya veo. Asi que tu nombre es " + nombre + " y dices que vienes desde un reino lejano.";
                break;
            case 8:
                texto.text = "Que por que me sorprende que puedas eschuchar mi voz?";
                break;
            case 9:
                texto.text = "Este reino ha sido asolado por una maldicion, hace ya mucho tiempo.";
                break;
            case 10:
                texto.text = "La maldicion trajo consigo hordas de monstruos, que ahora campan a sus anchas por todo el reino.";
                break;
            case 11:
                texto.text = "Pero ese no es el mayor problema. Invadio la capital y se hizo con el control del reino, privandolo de sonido.";
                break;
            case 12:
                texto.text = "Desde ese aciago dia, nadie ha vuelto a escuchar ni la mas minima palabra.";
                break;
            case 13:
                texto.text = "Hasta que has llegado tu. Yo solo podia escuchar mi voz, pero encontrarte hoy me ha dado esperanzas.";
                break;
            case 14:
                texto.text = "Este encuentro no ha sido fortuito. Creo que eres capaz de grandes cosas.";
                break;
            case 15:
                texto.text = "Se que pido mucho, pero tu ayuda podria resultarnos de vital importancia.";
                break;
            case 16:
                texto.text = "La mision que voy a encomendarte te pondra en situaciones peligrosas. Aun asi...";
                break;
            case 17:
                botonAceptar.GetComponent<Button>().enabled = false;
                botonAceptar.GetComponent<Image>().enabled = false;
                botonSi.GetComponent<Button>().enabled = true;
                botonSi.GetComponent<Image>().enabled = true;
                botonNo.GetComponent<Button>().enabled = true;
                botonNo.GetComponent<Image>().enabled = true;
                texto.text = "Estas dispuesto a luchar por este reino y devolverle el sonido que perdio?";
                break;
            case 18:
                botonAceptar.GetComponent<Button>().enabled = true;
                botonAceptar.GetComponent<Image>().enabled = true;
                botonSi.GetComponent<Button>().enabled = false;
                botonSi.GetComponent<Image>().enabled = false;
                botonNo.GetComponent<Button>().enabled = false;
                botonNo.GetComponent<Image>().enabled = false;
                if (si)
                {
                    texto.text = "Ya sabia yo que podia confiar en ti! Te esperare en la entrada del bosque para darte instrucciones.";
                }
                else 
                {
                    texto.text = "Bueno, ya sabia que era muy dificil que aceptaras. Te esperare a la entrada del bosque por si cambias de opinion.";
                }
                break;
            case 19:
                    texto.text = "Por cierto. Bienvenido a Symphonia!";
                break;
            case 20:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Malarcier");
                break;
            default:
                texto.text = "";
                break;
        }
        counter++;
    }

    public void ButtonSi()
    {
        si = true;
    }

    public void ButtonNo()
    {
        si = false;
    }
}
