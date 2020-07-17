using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHit : MonoBehaviour {

	public IEnemy parent;
	
	void Start(){
		parent = transform.root.GetComponent<IEnemy>();
		Debug.Log(parent);
	}
	
    void OnCollisionEnter(Collision collision) {
	    Debug.Log("enter child");
        parent.OnCollisionEnterChild(transform, collision);
    }
    
    void OnCollisionExit(Collision collision) {
        parent.OnCollisionExitChild(transform, collision);
    }
}
