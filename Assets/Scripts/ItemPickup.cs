using UnityEngine;
using System.Collections;

public class ItemPickup : Interactable {

	public Item item;	// Item to put in the inventory on pickup

	// When the player interacts with the item
	public override void Interact()
	{
		base.Interact();

		PickUp();	// Pick it up!
	}

	// Pick up the item
	void PickUp () {
		bool wasPickedUp = Inventory.instance.Add(item);	// Add to inventory

		// If successfully picked up
		if (wasPickedUp) {
			Destroy(gameObject);	// Destroy item from scene
		}
	}
	
	private void OnTriggerEnter(Collider other){
		//Debug.Log("enter " + other);
		if(other.tag == "Player") {
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
