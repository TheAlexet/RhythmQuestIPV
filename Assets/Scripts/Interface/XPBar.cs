using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Slider slider;

	int xpRestante;
	int nivel;

	public Text nivelText;

	//Update max XP bar
	public void SetMaxXP(int newMaxXP)
	{
		slider.maxValue = newMaxXP;
		slider.value = newMaxXP;
	}

	//Update current XP bar
	public void SetXP(int newXP) 
	{
		slider.value = newXP;
	}

	public void UpdateXPBar(Player jugador)
	{
		SetMaxXP(jugador.GetMaxXP());
		SetXP(jugador.GetXP());
	}

	public void ProgressiveUpdateXPBar(int totalXP, int nivelActual)
	{
		xpRestante = totalXP;
		nivel = nivelActual;
		InvokeRepeating("UpdateXP", 0.05f, 0.05f);
	}

	void UpdateXP()
	{
		if (xpRestante > 0)
		{
			if (slider.value + 1 == slider.maxValue)
			{
				if (nivel == 100)
				{
					slider.value += 1;
				}
				else
				{
					slider.maxValue += 100;
					slider.value = 0;
					nivel += 1;
					nivelText.GetComponent<Text>().text = nivel.ToString();
				}
			}
			else if(nivel != 100)
			{
				slider.value += 1;
			}
			xpRestante -= 1;
		}
	}
}
