using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlDesierto : MonoBehaviour
{

    public GameObject databaseObject;
    Database database;

    void Start()
    {
        database = databaseObject.GetComponent<Database>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            database.SaveMalarcierADesierto(true);
            SceneManager.LoadScene("Desierto Espejismo");
        }

    }
}
