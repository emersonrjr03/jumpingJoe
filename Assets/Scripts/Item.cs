using UnityEngine;
using System.Collections.Generic;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";	// Name of the item
	public Sprite icon = null;				// Item icon
	public GameObject prefab;
	
	public ItemType itemType;
	public int damage;
	public bool canWoodCut;
	public bool canRockCut;
	public int mass;
	public int crafTableLevelNeededToCraft;
	public List<Item> components;
	
	
	// Called when the item is pressed in the inventory
	public virtual void Use () {
		/*if(itemType == ItemType.Weapon || itemType == ItemType.Material) {
			isCurrentWeapon = true;
		}
		Inventory.instance.EquipItem(this);*/

		Debug.Log("Using item " + name + " on quick access");
	}

	public void RemoveFromInventory () {
		Inventory.instance.Remove(this);
	}
	
	public Dictionary<Item, int> getRecipe() {
		Dictionary<Item, int> recipe = new Dictionary<Item, int>();
		if(components != null) {
			foreach(Item item in components) {
				if(recipe.ContainsKey(item)) {
					recipe[item] = recipe[item] + 1;				
				} else {
					recipe.Add(item, 1);
				}
			}
		}
		return recipe;
	}
	
	public enum ItemType { Weapon, Food, Building, Material }
	
}
