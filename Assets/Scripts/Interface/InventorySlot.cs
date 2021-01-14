using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Button removeButton;
    
    public PickUp item;

    public GameObject game;

    public GameObject datosObjeto;

    InventoryMenu inventory;

    public void AddItem(PickUp newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnUseButton()
    {
        int objetoID = int.Parse(this.name.Substring(4));
        inventory = GameObject.Find("Inventory").GetComponent<InventoryMenu>();
        inventory.SeleccionarObjeto(objetoID);
    }

    public void OnRemoveButton()
    {
        game.GetComponent<Game>().RemoveItem(item, false);
        ClearSlot();
    }

    public void SlotUsado()
    {
        game.GetComponent<Game>().RemoveItem(item, true);
        ClearSlot();
    }

    public void OnRemoveButton2()
    {
        game.GetComponent<Game2>().RemoveItem(item, false);
        ClearSlot();
    }

    public void SlotUsado2()
    {
        game.GetComponent<Game2>().RemoveItem(item, true);
        ClearSlot();
    }
}
