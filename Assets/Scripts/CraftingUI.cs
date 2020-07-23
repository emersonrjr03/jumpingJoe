using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour {
	
	public GameObject craftingUI;
	public Transform itemsRecipeParent;
	public GameObject ItemRecipeLine;
	
	public List<Item> craftableItems;

	private Inventory inventory;
	private int craftTableLevel;
	
	// Start is called before the first frame update
    void Start() {
    	inventory = Inventory.instance;
    }
    
    public void showCraftingUI(int level){
    	craftTableLevel = level;

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
			if(item.crafTableLevelNeededToCraft <= craftTableLevel){
				AddItemToCraftingUI(item);			
			}
		}
	}
	
	void AddItemToCraftingUI(Item item) {
		GameObject newItemRecipeLine = Instantiate (ItemRecipeLine) as GameObject;
		newItemRecipeLine.GetComponent<ItemRecipeLine>().AddItemRecipe(item, inventory.items);
		newItemRecipeLine.transform.SetParent (itemsRecipeParent, false);
	}
	
    
}
