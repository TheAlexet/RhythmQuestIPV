using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AMalarcier : MonoBehaviour
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
            database.SaveDesiertoAMalarcier(true);
            SceneManager.LoadScene("Malarcier");
        }

    }
}
