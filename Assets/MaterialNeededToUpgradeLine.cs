using System.Collections;
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
    	craftingUI.AddMaterialToUpgrade(itemNeeded);
    	addToCurrentQuantity(1);
    	checkAndDisableAddMaterialToUpgradeBtn();
    	checkAmountsAndEnableUpgradeButton();
    }
    
    private void checkAmountsAndEnableUpgradeButton(){
    	if(craftingTable.canUpgradeCraftTable()) {
			transform.parent.parent.GetChild(3).GetComponent<Button>().interactable = true;
		} else {
			transform.parent.parent.GetChild(3).GetComponent<Button>().interactable = false;
		}
    }
}
