using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour {
	
	public GameObject craftingUI;
	public Transform itemsRecipeParent;
	public GameObject ItemRecipeLine;
	public GameObject levelUpCraftTableBtn;
	public Text craftLevelTxt;
	
	public GameObject upgradeDlg;
	
	public GameObject[] craftTablePrefabs;
	public List<Item> craftableItems;
	public GameObject materialNeededToUpgradeLine;
	
	private Inventory inventory;
	private CraftingTable craftTable;
	
	// Start is called before the first frame update
    void Start() {
    	inventory = Inventory.instance;
    }
    
    public void showCraftingUI(CraftingTable craftTableObj){
    	levelUpCraftTableBtn.SetActive(craftTableObj.level != craftTablePrefabs.Length);
    	craftLevelTxt.text = craftTableObj.level.ToString();
    	craftTable = craftTableObj;

	    hideConfirmationToUpgradeCraftTable();
		craftingUI.SetActive(true);
		updateItems();
		if(GetComponent<InventoryUI>().inventoryUI.activeInHierarchy){
    		GetComponent<InventoryUI>().showInventoryUI();
    	}
	}
	
	public void hideCraftingUI(){
		craftingUI.SetActive(false);
		if(GetComponent<InventoryUI>().inventoryUI.activeInHierarchy){
			GetComponent<InventoryUI>().hideInventoryUI();
		}
	}
	
	public void updateItems(){
		foreach (Transform child in itemsRecipeParent) {
			Destroy(child.gameObject);
		}
		foreach(Item item in craftableItems){
			if(item.crafTableLevelNeededToCraft <= craftTable.level){
				AddItemToCraftingUI(item);			
			}
		}
	}
	
	void AddItemToCraftingUI(Item item) {
		GameObject newItemRecipeLine = Instantiate (ItemRecipeLine) as GameObject;
		newItemRecipeLine.GetComponent<ItemRecipeLine>().AddItemRecipe(item, inventory.items);
		newItemRecipeLine.transform.SetParent (itemsRecipeParent, false);
	}

	public void AddMaterialToUpgrade(Item item) {
		//check inventory if we have enougth item
		if(inventory.hasAtLeastThatAmountOfItemsInTheInventory(item, 1)){
			//add one more to the current amount of needed materials
			craftTable.AddMaterialToUpgrade(item, 1);
			inventory.Remove(item);
		}
	}
	
	/**
	 This function is called by the confirm btn from the dialog after the user clicks in UPGRADE 
	 in the CraftTableUI
	**/ 
	public void showUpgradeCraftTableDlg() {
		//calculate and set the amount needed of each item.
		
		//ItemsNeededForUpgradeParent
		updateItemsNeeded(upgradeDlg.transform.GetChild(2));
		upgradeDlg.SetActive(true);
	}
	
	private void updateItemsNeeded(Transform parent) {
		foreach(Transform child in parent) {
			Destroy(child.gameObject);
		}
		
		//for each of the items needed to acheve the next level, we add one line
		//in the neededMaterialsDlg
		for(int i = 0; i < craftTable.itemsForNextLevel.Length; i++) {
			AddLineOfMaterialNeededToUpgrade(parent, craftTable.itemsForNextLevel[i]);	
		}
	}
	
	private void AddLineOfMaterialNeededToUpgrade(Transform parent, Item item){
		GameObject newLineMaterialNeeded = Instantiate (materialNeededToUpgradeLine) as GameObject;
		//this guy handles the icon, quantity, and the btn function
		newLineMaterialNeeded.GetComponent<MaterialNeededToUpgradeLine>().AddNewLine(craftTable, item);
		newLineMaterialNeeded.transform.SetParent (parent, false);
	}

	/**
	 This function is called by the cancel btn from the dialog after the user clicks in UPGRADE 
	 in the CraftTableUI
	**/ 
	public void hideConfirmationToUpgradeCraftTable() {
		upgradeDlg.SetActive(false);
	}

	/**
	 This function is called by the UPGRADE btn in the CraftTableUI.
	**/
	public void upgradeCraftTabel(){
		hideCraftingUI();

		int newlevel = craftTable.level + 1;

		Destroy(craftTable.transform.gameObject);
		Vector3 newPos = craftTable.transform.position;
		
		GameObject craftTableGO = Instantiate(craftTablePrefabs[newlevel - 1], 
												new Vector3(newPos.x, newPos.y, newPos.z), 
												craftTablePrefabs[newlevel - 1].transform.rotation);
		craftTable = craftTableGO.GetComponent<CraftingTable>();	
	}

    
}
