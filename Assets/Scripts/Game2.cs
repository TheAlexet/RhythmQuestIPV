using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Game2 : MonoBehaviour
{

	public GameObject playerObject;
	Player jugador;

	public GameObject hpBarObject;
	HPBar hpBar;

	public GameObject xpBarObject;
	XPBar xpBar;

	public GameObject playerMenuObject;
	PlayerMenu playerMenu;

	public GameObject inventoryMenuObject;
	InventoryMenu inventoryMenu;

	public GameObject objectInfoObject;
	public GameObject controlesObject;

	public Text nivelText;

	public GameObject mensajePickUp;
	public GameObject mensajeInventarioLleno;
	public GameObject mensajeHablar;

	public GameObject mensajeConversacion;
	public Text textoConversacion;
	int mensajeActualConversacion;

	public GameObject mensajeMision;
	public Text tituloMision;
	public Text textoMision;

	public GameObject infoMision;
	public Text tituloInfoMision;
	public Text textoInfoMision;

	public GameObject confirmacionSalida;

	public GameObject objetoColisionado;
	public GameObject npcColisionado;

	bool mensajePickUpOpen = false;
	bool mensajeInventarioLlenoOpen = false;
	bool mensajeHablarOpen = false;
	bool mensajeMisionOpen = false;
	bool infoMisionOpen = false;
	bool playerMenuOpen = false;
	bool inventoryOpen = false;
	bool confirmacionSalidaOpen = false;
	bool objectInfoOpen = false;
	bool conversacionOpen = false;
	bool controlesOpen = false;

	Vector3 posicion;

	public GameObject databaseObject;
	Database database;

	public GameObject hole;

	public GameObject musicaDesiertoEspejismo;

	public GameObject camara;
	CinemachineFreeLook vcam;

	void Start()
	{
		database = databaseObject.GetComponent<Database>();

		InitializePlayer();
		CheckState();
		InitializeInterface();
		DestroyPickUps();
		objetoColisionado = null;
		vcam = camara.GetComponent<CinemachineFreeLook>();
		database.SaveMapLevel("Desierto Espejismo");
	}

	void CheckState()
	{
		if (database.LoadFirstTime2())
		{
			GetComponent<CharacterController>().enabled = false;
			GameObject.Find("Player").transform.position = new Vector3(7.16f, 0.965f, 133.52f);
			GetComponent<CharacterController>().enabled = true;
			database.SaveFirstTime2(false);
			database.SaveMalarcierADesierto(false);
			InitializeEnemies();
			DestroyEnemy();
			musicaDesiertoEspejismo.GetComponent<AudioSource>().Stop();
		}
		else if (database.LoadMalarcierADesierto())
		{
			ResetEnemies();
			DestroyEnemy();
			database.SaveMalarcierADesierto(false);
			GetComponent<CharacterController>().enabled = false;
			GameObject.Find("Player").transform.position = new Vector3(7.16f, 0.965f, 133.52f);
			GameObject.Find("Player").transform.eulerAngles = new Vector3(16.26f, 0.37f, 17f);
			GetComponent<CharacterController>().enabled = true;
		}
		else if (database.LoadDesdeCombate())
		{
			database.SaveDesdeCombate(false);
			bool isWin = database.LoadIsWin();
			if (isWin)
			{
				DestroyEnemy();
				GetComponent<CharacterController>().enabled = false;
				GameObject.Find("Player").transform.position = database.LoadPlayerPosition();
				GameObject.Find("Player").transform.rotation = database.LoadPlayerRotation();
				GetComponent<CharacterController>().enabled = true;
			}
			else
			{
				ResetEnemies();
				DestroyEnemy();
				jugador.SetHP(jugador.GetMaxHP());
				GetComponent<CharacterController>().enabled = false;
				GameObject.Find("Player").transform.position = new Vector3(7.16f, 0.965f, 133.52f);
				GetComponent<CharacterController>().enabled = true;
			}
		}
		else
		{
			DestroyEnemy();
			GetComponent<CharacterController>().enabled = false;
			GameObject.Find("Player").transform.position = database.LoadPlayerPosition();
			GameObject.Find("Player").transform.rotation = database.LoadPlayerRotation();
			GetComponent<CharacterController>().enabled = true;
		}
	}


	void InitializePlayer()
	{
		jugador = playerObject.GetComponent<Player>();
		jugador.InitializePlayer();

	}

	void InitializeInterface()
	{
		playerMenu = playerMenuObject.GetComponent<PlayerMenu>();
		playerMenu.InitializePlayerMenu(jugador);

		inventoryMenu = inventoryMenuObject.GetComponent<InventoryMenu>();
		inventoryMenu.InitializeInventoryMenu(jugador);

		hpBar = hpBarObject.GetComponent<HPBar>();
		hpBar.UpdateHPBar(jugador);

		xpBar = xpBarObject.GetComponent<XPBar>();
		xpBar.UpdateXPBar(jugador);

		nivelText.GetComponent<Text>().text = jugador.GetLevel().ToString();

		CloseMensajePickUp();
		CloseMensajeInventarioLleno();
		CloseMensajeHablar();
		CloseMensajeMision();
		CloseInfoMision();
		UpdateMision(0);
		CloseConfirmacionSalida();
	}


	void Update()
	{
		if (mensajeConversacion.active)
		{
			conversacionOpen = true;
		}
		else
		{
			conversacionOpen = false;
		}
		if (Input.GetKeyDown("c"))
		{
			PlayerMenu();
		}

		if (Input.GetKeyDown("i"))
		{
			Inventory();
		}
		if (Input.GetKeyDown("n"))
		{
			Mision();
		}
		if (Input.GetKeyDown("v"))
		{
			Controles();
		}
		if (npcColisionado == null)
		{
			CloseMensajeMision();
		}
		if (Input.GetKeyDown("f"))
		{
			if (objetoColisionado != null)
			{
				PickUpItem();
			}
			if (npcColisionado != null && !conversacionOpen && !mensajeMisionOpen)
			{
				//Do something
			}
			if (confirmacionSalidaOpen)
			{
				CloseGame();
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (playerMenuOpen || inventoryOpen || infoMisionOpen || confirmacionSalidaOpen || mensajeMisionOpen || mensajeInventarioLlenoOpen || objectInfoOpen || conversacionOpen || controlesOpen)
			{
				playerMenu.ClosePlayerMenu();
				playerMenuOpen = false;
				inventoryMenu.CloseInventory();
				inventoryOpen = false;
				CloseInfoMision();
				CloseMensajeMision();
				CloseConfirmacionSalida();
				CloseMensajeInventarioLleno();
				CloseObjectInfo();
				CloseConversacion();
				CloseControles();
			}
			else
			{
				OpenConfirmacionSalida();
			}
		}
		if (playerMenuOpen || inventoryOpen || infoMisionOpen || confirmacionSalidaOpen || mensajeMisionOpen || mensajeInventarioLlenoOpen || objectInfoOpen || conversacionOpen || controlesOpen)
		{
			HabilitarCursor();
			vcam.m_XAxis.m_InputAxisName = "";
			vcam.m_XAxis.m_MaxSpeed = 0;
		}
		else
		{
			DeshabilitarCursor();
			vcam.m_XAxis.m_InputAxisName = "Mouse X";
			vcam.m_XAxis.m_MaxSpeed = 300;
		}
	}

	void HabilitarCursor()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	void DeshabilitarCursor()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void PlayerMenu()
	{
		inventoryMenu.CloseInventory();
		inventoryOpen = false;
		CloseInfoMision();
		CloseConfirmacionSalida();
		CloseControles();
		if (!playerMenu.IsOpen())
		{
			playerMenuOpen = true;
			playerMenu.OpenPlayerMenu();
		}
		else
		{
			playerMenuOpen = false;
			playerMenu.ClosePlayerMenu();
		}
	}

	public void Inventory()
	{
		playerMenu.ClosePlayerMenu();
		playerMenuOpen = false;
		CloseInfoMision();
		CloseConfirmacionSalida();
		CloseControles();
		if (!inventoryMenu.IsOpen())
		{
			inventoryOpen = true;
			inventoryMenu.OpenInventory();
		}
		else
		{
			inventoryOpen = false;
			inventoryMenu.CloseInventory();
		}
	}

	public void Mision()
	{
		playerMenu.ClosePlayerMenu();
		playerMenuOpen = false;
		inventoryMenu.CloseInventory();
		inventoryOpen = false;
		CloseConfirmacionSalida();
		CloseControles();
		if (!infoMisionOpen)
		{
			OpenInfoMision();
		}
		else
		{
			CloseInfoMision();
		}
	}

	public void Salida()
	{
		playerMenu.ClosePlayerMenu();
		playerMenuOpen = false;
		inventoryMenu.CloseInventory();
		inventoryOpen = false;
		CloseInfoMision();
		CloseControles();
		if (!confirmacionSalidaOpen)
		{
			OpenConfirmacionSalida();
		}
		else
		{
			CloseConfirmacionSalida();
		}
	}

	public void Controles()
	{
		playerMenu.ClosePlayerMenu();
		playerMenuOpen = false;
		inventoryMenu.CloseInventory();
		inventoryOpen = false;
		CloseInfoMision();
		CloseConfirmacionSalida();
		if (!controlesOpen)
		{
			OpenControles();
		}
		else
		{
			CloseControles();
		}
	}

	void PickUpItem()
	{
		if (jugador.GetListaObjetos().Count < 15)
		{
			jugador.AddItem(objetoColisionado.GetComponent<PickUp>());
			inventoryMenu.UpdateInventory(jugador);
			SavePickedUpItems();
			Destroy(objetoColisionado);
			CloseMensajePickUp();
			database.SaveObjetosRecogidos(database.LoadObjetosRecogidos() + 1);
		}
		else
		{
			CloseMensajePickUp();
			OpenMensajeInventarioLleno();
		}
	}

	public void RemoveItem(PickUp itemRemove, bool used)
	{
		jugador.RemoveItem(itemRemove);
		if (used)
		{
			UseItem(itemRemove.name);
		}
	}


	void UseItem(string item)
	{
		if (item.Equals("PocionVida"))
		{
			jugador.Heal(20);
			hpBar.UpdateHPBar(jugador);
			playerMenu.UpdateStats(jugador);
			database.SavePlayerData(jugador);
		}
		if (item.Equals("PocionXP"))
		{
			jugador.GainXP(50);
			playerMenu.UpdateStats(jugador);
			database.SavePlayerData(jugador);
			nivelText.GetComponent<Text>().text = jugador.GetLevel().ToString();
			hpBar.UpdateHPBar(jugador);
		}
	}

	void OpenMensajePickUp()
	{
		mensajePickUpOpen = true;
		mensajePickUp.SetActive(true);
	}

	void CloseMensajePickUp()
	{
		mensajePickUpOpen = false;
		mensajePickUp.SetActive(false);
	}

	void OpenMensajeInventarioLleno()
	{
		mensajeInventarioLlenoOpen = true;
		mensajeInventarioLleno.SetActive(true);
	}

	public void CloseMensajeInventarioLleno()
	{
		mensajeInventarioLlenoOpen = false;
		mensajeInventarioLleno.SetActive(false);
	}

	void OpenMensajeHablar()
	{
		mensajeHablarOpen = true;
		mensajeHablar.SetActive(true);
	}

	void CloseMensajeHablar()
	{
		mensajeHablarOpen = false;
		mensajeHablar.SetActive(false);
	}

	void OpenMensajeMision()
	{
		mensajeMisionOpen = true;
		mensajeMision.SetActive(true);
	}

	void CloseMensajeMision()
	{
		mensajeMisionOpen = false;
		mensajeMision.SetActive(false);
	}

	void OpenInfoMision()
	{
		infoMisionOpen = true;
		infoMision.SetActive(true);
		if (!database.LoadMisionCompletada())
		{
			UpdateMision(database.LoadSiguienteConversacion());
		}
	}

	public void CloseInfoMision()
	{
		infoMisionOpen = false;
		infoMision.SetActive(false);
	}

	public void OpenObjectInfo()
	{
		objectInfoOpen = true;
		objectInfoObject.SetActive(true);
	}

	public void CloseObjectInfo()
	{
		objectInfoOpen = false;
		objectInfoObject.SetActive(false);
	}

	public void OpenConversacion()
	{
		conversacionOpen = true;
		mensajeConversacion.SetActive(true);
	}

	public void CloseConversacion()
	{
		conversacionOpen = false;
		mensajeConversacion.SetActive(false);
	}

	public void OpenControles()
	{
		controlesOpen = true;
		controlesObject.SetActive(true);
	}

	public void CloseControles()
	{
		controlesOpen = false;
		controlesObject.SetActive(false);
	}

	public void OpenConfirmacionSalida()
	{
		confirmacionSalidaOpen = true;
		confirmacionSalida.SetActive(true);
	}

	public void CloseConfirmacionSalida()
	{
		confirmacionSalidaOpen = false;
		confirmacionSalida.SetActive(false);
	}

	public void CloseGame()
	{
		Application.Quit();
	}

	void UpdateMision(int mision)
	{
		switch (mision)
		{
			default:
				tituloInfoMision.text = "Vacio";
				textoInfoMision.text = "No hay ninguna mision disponible.";
				break;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "ItemPickUp")
		{
			objetoColisionado = col.gameObject;
			OpenMensajePickUp();
		}
		else if (col.tag == "Npc")
		{
			npcColisionado = col.gameObject;
			OpenMensajeHablar();
		}

	}

	void OnTriggerExit(Collider col)
	{
		objetoColisionado = null;
		npcColisionado = null;
		CloseMensajePickUp();
		CloseMensajeHablar();
		mensajeConversacion.SetActive(false);
	}

	void DestroyPickUps()
	{
		if (database.LoadHP21().Equals("false"))
		{
			Destroy(GameObject.Find("HealthPotion"));
		}
		if (database.LoadHP22().Equals("false"))
		{
			Destroy(GameObject.Find("HealthPotion2"));
		}
		if (database.LoadHP23().Equals("false"))
		{
			Destroy(GameObject.Find("HealthPotion3"));
		}
		if (database.LoadXP21().Equals("false"))
		{
			Destroy(GameObject.Find("XPPotion"));
		}
		if (database.LoadXP22().Equals("false"))
		{
			Destroy(GameObject.Find("XPPotion2"));
		}
	}

	void SavePickedUpItems()
	{
		if (objetoColisionado.name.Equals("HealthPotion"))
		{
			database.SaveHP21("false");
		}
		if (objetoColisionado.name.Equals("HealthPotion2"))
		{
			database.SaveHP22("false");
		}
		if (objetoColisionado.name.Equals("HealthPotion3"))
		{
			database.SaveHP23("false");
		}
		if (objetoColisionado.name.Equals("XPPotion"))
		{
			database.SaveXP21("false");
		}
		if (objetoColisionado.name.Equals("XPPotion2"))
		{
			database.SaveXP22("false");
		}
	}

	void DestroyEnemy()
	{
		if (database.LoadAl1().Equals("false"))
		{
			GameObject.Find("Alaguijon1").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Alaguijon1"));
		}
		if (database.LoadAl2().Equals("false"))
		{
			GameObject.Find("Alaguijon2").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Alaguijon2"));
		}
		if (database.LoadAl3().Equals("false"))
		{
			GameObject.Find("Alaguijon3").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Alaguijon3"));
		}
		if (database.LoadAl4().Equals("false"))
		{
			GameObject.Find("Alaguijon4").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Alaguijon4"));
		}
		if (database.LoadAl5().Equals("false"))
		{
			GameObject.Find("Alaguijon5").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Alaguijon5"));
		}
		if (database.LoadAl6().Equals("false"))
		{
			GameObject.Find("Alaguijon6").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Alaguijon6"));
		}
		if (database.LoadAl7().Equals("false"))
		{
			GameObject.Find("Alaguijon7").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Alaguijon7"));
		}
		if (database.LoadAl8().Equals("false"))
		{
			GameObject.Find("Alaguijon8").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Alaguijon8"));
		}
		if (database.LoadAb1().Equals("false"))
		{
			GameObject.Find("Absorbedora1").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Absorbedora1"));
		}
		if (database.LoadAb2().Equals("false"))
		{
			GameObject.Find("Absorbedora2").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Absorbedora2"));
		}
		if (database.LoadAb3().Equals("false"))
		{
			GameObject.Find("Absorbedora3").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Absorbedora3"));
		}
		if (database.LoadAb4().Equals("false"))
		{
			GameObject.Find("Absorbedora4").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Absorbedora4"));
		}
		if (database.LoadCa1().Equals("false"))
		{
			GameObject.Find("CabezaCarnivora").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("CabezaCarnivora"));
			musicaDesiertoEspejismo.GetComponent<AudioSource>().Play();
			hole.SetActive(true);
		}
	}

	void InitializeEnemies()
	{
		database.SaveAl1("true");
		database.SaveAl2("true");
		database.SaveAl3("true");
		database.SaveAl4("true");
		database.SaveAl5("true");
		database.SaveAl6("true");
		database.SaveAl7("true");
		database.SaveAl8("true");
		database.SaveAb1("true");
		database.SaveAb2("true");
		database.SaveAb3("true");
		database.SaveAb4("true");
		database.SaveCa1("true");
	}

	void ResetEnemies()
	{
		database.SaveAl1("true");
		database.SaveAl2("true");
		database.SaveAl3("true");
		database.SaveAl4("true");
		database.SaveAl5("true");
		database.SaveAl6("true");
		database.SaveAl7("true");
		database.SaveAl8("true");
		database.SaveAb1("true");
		database.SaveAb2("true");
		database.SaveAb3("true");
		database.SaveAb4("true");
	}

	void OnApplicationQuit()
	{
		database.SavePlayerPosition(playerObject);
		database.SavePlayerRotation(playerObject);
	}
}
