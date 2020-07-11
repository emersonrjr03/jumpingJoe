using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
	private Rigidbody rb;
	private Animator animator;
		
	public PlayerState playerState;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
    	animator.SetBool("Walking", playerState.currentPlayerState.Equals(PlayerState.Walking));
    	animator.SetBool("Running", playerState.currentPlayerState.Equals(PlayerState.Running));
		animator.SetBool("Jumping", playerState.currentPlayerState.Equals(PlayerState.Jumping));
		animator.SetBool("Attacking", playerState.currentPlayerState.Equals(PlayerState.Attacking));
		animator.SetBool("Dead", playerState.currentPlayerState.Equals(PlayerState.Dead));
    }
}
