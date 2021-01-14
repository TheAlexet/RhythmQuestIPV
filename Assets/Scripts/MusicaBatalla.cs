using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaBatalla : MonoBehaviour
{
    AudioSource musicaNormal;
    AudioSource musicaScarab;
    Database database;

    void Start()
    {
        musicaNormal = GameObject.Find("MusicNormal").GetComponent<AudioSource>();
        musicaScarab = GameObject.Find("MusicScarab").GetComponent<AudioSource>();
        database = GameObject.Find("Database").GetComponent<Database>();
        if (database.LoadEnemyName().Equals("Scarab") || database.LoadEnemyName().Equals("CabezaCarnivora"))
        {
            musicaScarab.Play();
        }
        else
        {
            musicaNormal.Play();
        }

    }
}
