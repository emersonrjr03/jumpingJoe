using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";	// Name of the item
	public Sprite icon = null;				// Item icon
	public bool isDefaultItem = false;      // Is the item default wear?

	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		
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
	
}
