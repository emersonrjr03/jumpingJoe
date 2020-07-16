using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	public Image icon;
	Item item;
	Vector2 iconInitialPos;
	
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
			if(Inventory.instance.isSlotSelected(this)){
				Inventory.instance.EquipItem(item);
			} else {
				Inventory.instance.SetSelectedSlot(this);
			}

		}
	}
	
	public void OnBeginDrag(PointerEventData eventData) {
		if(item != null) {
			iconInitialPos = icon.gameObject.transform.position;
			icon.gameObject.transform.position = eventData.position;
		}
	}
	
	public void OnDrag(PointerEventData eventData) {
		if(item != null) {
			icon.gameObject.transform.position = eventData.position;
		}
	}
	public void OnEndDrag(PointerEventData eventData) {
		if(item != null) {
			icon.gameObject.transform.position = iconInitialPos;
			Vector3 dropPosition = mainCamera.gameObject.transform.position - mainCamera.gameObject.GetComponent<PlayerFollow>().offset;
			Instantiate(item.prefab, dropPosition, Quaternion.identity);
			RemoveItemFromInventory(item);
		}
	}
	
	private void RemoveItemFromInventory(Item item){
		if(Inventory.instance.isSlotSelected(this)){
			Inventory.instance.SetSelectedSlot(null);
		}
		Inventory.instance.Remove(item);
	}
	
	public Item getItem(){
		return item;
	}
	public void setSlotColor(Color32 color){
		ColorBlock colorBlock = transform.GetChild(0).GetComponent<Button>().colors;
		colorBlock.normalColor = color;
		transform.GetChild(0).GetComponent<Button>().colors = colorBlock;
	}
}
