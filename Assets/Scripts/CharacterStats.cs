using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public float attackSpeed = 1f;
	public int damage = 5;
	public int armor;
	public int maxHealth = 100;
	public HealthBar healthBar;
	
	public Item currentItem;
	
	public int currentHealth { get; private set; }

	public virtual void Awake() {
		currentHealth = maxHealth;
	}
	
	// Damage the character
	public void TakeDamage (int damage) {
		// Subtract the armor value
		damage -= armor;
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		// Damage the character
		currentHealth -= damage;
		
		healthBar.setHealth(currentHealth);
		// If health reaches zero
		if (currentHealth <= 0) {
			Die();
		}
	}
	
	public virtual void Die () {
		// Die in some way
		// This method is meant to be overwritten
	}
	
	public virtual int getExtraDamage() {
		int extraDamage = 0;
		
		if(currentItem)
			extraDamage += currentItem.damage;

		return extraDamage;
	}
	
}
