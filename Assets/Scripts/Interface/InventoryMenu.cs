using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{

    bool inventoryOpen;

    public GameObject inventory;

    public GameObject datosObjeto;

    PickUp item;

    public Text tituloObjeto;

    public Text infoObjeto;

    public Image imagenObjeto;

    public Transform itemsParent;

    public InventorySlot[] slots;

    int objetoSeleccionado;

    public void InitializeInventoryMenu(Player jugador)
    {
        inventoryOpen = false;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        if (jugador != null)
        { 
            UpdateInventory(jugador);
        }
    }

    public bool IsOpen()
    {
        return inventoryOpen;
    }

    public void OpenInventory()
    {
        inventoryOpen = true;
        inventory.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryOpen = false;
        inventory.SetActive(false);
    }

    public void UpdateInventory(Player jugador)
    {
        if (jugador != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < jugador.GetListaObjetos().Count)
                {
                    slots[i].AddItem(jugador.GetListaObjetos()[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
    public void SeleccionarObjeto(int id)
    {
        objetoSeleccionado = id;
        if (slots[objetoSeleccionado - 1] != null)
        {
            item = slots[objetoSeleccionado - 1].item;
            UpdateItemSeleccionado(item);
            datosObjeto.SetActive(true);
        }
    }

    public void UpdateItemSeleccionado(PickUp pu)
    {
        imagenObjeto.sprite = pu.icon;
        if (pu.name.Equals("PocionVida"))
        {
            tituloObjeto.text = "Pocion de vida";
            infoObjeto.text = "Recuperas 20 HP al tomarla.";
        }
        if (pu.name.Equals("PocionXP"))
        {
            tituloObjeto.text = "Pocion de XP";
            infoObjeto.text = "Ganas 50 XP al tomarla.";
        }
    }

    public void UsarDatosObjeto()
    {
        slots[objetoSeleccionado - 1].SlotUsado();
        datosObjeto.SetActive(false);
    }

    public void UsarDatosObjeto2()
    {
        slots[objetoSeleccionado - 1].SlotUsado2();
        datosObjeto.SetActive(false);
    }

    public void CloseDatosObjeto()
    {
        datosObjeto.SetActive(false);
    }

}
