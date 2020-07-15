using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";	// Name of the item
	public Sprite icon = null;	
	[HideInInspector]			// Item icon
	public bool isCurrentItem = false;      // Is the item default wear?
	public ItemType itemType;
	public GameObject prefab;
	
	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		/*if(itemType == ItemType.Weapon || itemType == ItemType.Material) {
			isCurrentWeapon = true;
		}
		Inventory.instance.EquipItem(this);*/

		Debug.Log("Using item " + name + " on quick access");
	}

	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}
	
	public enum ItemType { Weapon, Food, Building, Material }
	
}
