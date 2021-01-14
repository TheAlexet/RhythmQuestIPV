using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    int RandomSpawner;

    GameObject SpawnAzul;
    GameObject SpawnVerde;
    GameObject SpawnNaranja;
    GameObject SpawnRojo;

    SpawnNotaAzul Azul;
    SpawnNotaVerde Verde;
    SpawnNotaNaranja Naranja;
    SpawnNotaRoja Roja;

    public GameObject databaseObject;
    Database database;

    public float[] intervaloNotas = new float[] { 0.5f, 0.7f, 1f };
    private float random;

    bool check;

    void Start()
    {
        database = databaseObject.GetComponent<Database>();
        database.SaveStopCircles("false");

        SpawnAzul = GameObject.Find("SpawnerAzul");
        SpawnVerde = GameObject.Find("SpawnerVerde");
        SpawnNaranja = GameObject.Find("SpawnerNaranja");
        SpawnRojo = GameObject.Find("SpawnerRoja");

        Azul = SpawnAzul.GetComponent<SpawnNotaAzul>();
        Verde = SpawnVerde.GetComponent<SpawnNotaVerde>();
        Naranja = SpawnNaranja.GetComponent<SpawnNotaNaranja>();
        Roja = SpawnRojo.GetComponent<SpawnNotaRoja>();

        check = true;
        InvokeRepeating("NotasManager", 0.5f, 1f);
    }

    void NotasManager()
    {
        if (database.LoadStopCircles().Equals("false"))
        {
            RandomSpawner = Random.Range(1, 7);

            if (RandomSpawner == 1)
            {
                PatternNota1();
            }
            else if (RandomSpawner == 2)
            {
                PatternNota2();
            }
            else if (RandomSpawner == 3)
            {
                PatternNota3();
            }
           else if (RandomSpawner == 4)
            {
                PatternNota4();
            }
            else if (RandomSpawner == 5)
            {
                PatternNota5();
            }
            else if (RandomSpawner == 6)
            {
                PatternNota6();
            }
        }
    }

    /* void NotasManager()
     {
         InvokeRepeating("RandomizeSpawnNotas", 0.5f, 2f);
     }*/

    /* void RandomizeSpawnNotas()
     {
         random = intervaloNotas[Random.Range(0, intervaloNotas.Length)];

         //InvokeRepeating("SpawnNotas", 0.5f, 0.5f);
         InvokeRepeating("SpawnNotas", 0.5f, random);
     }*/

    void PatternNota1()
    {
        Invoke("SpawnNotas", 0f);
        Invoke("SpawnNotas", 0.5f);
    }

    void PatternNota2()
    {
        Invoke("SpawnNotas", 0f);
        Invoke("SpawnNotas", 0.2f);
    }

    void PatternNota3()
    {
        Invoke("SpawnNotas", 0f);
        Invoke("SpawnNotas", 0.2f);
        Invoke("SpawnNotas", 0.4f);
    }

    void PatternNota4()
    {
        Invoke("SpawnNotas", 0f);
        Invoke("SpawnNotas", 0.3f);
        Invoke("SpawnNotas", 0.5f);

    }

    void PatternNota5()
    {
        Invoke("SpawnNotas", 0f);
        Invoke("SpawnNotas", 0.2f);
        Invoke("SpawnNotas", 0.5f);
    }
    void PatternNota6()
    {
        Invoke("SpawnNotas", 0f);
        Invoke("SpawnNotas", 0.3f);
        Invoke("SpawnNotas", 0.6f);
    }

    void SpawnNotas()
    {
        if (database.LoadStopCircles().Equals("false"))
        {
            RandomSpawner = Random.Range(1, 5);

            if (RandomSpawner == 1)
            {
                Azul.Spawn();
            }
            else if (RandomSpawner == 2)
            {
                Verde.Spawn();
            }
            else if (RandomSpawner == 3)
            {
                Naranja.Spawn();
            }
            else if (RandomSpawner == 4)
            {
                Roja.Spawn();
            }
        }

    }
}
