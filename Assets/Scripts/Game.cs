using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Game : MonoBehaviour
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

	public GameObject champimudoF1;
	public GameObject champimudoF2;
	public GameObject champimudoF3;
	public GameObject champimudoF4;
	public GameObject champimudoF5;
	public GameObject triFauces;
	public GameObject scarab;

	public GameObject hole;

	public GameObject musicaMalarcier;

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
		database.SaveMapLevel("Malarcier");
	}

	void CheckState()
	{
		if (database.LoadFirstTime())
		{
			InitializeEnemies();
			DestroyEnemy();
			jugador.SetHP(jugador.GetMaxHP());
			database.SaveSiguienteConversacion(0);
			database.SaveMisionCompletada(true);
			database.SavePlayerData(jugador);
			database.SaveSiguienteConversacion(0);
			database.SaveFirstTime(false);
			database.SaveObjetosRecogidos(0);
			musicaMalarcier.GetComponent<AudioSource>().Stop();
			hole.SetActive(false);
			OpenControles();
		}
		else if (database.LoadDesiertoAMalarcier())
		{
			InitializeEnemies();
			DestroyEnemy();
			database.SaveDesiertoAMalarcier(false);
			GetComponent<CharacterController>().enabled = false;
			GameObject.Find("Player").transform.position = new Vector3(16.26f, 0.37f, 17f);
			GameObject.Find("Player").transform.eulerAngles = new Vector3(GameObject.Find("Player").transform.eulerAngles.x, GameObject.Find("Player").transform.eulerAngles.y + 90, GameObject.Find("Player").transform.eulerAngles.z);
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
				GameObject.Find("Player").transform.position = new Vector3(61.16f, 1.69f, 60.57f);
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
		if(npcColisionado == null)
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
				TalkToNpc();
			}
			if (confirmacionSalidaOpen)
			{
				CloseGame();
			}
			if (mensajeMisionOpen)
			{
				AceptarMisionHandler();
			}
			if (conversacionOpen)
			{
				AceptarConversacionHandler();
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

	void TalkToNpc()
	{
		if (database.LoadObjetosRecogidos() >= 3 && database.LoadSiguienteConversacion() == 1 && !database.LoadMisionCompletada())
		{
			database.SaveMisionCompletada(true);
			database.SaveSiguienteConversacion(database.LoadSiguienteConversacion() + 1);
		}
		else if (database.LoadChF1().Equals("false") && database.LoadChF2().Equals("false") && database.LoadChF3().Equals("false") && database.LoadChF4().Equals("false") && database.LoadChF1().Equals("false") && database.LoadSiguienteConversacion() == 2 && !database.LoadMisionCompletada())
		{
			database.SaveMisionCompletada(true);
			database.SaveSiguienteConversacion(database.LoadSiguienteConversacion() + 1);
		}
		else if (database.LoadTr1().Equals("false") && database.LoadSiguienteConversacion() == 3 && !database.LoadMisionCompletada())
		{
			database.SaveMisionCompletada(true);
			database.SaveSiguienteConversacion(database.LoadSiguienteConversacion() + 1);
		}

		int siguienteConversacion = database.LoadSiguienteConversacion();
		Npc npc = npcColisionado.GetComponent<Npc>();
		mensajeConversacion.SetActive(true);
		if (database.LoadMisionCompletada())
		{
			UpdateMision(0);
			textoConversacion.text = npc.conversacion[siguienteConversacion][0];
			mensajeActualConversacion = 1;
		}
		else 
		{
			textoConversacion.text = npc.conversacion[npc.conversacion.Length - 1][0];
		}
	}

	public void AceptarConversacionHandler()
	{
		if (database.LoadMisionCompletada())
		{
			int siguienteConversacion = database.LoadSiguienteConversacion();
			Npc npc = npcColisionado.GetComponent<Npc>();
			if (mensajeActualConversacion == npc.conversacion[siguienteConversacion].Length)
			{
				switch (siguienteConversacion)
				{
					case 0:
						database.SaveSiguienteConversacion(siguienteConversacion + 1);
						break;
					case 1:
						tituloMision.text = "Prueba 1: Consigue provisiones";
						textoMision.text = "El anciano te ha pedido que encuentres 3 objetos por la zona que te ayuden en tu aventura. Habla con el cuando los consigas.";
						OpenMensajeMision();
						break;
					case 2:
						tituloMision.text = "Prueba 2: Limpieza de Champimudos";
						textoMision.text = "Una version mas peligrosa del Champimudo ha llegado al bosque. Acaba con 5 de estos Champimudos super fuertes. Al finalizar, habla con el anciano.";
						OpenMensajeMision();
						break;
					case 3:
						tituloMision.text = "Prueba 3: La evaluacion final";
						textoMision.text = "Para tu ultima prueba tendras que derrotar a la Trifauces que ha aparecido en la zona. Habla con el anciano despues de derrotarla.";
						OpenMensajeMision();
						break;
					case 4:
						database.SaveSiguienteConversacion(siguienteConversacion + 1);
						database.SaveSc1("true");
						scarab.SetActive(true);
						break;
					default:
						break;
				}
				mensajeConversacion.SetActive(false);
			}
			else
			{
				textoConversacion.text = npc.conversacion[siguienteConversacion][mensajeActualConversacion];
				mensajeActualConversacion++;
			}
		}
		else
		{
			mensajeConversacion.SetActive(false);
		}
	
	}

	public void AceptarMisionHandler()
	{
		int siguienteConversacion = database.LoadSiguienteConversacion();
		switch (siguienteConversacion)
		{
			case 1:
				Mision1();
				break;
			case 2:
				Mision2();
				break;
			case 3:
				Mision3();
				break;
			default:
				break;
		}
		CloseMensajeMision();
		database.SaveMisionCompletada(false);
	}

	public void CancelarMisionHandler()
	{
		CloseMensajeMision();
	}

	void Mision1()
	{
		UpdateMision(1);
		database.SaveMisionCompletada(false);
	}

	void Mision2()
	{
		UpdateMision(2);
		database.SaveMisionCompletada(false);
		database.SaveChF1("true");
		database.SaveChF2("true");
		database.SaveChF3("true");
		database.SaveChF4("true");
		database.SaveChF5("true");
		champimudoF1.SetActive(true);
		champimudoF2.SetActive(true);
		champimudoF3.SetActive(true);
		champimudoF4.SetActive(true);
		champimudoF5.SetActive(true);
	}

	void Mision3()
	{
		UpdateMision(3);
		database.SaveMisionCompletada(false);
		database.SaveTr1("true");
		triFauces.SetActive(true);
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
			case 1:
				tituloInfoMision.text = "Prueba 1: Consigue provisiones";
				textoInfoMision.text = "El anciano te ha pedido que encuentres 3 objetos por la zona que te ayuden en tu aventura. Habla con el cuando los consigas.";
				break;
			case 2:
				tituloInfoMision.text = "Prueba 2: Limpieza de Champimudos";
				textoInfoMision.text = "Una version mas peligrosa del Champimudo ha llegado al bosque. Acaba con 5 de estos Champimudos super fuertes. Al finalizar, habla con el anciano.";
				break;
			case 3:
				tituloInfoMision.text = "Prueba 3: La evaluacion final";
				textoInfoMision.text = "Para tu ultima prueba tendras que derrotar a la Trifauces que ha aparecido en la zona. Habla con el anciano despues de derrotarla.";
				break;
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
		if (database.LoadHP1().Equals("false"))
		{
			Destroy(GameObject.Find("HealthPotion"));
		}
		if (database.LoadHP2().Equals("false"))
		{
			Destroy(GameObject.Find("HealthPotion2"));
		}
		if (database.LoadHP3().Equals("false"))
		{
			Destroy(GameObject.Find("HealthPotion3"));
		}
		if (database.LoadXP1().Equals("false"))
		{
			Destroy(GameObject.Find("XPPotion"));
		}
		if (database.LoadXP2().Equals("false"))
		{
			Destroy(GameObject.Find("XPPotion2"));
		}
	}

	void SavePickedUpItems()
	{
		if (objetoColisionado.name.Equals("HealthPotion"))
		{
			database.SaveHP1("false");
		}
		if (objetoColisionado.name.Equals("HealthPotion2"))
		{
			database.SaveHP2("false");
		}
		if (objetoColisionado.name.Equals("HealthPotion3"))
		{
			database.SaveHP3("false");
		}
		if (objetoColisionado.name.Equals("XPPotion"))
		{
			database.SaveXP1("false");
		}
		if (objetoColisionado.name.Equals("XPPotion2"))
		{
			database.SaveXP2("false");
		}
	}

	void DestroyEnemy()
	{
		if (database.LoadCh1().Equals("false"))
		{
			GameObject.Find("Champimudo1").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Champimudo1"));
		}
		if (database.LoadCh2().Equals("false"))
		{
			GameObject.Find("Champimudo2").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Champimudo2"));
		}
		if (database.LoadCh3().Equals("false"))
		{
			GameObject.Find("Champimudo3").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Champimudo3"));
		}
		if (database.LoadCh4().Equals("false"))
		{
			GameObject.Find("Champimudo4").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Champimudo4"));
		}
		if (database.LoadCh5().Equals("false"))
		{
			GameObject.Find("Champimudo5").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Champimudo5"));
		}
		if (database.LoadChF1().Equals("false"))
		{
			champimudoF1.SetActive(false);
		}
		if (database.LoadChF2().Equals("false"))
		{
			champimudoF2.SetActive(false);
		}
		if (database.LoadChF3().Equals("false"))
		{
			champimudoF3.SetActive(false);
		}
		if (database.LoadChF4().Equals("false"))
		{
			champimudoF4.SetActive(false);
		}
		if (database.LoadChF5().Equals("false"))
		{
			champimudoF5.SetActive(false);
		}
		if (database.LoadFh1().Equals("false"))
		{
			GameObject.Find("Florhada1").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Florhada1"));
		}
		if (database.LoadFh2().Equals("false"))
		{
			GameObject.Find("Florhada2").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Florhada2"));
		}
		if (database.LoadFh3().Equals("false"))
		{
			GameObject.Find("Florhada3").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Florhada3"));
		}
		if (database.LoadFh4().Equals("false"))
		{
			GameObject.Find("Florhada4").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Florhada4"));
		}
		if (database.LoadFh5().Equals("false"))
		{
			GameObject.Find("Florhada5").GetComponent<BoxCollider>().enabled = false;
			Destroy(GameObject.Find("Florhada5"));
		}
		if (database.LoadTr1().Equals("false"))
		{
			triFauces.SetActive(false);
		}
		if (database.LoadSc1().Equals("false"))
		{
			scarab.SetActive(false);
			if (database.LoadSiguienteConversacion() == 5)
			{
				musicaMalarcier.GetComponent<AudioSource>().Play();
				hole.SetActive(true);
			}
		}
	}

	void InitializeEnemies()
	{
		database.SaveCh1("true");
		database.SaveCh2("true");
		database.SaveCh3("true");
		database.SaveCh4("true");
		database.SaveCh5("true");
		database.SaveChF1("false");
		database.SaveChF2("false");
		database.SaveChF3("false");
		database.SaveChF4("false");
		database.SaveChF5("false");
		database.SaveFh1("true");
		database.SaveFh2("true");
		database.SaveFh3("true");
		database.SaveFh4("true");
		database.SaveFh5("true");
		database.SaveTr1("false");
		database.SaveSc1("false");
	}

	void ResetEnemies() 
	{
		database.SaveCh1("true");
		database.SaveCh2("true");
		database.SaveCh3("true");
		database.SaveCh4("true");
		database.SaveCh5("true");
		database.SaveFh1("true");
		database.SaveFh2("true");
		database.SaveFh3("true");
		database.SaveFh4("true");
		database.SaveFh5("true");
	}

	void OnApplicationQuit()
	{
		database.SavePlayerPosition(playerObject);
		database.SavePlayerRotation(playerObject);
	}

}
