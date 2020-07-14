using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	public GameObject inventoryUI;	// The entire inventory UI
	public GameObject currentWeaponSlotUI;
	public Transform itemsParent;	// The parent object of all the items
	public FixedButton openInventoryButton;
	public FixedButton closeInventoryButton;
	
	Inventory inventory;	// Our current inventory

	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		inventory.onItemEquipedCallback += UpdateCurrentWeaponSlotUI;
	}

	// Check to see if we should open/close the inventory
	void Update () {

	}
	
	public void showInventoryUI(){
		inventoryUI.SetActive(true);
			UpdateUI();
	}
	
	public void hideInventoryUI(){
		inventoryUI.SetActive(false);
	}
	
		// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	public void UpdateUI () {
		InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();

		for (int i = 0; i < slots.Length; i++) {
			if (i < inventory.items.Count) {
				slots[i].AddItem(inventory.items[i]);
			} else {
				slots[i].ClearSlot();
			}
		}
	}
	
	public void UpdateCurrentWeaponSlotUI(Item item){
		CurrentItemSlot[] slots = currentWeaponSlotUI.GetComponentsInChildren<CurrentItemSlot>();
		for (int i = 0; i < slots.Length; i++) {
			slots[i].AddItem(item);
		}
	}
}
