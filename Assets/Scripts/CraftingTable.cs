using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable {
   // When the player interacts with the item
	public override void Interact()
	{
		base.Interact();

		Debug.Log("Interacting");
	}
	
	private void OnTriggerEnter(Collider other){
		//Debug.Log("enter " + other);
		if(other.tag == "Player") {
			GetComponentInChildren<Light>().enabled = true;
			GetComponent<ColorOnHover>().fireColoring();
			isInteractable = true;
		}
		/*ColorOnHover colorOnHover = other.GetComponent<ColorOnHover>();
		if(colorOnHover != null){
			colliders.Add(other);
			colorOnHover.fireColoring();
			other.GetComponent<Interactable>().isInteractable = true;
			pickUpButton.GetComponent<Image>().enabled = true;
			
		}*/
	}
	
	private void OnTriggerExit(Collider other){
		//Debug.Log("exit " + other);
		if(other.tag == "Player") {
			GetComponentInChildren<Light>().enabled = false;
			GetComponent<ColorOnHover>().undoColoring();
			isInteractable = false;
		}
		/*ColorOnHover colorOnHover = other.GetComponent<ColorOnHover>();
		if(colorOnHover != null){
			Debug.Log(colliders.Count);
			colliders.Remove(other);
			colorOnHover.undoColoring();
			other.GetComponent<Interactable>().isInteractable = false; 
			pickUpButton.GetComponent<Image>().enabled = colliders.Count != 0;
			
		}*/
	}
}
