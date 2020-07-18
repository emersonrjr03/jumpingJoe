using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {
	CharacterStats myStats;
    
  	private float attackCooldown = 0f;
  	
    void Start() {
    	myStats = GetComponent<CharacterStats>();
    }

    void Update() {
    	attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats) { 
    	if(attackCooldown <= 0f) {
			targetStats.TakeDamage(myStats.damage + myStats.getExtraDamage());
			attackCooldown = 1f / myStats.attackSpeed;
    	}
    }
}
