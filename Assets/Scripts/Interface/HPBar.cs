using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
	
	public Slider slider;
	public Gradient gradient;
	public Image fill;
	
	//Update max HP bar
	public void SetMaxHP(int newMaxHP)
	{
		slider.maxValue = newMaxHP;
		slider.value = newMaxHP;
		
		fill.color = gradient.Evaluate(1f);
	}
	
	//Update current HP bar
    public void SetHP(int newHP) 
	{
		slider.value = newHP;
		
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	public void UpdateHPBar(Player jugador)
	{
		SetMaxHP(jugador.GetMaxHP());
		SetHP(jugador.GetHP());
	}

	public void UpdateHPBar(Enemy enemigo)
	{
		SetMaxHP(enemigo.GetMaxHP());
		SetHP(enemigo.GetHP());
	}
}
