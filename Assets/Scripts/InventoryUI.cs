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
	
	Inventory inventory;	// Our current inventory

	//variables to keep track of which slots contains the current items (for display styling reasons)
	private Item currentWeapon;
	private Item currentFood;
	private Item currentBuilding;
	
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
	
	/**
		This method loops throug all the slots, and paint the first slot that has the current item, we paint the first only
		because we remove the item from the currentItems, after coloring the first slot
	**/
	public void UpdateSlotColors(){
		InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();
		List<Item> currentItems = new List<Item>(){ currentWeapon, currentFood, currentBuilding };
		for (int i = 0; i < slots.Length; i++) {
			if(slots[i].getItem() != null && currentItems.Contains(slots[i].getItem())){
				Item item = slots[i].getItem();
				currentItems.Remove(item);
				//selected weapon item #87BAFF (azul)
				if(item.itemType == Item.ItemType.Weapon || item.itemType == Item.ItemType.Material) {
					slots[i].setSlotColor(new Color32(135, 186, 255, 255));
				
				//selected food item #7CAB73 (verde) 
				} else if(item.itemType == Item.ItemType.Food) {
					slots[i].setSlotColor(new Color32(124, 171, 115, 255));

				//selected building item #FFBB64 (laranja)
				} else if(item.itemType == Item.ItemType.Building) {
					slots[i].setSlotColor(new Color32(255, 187, 100, 255));
				}
			} else {
				slots[i].setSlotColor(new Color32(255, 255, 255, 255));
			}
		}
	}
	
	public void UpdateCurrentItemSlotUI(Item item){
		
		//Just get the CurrentItemSlot component that is inside the CurrentItemSlot
		CurrentItemSlot[] slots = new CurrentItemSlot[0];
		if(item.itemType == Item.ItemType.Weapon || item.itemType == Item.ItemType.Material) {
			slots = currentWeaponSlotUI.GetComponentsInChildren<CurrentItemSlot>();

			currentWeapon = inventory.selectedSlot.getItem();
			
		} else if(item.itemType == Item.ItemType.Food) {
			slots = currentFoodSlotUI.GetComponentsInChildren<CurrentItemSlot>();		

			currentFood = inventory.selectedSlot.getItem();
		} else if(item.itemType == Item.ItemType.Building) {
			slots = currentBuildingSlotUI.GetComponentsInChildren<CurrentItemSlot>();

			currentBuilding = inventory.selectedSlot.getItem();
		}

		for (int i = 0; i < slots.Length; i++) {
			slots[i].PlaceItem(item);
		}
		UpdateSlotColors();
	}
	
	public void RemoveFromCurrentItemSlotUI(Item item){
		CurrentItemSlot[] slots = new CurrentItemSlot[0];
		if(item.itemType == Item.ItemType.Weapon || item.itemType == Item.ItemType.Material) {
			slots = currentWeaponSlotUI.GetComponentsInChildren<CurrentItemSlot>();
			currentWeapon = null;
		} else if(item.itemType == Item.ItemType.Food) {
			slots = currentFoodSlotUI.GetComponentsInChildren<CurrentItemSlot>();
			currentFood = null;
		} else if(item.itemType == Item.ItemType.Building) {
			slots = currentBuildingSlotUI.GetComponentsInChildren<CurrentItemSlot>();
			currentBuilding = null;
		}
		for (int i = 0; i < slots.Length; i++) {
			slots[i].ClearItem();
		}
		UpdateSlotColors();
	}

}
