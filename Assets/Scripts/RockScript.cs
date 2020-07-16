using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
	public Transform rockPieceSpawnertransform;
	public GameObject rockPiecePrefab;
	public Animator animationController;
	public MeshRenderer RockMeshRenderer;
	
	public float lifeBar = 100;
	
    void OnCollisionEnter(Collision other) {
        if(lifeBar > 0 && other.collider.tag == "Axe" && other.relativeVelocity.y != 0 && other.relativeVelocity.x == 0  && other.relativeVelocity.z == 0) {
	        lifeBar -= 20;
	        Debug.Log(lifeBar);
		animationController.SetFloat("rockLifeBar", lifeBar);
		instantiateRockPieces(1);

        }
    }
    
    private void instantiateRockPieces(int quantity) {
    	int i = 0;
    	for (i=0; i < quantity; i++) {
    		Vector3 position = new Vector3 ( Random.Range(rockPieceSpawnertransform.position.x - i*0.5f - 1f, rockPieceSpawnertransform.position.x - i*0.5f - 0.5f),
    										 rockPieceSpawnertransform.position.y,
    										 rockPieceSpawnertransform.position.z
    									   );
    		Vector3 rotation = new Vector3 ( rockPieceSpawnertransform.rotation.x,
    										 Random.Range(rockPieceSpawnertransform.rotation.y - 90f, rockPieceSpawnertransform.rotation.y + 180f),
    										 Random.Range(rockPieceSpawnertransform.rotation.z - 90f, rockPieceSpawnertransform.rotation.z + 180f)
    									   );
    		
    		GameObject rockPiece = Instantiate(rockPiecePrefab, position, Quaternion.Euler(rotation));
    		Rigidbody rockPieceRigidBody = rockPiece.AddComponent<Rigidbody>();
    		rockPieceRigidBody.mass = 30;
    	}
    }
}
