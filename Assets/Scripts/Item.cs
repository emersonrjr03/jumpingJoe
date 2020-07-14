using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";	// Name of the item
	public Sprite icon = null;	
	[HideInInspector]			// Item icon
	public bool isCurrentWeapon = false;      // Is the item default wear?
	public ItemType itemType;
	
	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		if(itemType == ItemType.Weapon) {
			isCurrentWeapon = true;
			Inventory.instance.EquipItem(this);
		} else	if(itemType == ItemType.Food) {
			Debug.Log("Equip " + itemType);
		} else	if(itemType == ItemType.Building) {
			Debug.Log("Equip " + itemType);
		} else	if(itemType == ItemType.Material) {
			Debug.Log("Equip " + itemType);
		}
		// Use the item
		//If it's a weapon, we equip it
		//If it's a construction thing, we will add it to the quick placing slot
		//If it's food, we will eat it and recover HP

		Debug.Log("Using " + name);
	}

	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}
	
	public enum ItemType { Weapon, Food, Building, Material }
	
}
