using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : CharacterStats {
	private GameObject zombieRightHand;
	private Collider rightHandCollider;
	
	private GameObject zombieLeftHand;
	private Collider leftHandCollider;

	public override void Awake() {
		base.Awake();

		List<GameObject> matches = Utils.GetChildObjectsByTag(transform, "Enemy Right Hand");
		if(matches.Count > 0) { 
			zombieRightHand = matches[0];
			rightHandCollider = zombieRightHand.GetComponent<Collider>();
		}
		
		matches = Utils.GetChildObjectsByTag(transform, "Enemy Left Hand");
		if(matches.Count > 0) {
			zombieLeftHand = matches[0];
			leftHandCollider = zombieLeftHand.GetComponent<Collider>();
		}
		
	}
	public override void Die () {
		GetComponent<Animator>().SetBool("Attacking", false);
		GetComponent<Animator>().SetBool("Dead", true);
		GetComponent<Zombie>().enabled = false;
		
	}
	
	public void changeZombieCurrentWeapon(Item item){
    	foreach (Transform t in zombieRightHand.transform) {
			Destroy(t.gameObject);
		}

    	GameObject newWeapon = (GameObject)Instantiate(item.prefab);
    	newWeapon.transform.SetParent(zombieRightHand.transform, false);
		rightHandCollider = newWeapon.GetComponent<Collider>();
		
    	base.currentItem = item;
    }
    
    public void Attack(Collision collision) {
    	PlayerStats targetStatus = collision.transform.GetComponent<PlayerStats>();
    	if(targetStatus != null) {
    		bool isAttacking = false;
			foreach (ContactPoint c in collision.contacts) {
		        if(c.thisCollider == rightHandCollider || c.thisCollider == leftHandCollider) {
			        isAttacking = true;
		        	break;
		        }
		    }
			
		    if(isAttacking && collision.transform.tag == "Player"){
		    	GetComponent<CharacterCombat>().Attack(collision.transform.GetComponent<CharacterStats>());
		    }
    	}
    }
}
