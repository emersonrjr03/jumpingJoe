using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	public int attackForce = 5;
	public float lookRadius = 10f;
	public float atackRadius = 1f;
	public float moveSpeed = 5f;
	public CapsuleCollider rightHandCollider;
	public CapsuleCollider leftHandCollider;
	
	Transform target;
	Transform transformZombie;
	Animator animatorController;
	
	private Vector3 direction;
	private float distance;
	private bool isAttacking = false;
	private ZombieStats zombieStats;
	
	Vector3 lastPosition = Vector3.zero;
	public float actualSpeed = 0;
	 
    // Start is called before the first frame update
    void Start() {
        transformZombie = GetComponent<Transform>();
        animatorController = GetComponent<Animator>();
        target = GameController.instance.player.transform;
        zombieStats = GetComponent<ZombieStats>();
    }

    // Update is called once per frame
    void Update() {
        distance = Vector3.Distance(target.position, transform.position);
        FaceTarget();
    }
    void FixedUpdate() {
        actualSpeed = (transform.position - lastPosition).magnitude;
	    lastPosition = transform.position;
		animatorController.SetFloat("Speed", actualSpeed);
    	if(distance > atackRadius) {
    		animatorController.SetBool("Attacking", false);
			MoveZombie();
    	} else {
    		//play atack animation if we are in a good distance from the player and if we are not dead
    		if(!Utils.AnimationIsPlaying(animatorController, "Attacking")) {
    			animatorController.SetBool("Attacking", true);
    		}
    	}
    }

	void MoveZombie(){
    	transformZombie.position = Vector3.MoveTowards(transform.position, target.position, (moveSpeed * Time.deltaTime));
	}
    void FaceTarget(){
    	direction = (target.position - transform.position).normalized;
    	Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    	transform.rotation = Quaternion.Slerp(transform.rotation,  lookRotation, Time.deltaTime * 5f);
    }
       
    void OnCollisionEnter(Collision collision) {
    	zombieStats.Attack(collision);
    }
    //using this approach to avoid one attack hitting twice
    void OnCollisionExit(Collision collision) {
    	isAttacking = false;
    }

}
