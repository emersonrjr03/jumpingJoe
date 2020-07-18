using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEnabling : MonoBehaviour {

	public List<Collider> allItemsCollidingWithPlayer = new List<Collider>();	
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    	if(canBtnShow()){
		    GetComponent<FixedButton>().showButton();
    	} else {
		    GetComponent<FixedButton>().hideButton();
    	}
    }
    
    private bool canBtnShow() {
    	allItemsCollidingWithPlayer.RemoveAll(c => c == null);
    	return allItemsCollidingWithPlayer.Count > 0;
    }
}
