using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	#region Singleton

	public static GameController instance;
	
	void Awake ()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Inventory found!");
			return;
		}

		instance = this;
	}

	#endregion
	
	public GameObject player;
	public GameObject currentWeapon;
	public HealthBar healthBar;

	private GameObject playerRightHand;
	private GameObject playerLeftHand;
	
	void Start(){
		healthBar.setHealth(100);

		List<GameObject> matches = Utils.GetChildObjectsByTag(player.transform, "Player Right Hand");
		if(matches.Count > 0) { 
			playerRightHand = matches[0];
		}
		
		matches = Utils.GetChildObjectsByTag(player.transform, "Player Left Hand");
		if(matches.Count > 0) { 
			playerLeftHand = matches[0];
		}
		
		if(currentWeapon != null){
			changePlayerCurrentWeapon(currentWeapon);
		}
	} 
	
	public void changePlayerCurrentWeapon(Item item){
		changePlayerCurrentWeapon(item.prefab);
	}

    public void changePlayerCurrentWeapon(GameObject newWeapon){
    	foreach (Transform t in playerRightHand.transform) {
			Destroy(t.gameObject);
		}

    	currentWeapon = (GameObject)Instantiate(newWeapon);
    	currentWeapon.transform.SetParent(playerRightHand.transform, false);;
    }
    
    public int getPlayerHealth(){
    	return healthBar.getHealth();
    }
    
    public void setPlayerHealth(int health) {
    	healthBar.setHealth(health);
    	if(healthBar.getHealth() == 0f) {
    		killPlayer();
    	}
    }
    
    private void killPlayer() {
    	player.GetComponent<PlayerState>().currentPlayerState = PlayerState.Dead;
    	player.GetComponent<PlayerMovement>().enabled = false;
    }
    
}
