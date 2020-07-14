using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	public GameObject inventoryUI;	// The entire inventory UI
	public GameObject currentWeaponSlotUI;
	public GameObject currentFoodSlotUI;
	public GameObject currentBuildingSlotUI;
	
	public Transform itemsParent;	// The parent object of all the items
	public FixedButton openInventoryButton;
	public FixedButton closeInventoryButton;
	
	Inventory inventory;	// Our current inventory

	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		inventory.onItemEquipedCallback += UpdateCurrentItemSlotUI;
		inventory.onItemRemovedCallback += RemoveFromCurrentItemSlotUI;
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
	
	public void UpdateCurrentItemSlotUI(Item item){
		//Just get the CurrentItemSlot component that is inside the CurrentItemSlot
		
		CurrentItemSlot[] slots = new CurrentItemSlot[0];
		Debug.Log("checking where " + item.itemType + " will go");
		if(item.itemType == Item.ItemType.Weapon || item.itemType == Item.ItemType.Material) {
			slots = currentWeaponSlotUI.GetComponentsInChildren<CurrentItemSlot>();
		} else if(item.itemType == Item.ItemType.Food) {
			slots = currentFoodSlotUI.GetComponentsInChildren<CurrentItemSlot>();		
		} else if(item.itemType == Item.ItemType.Building) {
			slots = currentBuildingSlotUI.GetComponentsInChildren<CurrentItemSlot>();
		}

		for (int i = 0; i < slots.Length; i++) {
			slots[i].PlaceItem(item);
		}
	}
	
	public void RemoveFromCurrentItemSlotUI(Item item){
		CurrentItemSlot[] slots = new CurrentItemSlot[0];
		Debug.Log("removing " + item.itemType + " from currentItem");
		if(item.itemType == Item.ItemType.Weapon || item.itemType == Item.ItemType.Material) {
			slots = currentWeaponSlotUI.GetComponentsInChildren<CurrentItemSlot>();
		} else if(item.itemType == Item.ItemType.Food) {
			slots = currentFoodSlotUI.GetComponentsInChildren<CurrentItemSlot>();		
		} else if(item.itemType == Item.ItemType.Building) {
			slots = currentBuildingSlotUI.GetComponentsInChildren<CurrentItemSlot>();
		}
		for (int i = 0; i < slots.Length; i++) {
			slots[i].ClearItem();
		}
	}
}
