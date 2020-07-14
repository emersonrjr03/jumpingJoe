using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentItemSlot : MonoBehaviour
{
    public Image icon;
	Item item;
	private InventoryUI inventoryUI;
	
	public void Start(){
		
		InventoryUI[] inventoryUIArray = transform.parent.parent.gameObject.GetComponentsInChildren<InventoryUI>();
		for (int i = 0; i < inventoryUIArray.Length; i++) {
			inventoryUI = inventoryUIArray[0]; 
			Debug.Log(inventoryUI);
		}
	}
	public void PlaceItem(Item newItem) {
		if(item != null){
			item.isCurrentItem = false;
		}		
		newItem.isCurrentItem = true;
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
	}
	
	public void UseItem() {
		if(item != null) {
			if(item.itemType != Item.ItemType.Weapon && item.itemType != Item.ItemType.Material) {
				item.Use();
			}
		} else {
			inventoryUI.showInventoryUI();
		}
	}
	
	public void ClearItem() {
		if(item != null) {
			item.isCurrentItem = false;
			item = null;
			icon.sprite = null;
			icon.enabled = false;
		}
	}
	
	private void openInventoryUI() {
	}
}
