using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour {
	private GameObject currentPlaceableObject;
	private GameObject prefab;
	
	private Inventory inventory;
	
	private float rotation;
	
	private bool didFirstTouch = false;
    // Start is called before the first frame update
    void Start() {
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    private void Update() {
    	//if prefab was set, we are waiting for the user click to see where the fence will be instantiated
    	if(currentPlaceableObject != null && Input.GetMouseButton(0)) {
    	//if(currentPlaceableObject != null && Input.touches.Length > 0) {
    		Debug.Log("moving");
    		MoveCurrentPlaceableObjToTouch();
    	} else {
    		if(prefab != null && Input.GetMouseButtonDown(0)) {
			//if(prefab != null && Input.touches.Length > 0) {
				InstantiateObject();
			//if prefab == null it means that the object was already instantiated.
			} else if(prefab == null  && Input.touches.Length == 0) {
				PlaceObject();
			}
    	}
    	/*if(currentPlaceableObject != null) {
    		MoveCurrentPlaceableObjToTouch();
    		//RotateObject();
    	}*/       
    }
    
    public void AddToRotation(){
    	rotation += 5;
    }
    
    public void RemoveFromRotation(){
    	rotation -= 5;
    }
    
    public void PlaceObject(){
    	if(currentPlaceableObject != null) {
			currentPlaceableObject.transform.GetComponent<BuildingItem>().StopMovingObjOnGround(currentPlaceableObject);
			currentPlaceableObject = null;
    	}
    }
    
    private void RotateObject(){
    	 currentPlaceableObject.transform.Rotate(Vector3.up, rotation * 10f);
    }
    
    private void InstantiateObject() {
    	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

		RaycastHit hitInfo;
		if(Physics.Raycast(ray, out hitInfo)) {
			currentPlaceableObject = Instantiate(prefab, hitInfo.point, Quaternion.Euler(hitInfo.normal));
			prefab = null;
		}
		
    }
    public void StartMovingObjectOnGround(GameObject obj){
    	currentPlaceableObject = obj;
    	MoveCurrentPlaceableObjToTouch();
    }
    
    public void MoveCurrentPlaceableObjToTouch() {
    	//if(Input.touches.Length > 0) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hitInfo;
			if(Physics.Raycast(ray, out hitInfo)) {
				currentPlaceableObject.transform.position = hitInfo.point;
				//we want the fence always up, not going according to the terrain.
				//currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
			}
    	//}
    }
    
    public void HandleObjectPlaceament(GameObject goPrefab) {
    	/*if(currentPlaceableObject == null) {
    		currentPlaceableObject = Instantiate(goPrefab);
		}*/
		if(prefab == null) {
			prefab = goPrefab;
		}
    }
    
    public void UnplacePlaceableItem(GameObject obj){
    	Destroy(obj);
    	inventory.Add(obj.GetComponent<ItemPickup>().item);
    }
}
