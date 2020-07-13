using UnityEngine;
using UnityEngine.AI;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

[RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour {

	public float radius = 3f;
	public Transform interactionTransform;

	Transform player;		// Reference to the player transform

	bool hasInteracted = false;	// Have we already interacted with the object?

	public FixedButton pickupButton;
	
	void Update () {
		if (pickupButton.pressed) {
			Debug.Log("Interacting");
			// Interact with the object
			hasInteracted = true;
			Interact();
		}
	}


	// This method is meant to be overwritten
	public virtual void Interact () {
		
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}

} 	
