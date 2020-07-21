using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable {
	public CraftingUI craftingUI;
	
   // When the player interacts with the item
	public override void Interact()
	{
		base.Interact();

		craftingUI.showCraftingUI();
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
