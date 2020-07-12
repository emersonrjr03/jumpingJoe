using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
	public Transform playerTransform;
	public float smoothSpeed;
	public Vector3 offset;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
    	Vector3 desiredPosition = playerTransform.position + offset;
    	Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    	transform.position = playerTransform.position + offset;
    	
    	transform.LookAt(playerTransform.position + offset);
    }
}
