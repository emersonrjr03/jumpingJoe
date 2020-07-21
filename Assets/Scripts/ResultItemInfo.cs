using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultItemInfo : MonoBehaviour {
	public Image icon;
	public Text quantityCanBeMadeText;
	
	private Image resultBackgroundImg;
	private Button resultBackgroundBtn;
	private Item resultItem;
	
	private Inventory inventory;
	private CraftingUI craftingUI;
	void Start() {
		inventory = Inventory.instance;
		craftingUI = transform.root.GetComponent<CraftingUI>();
	}
	public void AddResult(Item item, int quantityCanBeMade) {
		resultItem = item;
		icon.enabled = true;
		icon.sprite = item.icon;
		quantityCanBeMadeText.text = quantityCanBeMade.ToString();
		
		enableComponent(quantityCanBeMade > 0);
	}

	public void craftItem() {
		//Here maybe we can call the craftingUI passing the item, so it can access the inventory to remove the items need to craft the item
		//and add the resultItem to the inventory
		Debug.Log("Crafting Item " + resultItem);
		foreach(Item component in resultItem.components) {
			inventory.Remove(component);
		}
		inventory.Add(resultItem);
		craftingUI.updateItems();
	}
	
    private void enableComponent(bool enable){
    	if(enable) {
    		getResultBackgroundBtn().interactable = true;
			getResultBackgroundImg().color = new Color32(255,255,255,255);
    	} else {
    		getResultBackgroundBtn().interactable = false;
			getResultBackgroundImg().color = new Color32(205,205,205,225);
    	}
    }
    
   	private Image getResultBackgroundImg() {
		if(resultBackgroundImg == null)
			resultBackgroundImg = GetComponent<Image>();
		return resultBackgroundImg;
	}

	private Button getResultBackgroundBtn() {
		if(resultBackgroundBtn == null)
			resultBackgroundBtn = GetComponent<Button>();
		return resultBackgroundBtn;
	}
}
