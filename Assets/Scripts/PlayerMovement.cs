using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rb;
	public float movingVelocity = 2000f;
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
	}
	
    void Update() {		
	    if((joystick.Horizontal > 0.5 || joystick.Vertical > 0.5) || (joystick.Horizontal < -0.5 || joystick.Vertical < -0.5) ){
    		playerState.currentPlayerState = PlayerState.Running;
    	} else if((joystick.Horizontal > 0 || joystick.Vertical > 0) || (joystick.Horizontal < 0 || joystick.Vertical < 0) ){
    		playerState.currentPlayerState = PlayerState.Walking;
    	} else {
    		playerState.currentPlayerState = PlayerState.Idle;
    	}
		if(jumpButton.pressed && IsGrounded()) {
		    playerState.currentPlayerState = PlayerState.Jumping;
			rb.velocity += Vector3.up * jumpForce;
		}
		
		if(attackButton.pressed) {
		    playerState.currentPlayerState = PlayerState.Attacking;
		}
		Debug.Log(playerState.currentPlayerState);
    }
    
    private bool IsGrounded() {
    	return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }
}
