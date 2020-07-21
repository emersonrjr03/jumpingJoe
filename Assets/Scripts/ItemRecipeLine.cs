using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecipeLine : MonoBehaviour
{
	public Transform itemRecipeLine;//this one has fixed 3 childs
	public Transform itemResult;
	
	private const int CHILD_ICON = 0; 
	private const int CHILD_QUANTITY_TEXT = 1;
	
	public void AddItemRecipe(Item item, List<Item> inventoryItems) {
		int i = 0;
		bool hasEnougthItems = true;
		foreach(KeyValuePair<Item, int> entry in item.getRecipe()) {
			int howManyItemsInInventory = countHowManyOfThisItem(entry.Key, inventoryItems);
			hasEnougthItems &= howManyItemsInInventory >= entry.Value;
			itemRecipeLine.GetChild(i).GetComponent<ComponentInfo>().AddComponent(entry.Key, entry.Value, countHowManyOfThisItem(entry.Key, inventoryItems)); 
			i++;
		}
		if(!hasEnougthItems) {
			itemResult.GetChild(0).GetComponent<ResultItemInfo>().AddResult(item, 0);
		} else {
			itemResult.GetChild(0).GetComponent<ResultItemInfo>().AddResult(item, countHowManyOfThisCanBeMade(item, inventoryItems));
		}
	}
	
	private int countHowManyOfThisItem(Item itemToLookFor, List<Item> items) {
		int count = 0;
		foreach(Item i in items) {
			if(i.Equals(itemToLookFor)) {
				count++;		
			}
		}
		return count;
	}
	
	private int countHowManyOfThisCanBeMade(Item itemToBeMade, List<Item> itemsInInventory){
		int[] qnty = {99999,99999,99999};
		int i = 0;
		foreach(KeyValuePair<Item, int> entry in itemToBeMade.getRecipe()) {
			qnty[i] = countHowManyOfThisItem(entry.Key, itemsInInventory) / entry.Value;
			Debug.Log("tenho " + countHowManyOfThisItem(entry.Key, itemsInInventory) + " " + entry.Key);
			i++;
		}
		return Mathf.Min(qnty);
	}
}

