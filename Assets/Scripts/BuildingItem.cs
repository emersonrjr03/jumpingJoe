using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingItem : MonoBehaviour {
	
	private Button unplaceObjBtn;
	private Button moveObjBtn;
	public GameController gameController;

    void Start() {
    	gameController = GameController.instance;
    	Button[] buttons = transform.GetChild(1).GetComponentsInChildren<Button>();
    	unplaceObjBtn = buttons[0];
    	moveObjBtn = buttons[1];
    	
    	unplaceObjBtn.onClick.AddListener(delegate { UnplaceObjectFromGround(unplaceObjBtn.transform.parent.parent.gameObject); });
    	
    	moveObjBtn.onClick.AddListener(delegate { StartMovingObjectOnGround(moveObjBtn.transform.parent.parent.gameObject); });
    	
    	unplaceObjBtn.gameObject.SetActive(false);
		moveObjBtn.gameObject.SetActive(true);
    }
    
	public void UnplaceObjectFromGround(GameObject obj) {
		Debug.Log("removing " + obj);
		gameController.UnplaceObjectFromGround(obj);
	}
	
	public void StartMovingObjectOnGround(GameObject obj) {
		unplaceObjBtn.gameObject.SetActive(true);
		moveObjBtn.gameObject.SetActive(false);
		Debug.Log("moving " + obj);
		gameController.StartMovingObjectOnGround(obj);
	}
	
	public void StopMovingObjOnGround(GameObject obj) {
		unplaceObjBtn.gameObject.SetActive(false);
		moveObjBtn.gameObject.SetActive(true);
		Debug.Log("stop moving " + obj);
	}
}
