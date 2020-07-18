using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	[HideInInspector]
	public InventorySlot selectedSlot;
	
	void Awake () {
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Inventory found!");
			return;
		}

		instance = this;
	}

	#endregion

	// Callback which is triggered when
	// an item gets added/removed.
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public delegate void OnItemEquiped(Item item);
	public OnItemEquiped onItemEquipedCallback;
	
	public delegate void OnItemRemoved(Item item);
	public OnItemEquiped onItemRemovedCallback;
	
	public int space = 20;	// Amount of slots in inventory

	// Current list of items in inventory
	public List<Item> items = new List<Item>();

	public void EquipItem(Item item) {
		// Trigger callback
		if (onItemEquipedCallback != null)
			onItemEquipedCallback.Invoke(item);
	}
	
	// Add a new item. If there is enough room we
	// return true. Else we return false.
	public bool Add (Item item) {

		// Check if out of space
		if (items.Count >= space) {
			Debug.Log("Not enough room.");
			return false;
		}

		items.Add(item);	// Add item to list

		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();

		return true;
	}

	// Remove an item
	public void Remove (Item item) {
		items.Remove(item);		// Remove item from list

		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
		
		// Trigger to call the clearing of the currentItem slot
		if (onItemRemovedCallback != null)
			onItemRemovedCallback.Invoke(item);
	
	}
	
	public void SetSelectedSlot(InventorySlot inventorySlot) {
		//if we had a selectedSlot before, we just undo the resize
		if(selectedSlot != null) {
			selectedSlot.transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
		}
		
		if(inventorySlot != null){
			inventorySlot.transform.localScale += new Vector3(0.1f, 0.1f, 0f);		
		}
		selectedSlot = inventorySlot;
	}
	
	public bool isSlotSelected(InventorySlot inventorySlot) {
		return selectedSlot != null && selectedSlot.Equals(inventorySlot);
	}

}
