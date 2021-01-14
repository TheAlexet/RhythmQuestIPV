using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    void Start()
    {
        Invoke("Close", 40);
    }

    void Close()
    {
        Debug.Log("Cerrando");
        Application.Quit();
    }
}
