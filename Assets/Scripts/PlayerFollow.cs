using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
	public Transform playerTransform;
	public float smoothSpeed;
	public Vector3 offset;
	
    // Start is called before the first frame update
    void Start() {
    
        
    	transform.position = playerTransform.position + offset;
    	
    	transform.LookAt(playerTransform.position + offset);
    	
    }

    void FixedUpdate() {
    	Vector3 desiredPosition = playerTransform.position + offset;
    	Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    	
    	transform.position = playerTransform.position + offset;
    	transform.LookAt(playerTransform.position);
    	/*
    	float h = playerTransform.GetComponent<PlayerMovement>().joystick.Horizontal;
    	float v = playerTransform.GetComponent<PlayerMovement>().joystick.Vertical;
    	
    	Vector3 offsetFake = new Vector3(10,4,10);
    	Vector3 camOffset = new Vector3 (h * offsetFake.x * -1f, offsetFake.y, v * offsetFake.z * -1f);
    	transform.position = playerTransform.position + camOffset;
    	transform.LookAt(playerTransform.position);
    	if(playerTransform.GetComponent<PlayerMovement>().joystick.Horizontal != 0f || playerTransform.GetComponent<PlayerMovement>().joystick.Vertical != 0f){
    		Debug.Log(camOffset);
    		Debug.Log("H " + playerTransform.GetComponent<PlayerMovement>().joystick.Horizontal + 
    					" V " + playerTransform.GetComponent<PlayerMovement>().joystick.Vertical);
    	}*/
    	
    }
}
