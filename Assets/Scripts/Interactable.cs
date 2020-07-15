using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

[RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour {

	[HideInInspector]
	public bool isInteractable = false;	// Have we already interacted with the object?

	public FixedButton pickupButton;
	
	void Start() {
		//finding pickup button on screen and assign to the item, so we don't have to assign for every prefab.
		if(pickupButton == null){
			Object[] objects = FindObjectsOfType<FixedButton>() as UnityEngine.Object[];
			for (int i = 0; i < objects.Length; i++){
				FixedButton fixedButtonGO = objects[i] as FixedButton;
				if(fixedButtonGO.tag == "PickUpButton"){
					pickupButton = fixedButtonGO;
					break;
				}
			}
		}
	}
	void Update () {
		if (pickupButton.pressed && isInteractable ) {
			Interact();
		}
	}


	// This method is meant to be overwritten
	public virtual void Interact () {
		
	}

} 	
