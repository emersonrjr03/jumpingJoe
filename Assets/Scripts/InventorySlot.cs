using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler {

	public Image icon;
	Item item;
	Vector3 iconInitialPos;
	
	Inventory inventory;	// Our current inventory
	Camera mainCamera;
	void Start () {
		inventory = Inventory.instance;
		Object[] cameras = FindObjectsOfType<Camera>() as UnityEngine.Object[];
		for (int i = 0; i < cameras.Length; i++){
			mainCamera = cameras[i] as Camera;
			break;
		}
	}
	
	public void AddItem(Item newItem) {
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
	}
	
	public void ClearSlot() {
		item = null;
		icon.sprite = null;
		icon.enabled = false;
	}
	
	public void EquipItem() {
		if(item != null) {
			Inventory.instance.EquipItem(item);
		}
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		if(item != null) {
			Debug.Log("POSICAO INICIAL " +  icon.gameObject.transform.position);
			iconInitialPos = icon.gameObject.transform.position;
			icon.gameObject.transform.position = eventData.position;
		}
	}
	
	public void OnDrag(PointerEventData eventData) {
		if(item != null) {
			icon.gameObject.transform.position = eventData.position;
		}
	}
	public void OnDrop(PointerEventData eventData){	
		Debug.Log("Dropping " + item);
		if(item != null) {
			Debug.Log( mainCamera.ScreenToWorldPoint(eventData.position));			
			Vector3 dropPosition = mainCamera.gameObject.transform.position - mainCamera.gameObject.GetComponent<PlayerFollow>().offset;
			Instantiate(item.prefab, dropPosition, Quaternion.identity);
			icon.gameObject.transform.position = new Vector3(0f,0f,0f);
			Inventory.instance.Remove(item);
		} else {
			Debug.Log("Resetting " + icon.gameObject.transform.position);
			icon.gameObject.transform.position = new Vector3(0f,0f,0f);
		}
	}
	
}
