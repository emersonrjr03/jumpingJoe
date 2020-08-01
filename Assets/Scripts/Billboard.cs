using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public Transform camera;
	
	void Start(){
		if(camera == null) {
			camera = Camera.main.transform;		
		}
	}
	//using this because it's called after update
    void LateUpdate() {
        transform.LookAt(transform.position + camera.forward);
    }
}
