using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{

	public GameObject Prefab;
	public Animator animationController;
	public MeshRenderer treeMeshRenderer;
	
	public float lifeBar = 100;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision other) {
    	        Debug.Log("lifebar: " + lifeBar + " y: " + other.relativeVelocity.y);
        if(other.collider.tag == "Axe" && other.relativeVelocity.y != 0 && other.relativeVelocity.x == 0  && other.relativeVelocity.z == 0) {
	        lifeBar -= 20;

			if(lifeBar <= 0){
				Instantiate(Prefab, transform.position + new Vector3(transform.position.x + 0.000001f, transform.position.y + 0.03f , transform.position.z + 0.000001f), transform.rotation);
				Instantiate(Prefab, transform.position + new Vector3(transform.position.x + 0.0000015f, transform.position.y + 0.04f, transform.position.z + 0.0000015f), transform.rotation);
				Instantiate(Prefab, transform.position + new Vector3(transform.position.x + 0.0000020f, transform.position.y + 0.05f, transform.position.z + 0.0000020f), transform.rotation);
				animationController.Play("treeAnimation");
			}
        }
    }
}
