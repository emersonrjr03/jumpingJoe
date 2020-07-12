﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rb;
	public Transform transform;

	public float movingVelocity = 2000f;
	public float rotationSpeed = 20;
	public float jumpForce = 100f;
	public float distToGround = 0.5f;
	public float animationTime = 2;

	public PlayerState playerState;
	
	protected Joystick joystick;
	public FixedButton jumpButton;
	public FixedButton attackButton;

	void Start(){
		joystick = FindObjectOfType<Joystick>();
	}
	
    void FixedUpdate() {
		rb.velocity = new Vector3( joystick.Horizontal * movingVelocity, 
									rb.velocity.y,
									joystick.Vertical * movingVelocity);

		changeLookingDirection(joystick);
	}
	
    void Update() {
		checkJoystickAndSetState(joystick);

		if(jumpButton.pressed && IsGrounded()) {
		    playerState.currentPlayerState = PlayerState.Jumping;
			rb.velocity += Vector3.up * jumpForce;
		}
		
		if(attackButton.pressed) {
		    playerState.currentPlayerState = PlayerState.Attacking;
		}
    }
    
	private void changeLookingDirection(Joystick joystick){		 
		Vector3 currentRotation = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

		if(currentRotation != Vector3.zero) {
		    Quaternion lookRotation = Quaternion.LookRotation(currentRotation, Vector3.up);
		    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
		}
	}

	private void checkJoystickAndSetState(Joystick joystick){
		if((joystick.Horizontal > 0.5 || joystick.Vertical > 0.5) || (joystick.Horizontal < -0.5 || joystick.Vertical < -0.5) ){
    		playerState.currentPlayerState = PlayerState.Running;
    	} else if((joystick.Horizontal > 0 || joystick.Vertical > 0) || (joystick.Horizontal < 0 || joystick.Vertical < 0) ){
    		playerState.currentPlayerState = PlayerState.Walking;
    	} else {
    		playerState.currentPlayerState = PlayerState.Idle;
    	}
	}

    private bool IsGrounded() {
    	return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }
}
