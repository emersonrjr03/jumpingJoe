using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;
	Item item;
	
	Inventory inventory;	// Our current inventory

	void Start () {
		inventory = Inventory.instance;
	}
	
	public void AddItem(Item newItem) {
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}
	
	public void ClearSlot() {
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}
	
	public void OnRemoveButton() {
		Inventory.instance.Remove(item);
	}
	
	public void EquipItem() {
		if(item != null) {
			Inventory.instance.EquipItem(item);
		}
	}
}
