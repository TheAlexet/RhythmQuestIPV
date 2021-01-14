using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

	public string name;

	int attack;

	int maxHP;
	int currentHP;

	int xpGiven;

	public Animator animator;

	public void InitializeEnemy(string newName)
	{
		name = newName;
		animator = GetComponent<Animator>();
		CalculateStats();
	}

	public void CalculateStats()
	{
		if (name.Equals("Champimudo"))
		{
			attack = 1;
			maxHP = 30;
			currentHP = 30;
			xpGiven = 20;
		}
		else if (name.Equals("ChampimudoFurioso"))
		{
			attack = 3;
			maxHP = 50;
			currentHP = 50;
			xpGiven = 50;
		}
		else if (name.Equals("Florhada"))
		{
			attack = 3;
			maxHP = 20;
			currentHP = 20;
			xpGiven = 30;
		}
		else if (name.Equals("Trifauces"))
		{
			attack = 5;
			maxHP = 250;
			currentHP = 250;
			xpGiven = 200;
		}
		else if (name.Equals("Scarab"))
		{
			attack = 10;
			maxHP = 500;
			currentHP = 500;
			xpGiven = 500;
		}
		else if (name.Equals("Alaguijon"))
		{
			attack = 8;
			maxHP = 100;
			currentHP = 100;
			xpGiven = 100;
		}
		else if (name.Equals("Absorbedora"))
		{
			attack = 6;
			maxHP = 180;
			currentHP = 180;
			xpGiven = 120;
		}
		else if (name.Equals("CabezaCarnivora"))
		{
			attack = 20;
			maxHP = 1000;
			currentHP = 1000;
			xpGiven = 1000;
		}
	}

	//Returns enemy name
	public string GetName()
	{
		return name;
	}

	//Update enemy name
	public void SetName(string newName)
	{
		name = newName;
	}

	//Returns enemy attack
	public int GetAttack()
	{
		return attack;
	}

	//Update enemy atack
	public void SetAttack(int newAtack)
	{
		attack = newAtack;
	}

	//Returns enemy max HP
	public int GetMaxHP()
	{
		return maxHP;
	}

	//Update enemy max HP stat and update HP bar
	public void SetMaxHP(int newMaxHP)
	{
		maxHP = newMaxHP;
	}

	//Returns enemy current HP
	public int GetHP()
	{
		return currentHP;
	}

	//Update enemy current HP stat and update HP bar
	public void SetHP(int newHP)
	{
		currentHP = newHP;
	}

	//Returns enemy XP given when defeated
	public int GetXPGiven()
	{
		return xpGiven;
	}

	//Update enemy XP given when defeated
	public void SetXPGiven(int newXP)
	{
		xpGiven = newXP;
	}

	public void TakeDamage(int damage)
	{
		SetHP(currentHP - damage);
	}

	public void DieAnimation()
	{
		animator = GetComponent<Animator>();
		animator.SetTrigger("Die");
	}
}
