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
	public GroundPlacementController groundPlacementController;
	void Start(){
	} 
	
	public void changePlayerCurrentWeapon(Item item){
		player.GetComponent<PlayerStats>().changePlayerCurrentWeapon(item);
	}
	
	public void StartMovingObjectOnGround(GameObject obj){
		groundPlacementController.StartMovingObjectOnGround(obj);
	}
	
	public void PlaceObjectOnGround() {
		groundPlacementController.PlaceObject();
	}
	
	public void UnplaceObjectFromGround(GameObject obj) {
		groundPlacementController.UnplacePlaceableItem(obj);
	}
}
