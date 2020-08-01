using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Input = InputWrapper.Input;

public class MovingObjectController : MonoBehaviour {
	
	private bool movingObj = true;
	private Transform dragginMarkImage;
	private Button stopDraggingBtn;
	private bool wasPressed = false;
	private float timer;
	private float holdDur = 3f;

    // Start is called before the first frame update
    void Start() {
        Button[] buttons = transform.GetComponentsInChildren<Button>();
        stopDraggingBtn = buttons[0];
        dragginMarkImage = transform.GetChild(0).GetChild(0);
        stopDraggingBtn.onClick.AddListener(delegate { DisableDragging(); });
        
        dragginMarkImage.GetComponent<Image>().enabled = false;
        stopDraggingBtn.GetComponent<Image>().enabled = false;
    }

    /*// Update is called once per frame
    void Update(){
        if(movingObj) {
        	MoveCurrentPlaceableObjToTouch();
        }
    }
    */
    /*public void MoveCurrentPlaceableObjToTouch() {
    	//if(Input.touches.Length > 0) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hitInfo;
			if(Physics.Raycast(ray, out hitInfo)) {
				transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 1f, hitInfo.point.z);
				//we want the fence always up, not going according to the terrain.
				//currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
			}
    	//}
    }*/
    public void EnableDragging(){
    	movingObj = true;
    	dragginMarkImage.GetComponent<Image>().enabled = true;
        stopDraggingBtn.GetComponent<Image>().enabled = true;
	}
	
	public void DisableDragging(){
		movingObj = false;
		dragginMarkImage.GetComponent<Image>().enabled = false;
        stopDraggingBtn.GetComponent<Image>().enabled = false;
        timer = float.PositiveInfinity;
        wasPressed= false;

	}
	
	GameObject gObj = null;
	Plane objPlane;
	Vector3 m0;

	Ray GenerateTouchRay(Vector3 touchPos){
		Vector3 touchPosFar = new Vector3 (touchPos.x, touchPos.y, Camera.main.farClipPlane);
		Vector3 touchPosNear = new Vector3 (touchPos.x, touchPos.y, Camera.main.nearClipPlane);
		Vector3 touchPosF = Camera.main.ScreenToWorldPoint (touchPosFar);
		Vector3 touchPosN = Camera.main.ScreenToWorldPoint (touchPosNear);
		Ray mr = new Ray (touchPosN, touchPosF - touchPosN);
		return mr;
	}
	
	void UpdateTimer(){

			if(Input.touchCount > 0){
				if(!wasPressed){
					Debug.Log("set timer");
					timer = Time.time;
					wasPressed = true;
				} else {
					Debug.Log("time hold: " + (Time.time - timer));
					if(Time.time - timer > holdDur) {
						EnableDragging();
					}
				}
			} else {
				timer = float.PositiveInfinity;
			}
	}
	
	void Update() {
		if(Input.touchCount > 0){
			UpdateTimer();
			if(movingObj){
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					Ray touchRay = GenerateTouchRay (Input.GetTouch (0).position);
					RaycastHit hit;

					if (Physics.Raycast (touchRay.origin, touchRay.direction, out hit)) {
						gObj = hit.transform.gameObject;
						Debug.Log(gObj);
						objPlane = new Plane (Camera.main.transform.forward *= 1, gObj.transform.position);

						//Calcolo l'offset del touch
						Ray mRay = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
						float rayDistance;
						objPlane.Raycast (mRay, out rayDistance);
						m0 = gObj.transform.position - mRay.GetPoint (rayDistance);
					}
				} else if (Input.GetTouch (0).phase == TouchPhase.Moved && gObj) {
					Ray mRay = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
					float rayDistance;
					if (objPlane.Raycast (mRay, out rayDistance)) {
					 gObj.transform.position = mRay.GetPoint (rayDistance) + m0;
					}
				} else if (Input.GetTouch (0).phase == TouchPhase.Ended && gObj) {
					gObj = null;
				}
			}
		}
	}
}
