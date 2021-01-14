using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Combate : MonoBehaviour
{
    bool activeAzul = false;
    bool activeRojo = false;
    bool activeNaranja = false;
    bool activeVerde = false;
    GameObject notaAzul;
    GameObject notaVerde;
    GameObject notaRoja;
    GameObject notaNaranja;

    GameObject jugadorObject;
    Player jugador;

    public GameObject champimudoObject;
    public GameObject champimudoFuriosoObject;
    public GameObject florhadaObject;
    public GameObject trifaucesObject;
    public GameObject scarabObject;
    Enemy enemigo;

    public GameObject hpBarObject;
    HPBar hpBar;

    public GameObject xpBarObject;
    XPBar xpBar;

    public GameObject hpEnemyBarObject;
    HPBar hpEnemyBar;

    public Text nivelText;

    public Text experienciaGanada;

    public GameObject mensajeVictoria;
    public GameObject mensajeDerrota;

    public GameObject databaseObject;
    Database database;

    public GameObject SonidoAzul;
    public GameObject SonidoRojo;
    public GameObject SonidoNaranja;
    public GameObject SonidoVerde;

    public GameObject explosionRoja;
    public GameObject explosionAzul;
    public GameObject explosionVerde;
    public GameObject explosionNaranja;

    void Start()
    {
        database = databaseObject.GetComponent<Database>();

        InitializePlayer();
        InitializeEnemy();
        InitializeInterface();
        InitializeMusic();
        InitializeVFX();
    }

    void InitializePlayer()
    {
        jugadorObject = GameObject.Find("Player");
        jugador = jugadorObject.GetComponent<Player>();
        jugador.InitializePlayer();
    }

    void InitializeEnemy()
    {
        string enemyName = database.LoadEnemyName();
        if (enemyName.Equals("Champimudo"))
        {
            champimudoObject.transform.position = new Vector3(51.5f, 2.62f, 56.53f);
            enemigo = champimudoObject.GetComponent<Enemy>();
        }
        else if (enemyName.Equals("ChampimudoFurioso"))
        {
            champimudoFuriosoObject.transform.position = new Vector3(51.5f, 2.62f, 56.53f);
            enemigo = champimudoFuriosoObject.GetComponent<Enemy>();
        }
        else if (enemyName.Equals("Florhada"))
        {
            florhadaObject.transform.position = new Vector3(51.5f, 2.62f, 56.53f);
            enemigo = florhadaObject.GetComponent<Enemy>();
        }
        else if (enemyName.Equals("Trifauces"))
        {
            trifaucesObject.transform.position = new Vector3(51.5f, 2.62f, 56.53f);
            enemigo = trifaucesObject.GetComponent<Enemy>();
        }
        else if(enemyName.Equals("Scarab"))
        {
            scarabObject.transform.position = new Vector3(51.5f, 2.62f, 56.53f);
            enemigo = scarabObject.GetComponent<Enemy>();
        }
        enemigo.InitializeEnemy(enemyName);
    }

    void InitializeInterface()
    {
        hpBar = hpBarObject.GetComponent<HPBar>();
        hpBar.UpdateHPBar(jugador);

        xpBar = xpBarObject.GetComponent<XPBar>();
        xpBar.UpdateXPBar(jugador);

        hpEnemyBar = hpEnemyBarObject.GetComponent<HPBar>();
        hpEnemyBar.UpdateHPBar(enemigo);

        nivelText.GetComponent<Text>().text = jugador.GetLevel().ToString();
    }

    void InitializeMusic()
    {
        SonidoAzul = GameObject.Find("SonidoAzul");
        SonidoRojo = GameObject.Find("SonidoRoja");
        SonidoNaranja = GameObject.Find("SonidoNaranja");
        SonidoVerde = GameObject.Find("SonidoVerde");
    }

    void InitializeVFX()
    {
        explosionAzul = GameObject.Find("ExplosionAzul");
        explosionRoja = GameObject.Find("ExplosionRoja");
        explosionVerde = GameObject.Find("ExplosionVerde");
        explosionNaranja = GameObject.Find("ExplosionNaranja");
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (mensajeVictoria.active || mensajeDerrota.active)
            {
                ReturnToMap("Malarcier");
            }
        }
        if (mensajeVictoria.active || mensajeDerrota.active)
        {
            HabilitarCursor();
        }
        else
        {
            DeshabilitarCursor();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && activeAzul)
        {
            Destroy(notaAzul);
            explosionAzul.GetComponent<ParticleSystem>().Play();
            //SonidoAzul.GetComponent<AudioSource>().Play();
            NotaAcertada();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && activeVerde)
        {
            Destroy(notaVerde);
            explosionVerde.GetComponent<ParticleSystem>().Play();
            //SonidoVerde.GetComponent<AudioSource>().Play();
            NotaAcertada();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && activeNaranja)
        {
            Destroy(notaNaranja);
            explosionNaranja.GetComponent<ParticleSystem>().Play();
            //SonidoNaranja.GetComponent<AudioSource>().Play();
            NotaAcertada();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && activeRojo)
        {
            Destroy(notaRoja);
            explosionRoja.GetComponent<ParticleSystem>().Play();
            //SonidoRojo.GetComponent<AudioSource>().Play();
            NotaAcertada();
        }
    }

    void HabilitarCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void DeshabilitarCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void NotaAcertada()
    {
        enemigo.TakeDamage(jugador.GetAttack());
        hpEnemyBar.UpdateHPBar(enemigo);
        if (enemigo.GetHP() <= 0)
        {
            Win();
        }
    }

    void Win()
    {
        xpBar.ProgressiveUpdateXPBar(enemigo.GetXPGiven(), jugador.GetLevel());
        jugador.GainXP(enemigo.GetXPGiven());
        database.SavePlayerData(jugador);
        experienciaGanada.text = enemigo.GetXPGiven().ToString();
        enemigo.DieAnimation();
        database.SaveIsWin(true);
        mensajeVictoria.SetActive(true);
        database.SaveStopCircles("true");
        SaveEnemyDefeated(database.LoadLastEnemy());
        DestroyEnemy();
    }

    public void NotaFallada()
    {
        jugador.TakeDamage(enemigo.GetAttack());
        hpBar.UpdateHPBar(jugador);
        if (jugador.GetHP() <= 0)
        {
            Defeat();
        }
    }

    void Defeat()
    {
        database.SaveIsWin(false);
        mensajeDerrota.SetActive(true);
        database.SaveStopCircles("true");
        Destroy(jugadorObject);
    }

    void DestroyEnemy()
    {
        if (database.LoadEnemyName().Equals("Champimudo"))
        {
            Destroy(champimudoObject, 1.4f);
        }
        else if (database.LoadEnemyName().Equals("ChampimudoFurioso"))
        {
            Destroy(champimudoFuriosoObject, 1.4f);
        }
        else if (database.LoadEnemyName().Equals("Florhada"))
        {
            Destroy(florhadaObject, 1f);
        }
        else if (database.LoadEnemyName().Equals("Trifauces"))
        {
            Destroy(trifaucesObject, 1f);
        }
        else if (database.LoadEnemyName().Equals("Scarab"))
        {
            Destroy(scarabObject, 1f);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "NotaAzul")
        {
            notaAzul = col.gameObject;
            activeAzul = true;
        }
        if (col.gameObject.tag == "Nota Verde")
        {
            notaVerde = col.gameObject;
            activeVerde = true;
        }
        if (col.gameObject.tag == "NotaNaranja")
        {
            notaNaranja = col.gameObject;
            activeNaranja = true;
        }
        if (col.gameObject.tag == "NotaRoja")
        {
            notaRoja = col.gameObject;
            activeRojo = true;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        activeRojo = false;
        activeAzul = false;
        activeVerde = false;
        activeNaranja = false;
    }

    public void ReturnToMap(string sceneName)
    {
        Debug.Log("Primero");
        database.SaveDesdeCombate(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    void SaveEnemyDefeated(string name)
    {
        switch (name)
        {
            case "ch1":
                database.SaveCh1("false");
                break;
            case "ch2":
                database.SaveCh2("false");
                break;
            case "ch3":
                database.SaveCh3("false");
                break;
            case "ch4":
                database.SaveCh4("false");
                break;
            case "ch5":
                database.SaveCh5("false");
                break;
            case "chf1":
                database.SaveChF1("false");
                break;
            case "chf2":
                database.SaveChF2("false");
                break;
            case "chf3":
                database.SaveChF3("false");
                break;
            case "chf4":
                database.SaveChF4("false");
                break;
            case "chf5":
                database.SaveChF5("false");
                break;
            case "fh1":
                database.SaveFh1("false");
                break;
            case "fh2":
                database.SaveFh2("false");
                break;
            case "fh3":
                database.SaveFh3("false");
                break;
            case "fh4":
                database.SaveFh4("false");
                break;
            case "fh5":
                database.SaveFh5("false");
                break;
            case "tr1":
                database.SaveTr1("false");
                break;
            case "sc1":
                database.SaveSc1("false");
                break;
            default:
                database.SaveSc1("false");
                break;
        }
    }
}
