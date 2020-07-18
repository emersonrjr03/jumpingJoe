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
	
	void Start(){

	} 
	
	public void changePlayerCurrentWeapon(Item item){
		player.GetComponent<PlayerStats>().changePlayerCurrentWeapon(item);
	}
    
}
