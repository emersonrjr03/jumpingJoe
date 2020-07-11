using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	public const string Idle = "Idle";
	public const string Walking = "Walking";
	public const string Running = "Running";
	public const string Jumping = "Jumping";
	public const string Attacking = "Attacking";
	public const string Dead = "Dead";
	
	public string currentPlayerState;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerState = Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
