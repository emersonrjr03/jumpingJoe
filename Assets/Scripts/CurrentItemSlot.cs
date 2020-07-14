using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentItemSlot : MonoBehaviour
{
    public Image icon;
	Item item;
	
	public void AddItem(Item newItem) {
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
	}
	
	public void UseItem() {
		if(item != null && item.itemType != Item.ItemType.Weapon) {
			item.Use();
		}
	}
}
