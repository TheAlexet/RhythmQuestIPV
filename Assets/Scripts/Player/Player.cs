using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    string name;

    int level;

    int attack;

    int maxHP;
    int currentHP;

    int maxXP;
    int currentXP;

    List<PickUp> listaObjetos;

    public GameObject databaseObject;
    Database database;

    public void InitializePlayer()
    {
        database = databaseObject.GetComponent<Database>();

        name = database.LoadPlayerName();
        level = database.LoadPlayerLevel();
        attack = database.LoadPlayerAttack();
        maxHP = database.LoadPlayerMaxHP();
        currentHP = database.LoadPlayerHP();
        maxXP = database.LoadPlayerMaxXP();
        currentXP = database.LoadPlayerXP();
        listaObjetos = new List<PickUp>();
        UpdateListaObjetos();
    }

    //Returns player name
    public string GetName()
    {
        return name;
    }

    //Update player name
    public void SetName(string newName)
    {
        name = newName;
    }

    //Returns player level
    public int GetLevel()
    {
        return level;
    }

    //Update player level
    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    //Returns player attack
    public int GetAttack()
    {
        return attack;
    }

    //Update player atack
    public void SetAttack(int newAtack)
    {
        attack = newAtack;
    }

    //Returns player max HP
    public int GetMaxHP()
    {
        return maxHP;
    }

    //Update player max HP stat and update HP bar
    public void SetMaxHP(int newMaxHP)
    {
        maxHP = newMaxHP;
    }

    //Returns player current HP
    public int GetHP()
    {
        return currentHP;
    }

    //Update player current HP stat and update HP bar
    public void SetHP(int newHP)
    {
        currentHP = newHP;
    }

    //Returns player max XP
    public int GetMaxXP()
    {
        return maxXP;
    }

    //Update player max XP stat and update XP bar
    public void SetMaxXP(int newMaxXP)
    {
        maxXP = newMaxXP;
    }

    //Returns player current XP
    public int GetXP()
    {
        return currentXP;
    }

    //Update player current XP stat and update XP bar
    public void SetXP(int newXP)
    {
        currentXP = newXP;
    }

    public List<PickUp> GetListaObjetos()
    {
        return listaObjetos;
    }

    public void SetListaObjetos(List<PickUp> newList)
    {
        listaObjetos = newList;
    }

    public void TakeDamage(int damage)
    {
        SetHP(currentHP - damage);
    }

    public void Heal(int hp)
    {
        SetHP(currentHP + hp);
		if (currentHP > maxHP)
		{
			currentHP = maxHP;
		}
    }

    public void GainXP(int points)
    {
        if (level < 100)
        {
            SetXP(currentXP + points);
            if (currentXP >= maxXP)
            {
                SetXP(currentXP - maxXP);
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        if (level < 100)
        {
            level += 1;
            attack += 2;
            SetMaxHP(maxHP + 10);
            SetHP(maxHP);
            SetMaxXP(maxXP + 100);
        }
    }

    void UpdateListaObjetos()
    {
        listaObjetos = database.LoadListaObjetos(); 
    }

    public void AddItem(PickUp itemAdd)
    {
        listaObjetos.Add(itemAdd);
        database.SaveListaObjetos(listaObjetos);
    }

    public void RemoveItem(PickUp itemRemove)
    {
        listaObjetos.Remove(itemRemove);
        database.SaveListaObjetos(listaObjetos);
    }

}
