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
	

	
	public GameObject statisticsToCraftCraftTableContainer;
	public GameObject craftTableLevel1Prefab;
	
	Inventory inventory;	// Our current inventory

	private Vector2 horizontalSize = new Vector2(530f, 225f);
	private Vector2 verticalSize = new Vector2(273f, 490f);

	//variables to keep track of which slots contains the current items (for display styling reasons)
	private Item currentWeapon;
	private Item currentFood;
	private Item currentBuilding;
	
	private GameObject craftCraftTableBtnGO;
	private GameObject statisticsPanelGO;
	private Text rocksCountTxt;
	private Text branchesCountTxt;
	private const int craftTableAmountRocksNeeded = 10;
	private const int craftTableAmountBranchesNeeded = 10;
	private int rocksCount = 0;
	private int branchesCount = 0;
	private bool craftTableAlreadyCrafted;
	
	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		inventory.onItemEquipedCallback += UpdateCurrentItemSlotUI;
		inventory.onItemRemovedCallback += RemoveFromCurrentItemSlotUI;
		
		craftTableAlreadyCrafted = false;
		craftCraftTableBtnGO = statisticsToCraftCraftTableContainer.transform.GetChild(1).gameObject;
		statisticsPanelGO = statisticsToCraftCraftTableContainer.transform.GetChild(2).gameObject;
		rocksCountTxt = statisticsPanelGO.transform.GetChild(1).GetComponent<Text>();
		branchesCountTxt = statisticsPanelGO.transform.GetChild(3).GetComponent<Text>();
	}

	// Check to see if we should open/close the inventory
	void Update () {

	}
	
	public void showInventoryUI(){
		updateCraftTableStatistics();
		if(isSomePanelOpened()) {
			inventoryUI.GetComponent<RectTransform>().sizeDelta = verticalSize;
			statisticsToCraftCraftTableContainer.SetActive(false);
		} else {
			showOrNotCraftTableStatistics();
			inventoryUI.GetComponent<RectTransform>().sizeDelta = horizontalSize;
		}
		inventoryUI.SetActive(true);
		UpdateUI();
	}
	
	public bool isSomePanelOpened(){
		bool result = false;
		
		result |= GetComponent<CraftingUI>().craftingUI.activeInHierarchy;
		//and in the future we can add more UIs here
		return result;
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

	private void showOrNotCraftTableStatistics(){
		if(craftTableAlreadyCrafted){
			statisticsToCraftCraftTableContainer.SetActive(false);
		} else {
			statisticsToCraftCraftTableContainer.SetActive(true);
		}
	}

	public void updateCraftTableStatistics(){
		if(craftTableAlreadyCrafted){
			showOrNotCraftTableStatistics();
		} else {
			rocksCount = 0;
			branchesCount = 0;
			foreach(Item item in inventory.items) {
				if(item.name == "Rock"){
					rocksCount++;
				} else if(item.name == "Branch"){
					branchesCount++;
				}
			}
			
			if(canCraftACraftTable()){
				craftCraftTableBtnGO.SetActive(true);
				statisticsPanelGO.SetActive(false);
				
			} else {
				craftCraftTableBtnGO.SetActive(false);
				statisticsPanelGO.SetActive(true);

				rocksCountTxt.text = rocksCount.ToString() + "/" + craftTableAmountRocksNeeded.ToString();
				branchesCountTxt.text = branchesCount.ToString() + "/" + craftTableAmountBranchesNeeded.ToString();
			}
		}
	}

	private bool canCraftACraftTable(){
		return rocksCount >= craftTableAmountRocksNeeded && branchesCount >= craftTableAmountBranchesNeeded;
	}
	
	private void removeRocksAndBranchesFromInventoryForCraftingTable(){
		int removedRocks = 0;
		int removedBranches = 0;
		List<Item> toBeRemoved = inventory.items.FindAll(item => {
			if(item.name == "Rock" && removedRocks != craftTableAmountRocksNeeded){
				removedRocks++;
				return true;
			} else if(item.name == "Branch" && removedBranches != craftTableAmountBranchesNeeded){
				removedBranches++;
				return true;
			} else {
				return false;
			}
		});
		
		
		foreach(Item item in toBeRemoved) {
			inventory.Remove(item);
		}
	}
	
	public void craftCraftTable(Transform player){
		removeRocksAndBranchesFromInventoryForCraftingTable();

		GameObject craftTable = Instantiate(craftTableLevel1Prefab, new Vector3(player.position.x, 
																				player.position.y + 0.8f, 
																				player.position.z), craftTableLevel1Prefab.transform.rotation);
		craftTableAlreadyCrafted = true;
		hideInventoryUI();
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
