using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentItemSlot : MonoBehaviour {
    public Image icon;
	Item item;
	
	private InventoryUI inventoryUI;
	
	public void Start(){
		
		
		inventoryUI = transform.root.GetComponent<InventoryUI>();
		/*for (int i = 0; i < inventoryUIArray.Length; i++) {
			inventoryUI = inventoryUIArray[0]; 
		}*/
	}
	public void PlaceItem(Item newItem) {
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		//equipping weapon here
		AfterPlacingItem();
	}
	
	public void UseItem() {
		if(item != null) {
			if(item.itemType == Item.ItemType.Food) {
				item.Use();
			} else if(item.itemType == Item.ItemType.Building) {
				inventoryUI.placeBuildingObject(item);
			}
		} else {
			inventoryUI.showInventoryUI();
		}
	}
	
	public void ClearItem() {
		if(item != null) {
			item = null;
			icon.sprite = null;
			icon.enabled = false;
		}
	}
	
	private void AfterPlacingItem(){
		if(item.itemType == Item.ItemType.Weapon || item.itemType == Item.ItemType.Material) {
			GameController.instance.changePlayerCurrentWeapon(item);
		} else if(item.itemType == Item.ItemType.Food) {
		
		} else if(item.itemType == Item.ItemType.Building) {

		}
	}
	
}
