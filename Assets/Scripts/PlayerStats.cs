using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	private GameObject playerRightHand;
	private Collider rightHandCollider;
	
	private GameObject playerLeftHand;
	private Collider leftHandCollider;

	public override void Awake() {
		base.Awake();

		List<GameObject> matches = Utils.GetChildObjectsByTag(transform, "Player Right Hand");
		if(matches.Count > 0) { 
			playerRightHand = matches[0];
			rightHandCollider = playerRightHand.GetComponent<Collider>();
		}
		
		matches = Utils.GetChildObjectsByTag(transform, "Player Left Hand");
		if(matches.Count > 0) {
			playerLeftHand = matches[0];
			leftHandCollider = playerLeftHand.GetComponent<Collider>();
		}
		
	}
	public override void Die () {
		GetComponent<PlayerState>().currentPlayerState = PlayerState.Dead;
		GetComponent<PlayerMovement>().enabled = false;
	}
	
	public void changePlayerCurrentWeapon(Item item){
    	foreach (Transform t in playerRightHand.transform) {
			Destroy(t.gameObject);
		}

    	GameObject newWeapon = (GameObject)Instantiate(item.prefab);
    	newWeapon.transform.SetParent(playerRightHand.transform, false);
		rightHandCollider = newWeapon.GetComponent<Collider>();
		
    	base.currentItem = item;
    }
    
    public void Attack(Collision collision) {
    	CharacterStats targetStatus = collision.transform.GetComponent<CharacterStats>();
    	if(targetStatus != null) {
    		bool isAttacking = false;
			foreach (ContactPoint c in collision.contacts) {
		        if(c.thisCollider == rightHandCollider || c.thisCollider == leftHandCollider) {
			        isAttacking = true;
		        	break;
		        }
		    }
		    Debug.Log("tem target");
		    if(isAttacking && collision.transform.tag == "Enemy"){
		    	GetComponent<CharacterCombat>().Attack(targetStatus);
		    }
    	}
    }
}
