﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialNeededToUpgradeLine : MonoBehaviour {
    private CraftingUI craftingUI;
    private Item itemNeeded;
    private int quantityNeeded;
    private int currentQuantity;
    private CraftingTable craftingTable;
    private int itemIndex; 
    
    void Start() {
		craftingUI = GameObject.FindGameObjectsWithTag("Main Canvas")[0].GetComponent<CraftingUI>();
    }
    
    public void AddNewLine(CraftingTable craftingTableObj, Item item) {
    	craftingTable = craftingTableObj;
    	itemNeeded = item;
    	itemIndex = System.Array.IndexOf(craftingTable.itemsForNextLevel, itemNeeded);

    	addToQuantityNeededText(craftingTable.quantityForNextLevel[itemIndex]);
    	addToCurrentQuantity(craftingTable.currentQuantityForNextLevel[itemIndex]);

    	transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
    	
    	//if we didn't achieve the needed quantity yet 
    	checkAndDisableAddMaterialToUpgradeBtn();
    }
    
    private void checkAndDisableAddMaterialToUpgradeBtn(){
    	//if we didn't achieve the needed quantity yet 
    	if(currentQuantity < quantityNeeded) {
			transform.GetChild(3).GetComponent<Button>().interactable = true;
    	} else {
			transform.GetChild(3).GetComponent<Button>().interactable = false;    	
    	}
    }
    
    private void addToQuantityNeededText(int addingQuantityNeeded){
    	quantityNeeded += addingQuantityNeeded;
   		transform.GetChild(1).GetComponent<Text>().text = quantityNeeded.ToString();
    }
    
    private void addToCurrentQuantity(int addingQuantity){
    	currentQuantity += addingQuantity;
    	transform.GetChild(2).GetComponent<Text>().text = currentQuantity.ToString();
    }
    
    public void addMaterialToUpgrade(){
    	if(craftingUI.AddMaterialToUpgrade(itemNeeded)) {
			addToCurrentQuantity(1);
			checkAndDisableAddMaterialToUpgradeBtn();
			checkAmountsAndEnableUpgradeButton();
    	} else {
			showMsg("Not enougth materials");    	
    	}
    }
    
    void showMsg(string msg) {
		transform.parent.parent.GetChild(4).GetChild(0).GetComponent<Text>().text = msg;
		transform.parent.parent.GetChild(4).gameObject.SetActive(true);
		CanvasGroup cvGroup = transform.parent.parent.GetChild(4).GetComponent<CanvasGroup>();
		cvGroup.alpha = 1;
        StartCoroutine(disableMsg(3));
    }
    
	IEnumerator disableMsg(int sec) {
		CanvasGroup cvGroup = transform.parent.parent.GetChild(4).GetComponent<CanvasGroup>();

		yield return doFade(cvGroup, cvGroup.alpha, 0, sec);

		transform.parent.parent.GetChild(4).gameObject.SetActive(false);
	}
	
	IEnumerator doFade(CanvasGroup canvGroup, float start, float end, float duration) {
		float counter = 0f;
		while(counter < duration) {
			counter += Time.deltaTime;
			canvGroup.alpha = Mathf.Lerp(start, end, counter / duration);
			yield return null;
		}
	}
     
    private void checkAmountsAndEnableUpgradeButton(){
    	if(craftingTable.canUpgradeCraftTable()) {
			transform.parent.parent.GetChild(3).GetComponent<Button>().interactable = true;
		} else {
			transform.parent.parent.GetChild(3).GetComponent<Button>().interactable = false;
		}
    }
}
