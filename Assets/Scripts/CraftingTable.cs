using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable {
	private CraftingUI craftingUI;
	public int level = 0;
	
	public int[] quantityForNextLevel;
	public Item[] itemsForNextLevel;
	public int[] currentQuantityForNextLevel;

	protected override void Start() {
		base.Start();
		if(craftingUI == null) {
			craftingUI = GameObject.FindGameObjectsWithTag("Main Canvas")[0].GetComponent<CraftingUI>();
		}		
	}
   // When the player interacts with the item
	public override void Interact()
	{
		base.Interact();
		craftingUI.showCraftingUI(this);
	}
	
	public void AddMaterialToUpgrade(Item item, int quantity){
		currentQuantityForNextLevel[System.Array.IndexOf(itemsForNextLevel,item)] += quantity;
	}
	
	public bool canUpgradeCraftTable(){
		for(int i = 0; i < quantityForNextLevel.Length; i++) {
			if(quantityForNextLevel[i] != currentQuantityForNextLevel[i]){
				return false;
			}
		}
		return true;
	}
	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player") {
			GetComponentInChildren<Light>().enabled = true;
			GetComponent<ColorOnHover>().fireColoring();
			isInteractable = true;
		}
	}
	
	private void OnTriggerExit(Collider other){
		if(other.tag == "Player") {
			craftingUI.hideCraftingUI();
			GetComponentInChildren<Light>().enabled = false;
			GetComponent<ColorOnHover>().undoColoring();
			isInteractable = false;
		}
	}
}
