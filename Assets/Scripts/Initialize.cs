using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initialize : MonoBehaviour
{
	
	string saveName;
	public Text inputText;
	
	public Slider volumenGeneral;
	public Slider musica;

	string nameOfPlayer;

	public Text loadedName;
	public InputField iField;

	public GameObject databaseObject;
	Database database;

	void Start()
	{
		database = databaseObject.GetComponent<Database>();
		database.SaveIsWin(false);

		volumenGeneral.value = volumenGeneral.maxValue/2;
		musica.value = musica.maxValue/2;
	}

	void Update()
	{
		nameOfPlayer = database.LoadPlayerName();
		if (nameOfPlayer == "none")
		{
			loadedName.text = "JUGADOR: NINGUNO";
		}
		else
		{
			loadedName.text = "JUGADOR: " + nameOfPlayer;
		}
	}


	public void changeScene()
    {
		saveName = database.LoadPlayerName();
		if (saveName != "none")
		{
			database.SaveFirstTime(false);
			database.SaveDesiertoAMalarcier(false);
			database.SaveMalarcierADesierto(false);
			database.SaveDesdeCombate(false);
			if (database.LoadMapLevel().Equals("Malarcier"))
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene("Malarcier");
			}
			else if (database.LoadMapLevel().Equals("Desierto Espejismo"))
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene("Desierto Espejismo");
			}
		}
    }
	
	public void newGame(string sceneName)
    {
		if(inputText.text != "")
		{
			database.SaveFirstTime(true);
			database.SaveFirstTime2(true);
			database.SaveDesiertoAMalarcier(false);
			database.SaveMalarcierADesierto(false);
			database.SaveDesdeCombate(false);
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		}
    }

	public void SetName()
	{
		saveName = iField.text;
		PlayerPrefs.SetString("playerName", saveName);
		PlayerPrefs.SetInt("playerLevel", 1);
		PlayerPrefs.SetInt("playerAttack", 2);
		PlayerPrefs.SetInt("playerHP", 20);
		PlayerPrefs.SetInt("playerMaxHP", 20);
		PlayerPrefs.SetInt("playerXP", 0);
		PlayerPrefs.SetInt("playerMaxXP", 100);
	}

	public void ResetName()
	{
		iField.text = "Bardo";
	}

	public void ResetInventory()
	{
		database.ResetListaObjetos();
	}

	public void ResetPickUps()
	{
		database.SaveHP1("true");
		database.SaveHP2("true");
		database.SaveHP3("true");
		database.SaveXP1("true");
		database.SaveXP2("true");
	}

	public void doquit()
    {
        Application.Quit();
    }
}
