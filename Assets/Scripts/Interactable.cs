using UnityEngine;
using UnityEngine.AI;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

[RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour {

	public Transform interactionTransform;

	Transform player;		// Reference to the player transform

	[HideInInspector]
	public bool isInteractable = false;	// Have we already interacted with the object?

	public FixedButton pickupButton;
	
	void Update () {
		if (pickupButton.pressed && isInteractable ) {
			Debug.Log("Interacting");
			Interact();
		}
	}


	// This method is meant to be overwritten
	public virtual void Interact () {
		
	}

} 	
