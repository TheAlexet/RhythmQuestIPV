using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision2 : MonoBehaviour
{
    GameObject jugadorObject;
    Player jugador;

    public GameObject enemigoObject;
    public string enemyName;
    Enemy enemigo;
    Vector3 posicionPlayer;
    Combate combate;

    public GameObject databaseObject;
    Database database;

    void OnTriggerEnter(Collider col)
    {
        enemigo = GetComponent<Enemy>();
        if (col.tag == "Player")
        {
            database = databaseObject.GetComponent<Database>();
            jugadorObject = col.gameObject;
            jugador = jugadorObject.GetComponent<Player>();

            database.SavePlayerData(jugador);
            database.SavePlayerPosition(jugadorObject);
            database.SavePlayerRotation(jugadorObject);

            enemigo = enemigoObject.GetComponent<Enemy>();
            database.SaveLastEnemy(enemyName);
            database.SaveEnemyName(enemigo);
            SceneManager.LoadScene("Combate2");
        }

    }

}