using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
	public Transform stumpSpawnertransform;
	public Transform branchesSpawnertransform;
	public GameObject stumpPrefab;
	public GameObject branchesPrefab;
	public Animator animationController;
	public MeshRenderer treeMeshRenderer;
	
	public float lifeBar = 100;
	
    void OnCollisionEnter(Collision other) {
        if(lifeBar > 0 && other.collider.tag == "Axe" && other.relativeVelocity.y != 0 && other.relativeVelocity.x == 0  && other.relativeVelocity.z == 0) {
	        lifeBar -= 20;
			if(lifeBar <= 0){
				Instantiate(stumpPrefab, stumpSpawnertransform.position, stumpSpawnertransform.rotation);
				instantiateBranches(3);
				animationController.Play("treeAnimation");
			}
        }
    }
    
    private void instantiateBranches(int quantity) {
    	int i = 0;
    	for (i=0; i < quantity; i++) {
    		Vector3 position = new Vector3 ( Random.Range(branchesSpawnertransform.position.x - i*0.5f - 1f, branchesSpawnertransform.position.x - i*0.5f - 0.5f),
    										 branchesSpawnertransform.position.y,
    										 branchesSpawnertransform.position.z
    									   );
    		Vector3 rotation = new Vector3 ( branchesSpawnertransform.rotation.x,
    										 Random.Range(branchesSpawnertransform.rotation.y - 90f, branchesSpawnertransform.rotation.y + 180f),
    										 Random.Range(branchesSpawnertransform.rotation.z - 90f, branchesSpawnertransform.rotation.z + 180f)
    									   );
    		Instantiate(branchesPrefab, position, Quaternion.Euler(rotation));
    	}
    }
}
