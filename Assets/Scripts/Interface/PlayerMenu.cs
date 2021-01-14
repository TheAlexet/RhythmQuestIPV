using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    bool playerMenuOpen;

    public GameObject playerMenu;

    public Text playerMenuName;
    public Text playerMenuLevel;
    public Text playerMenuXP;
    public Text playerMenuHP;
    public Text playerMenuAttack;

    public void InitializePlayerMenu(Player jugador)
    {
        playerMenuOpen = false;
        if (jugador != null)
        {
            UpdateStats(jugador);
        }
    }

    public bool IsOpen()
    {
        return playerMenuOpen;
    }

    public void OpenPlayerMenu()
    {
        playerMenuOpen = true;
        playerMenu.SetActive(true);
    }

    public void ClosePlayerMenu()
    {
        playerMenuOpen = false;
        playerMenu.SetActive(false);
    }

    public void UpdateStats(Player jugador)
    {
        if (jugador != null)
        {
            playerMenuName.GetComponent<Text>().text = jugador.GetName();
            playerMenuLevel.GetComponent<Text>().text = "NIVEL: " + jugador.GetLevel().ToString();
            playerMenuXP.GetComponent<Text>().text = "XP: " + jugador.GetXP().ToString() + "/" + jugador.GetMaxXP().ToString();
            playerMenuHP.GetComponent<Text>().text = "HP: " + jugador.GetHP().ToString() + "/" + jugador.GetMaxHP().ToString();
            playerMenuAttack.GetComponent<Text>().text = "ATAQUE: " + jugador.GetAttack().ToString();
        }
    }
}
