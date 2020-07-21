using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentInfo : MonoBehaviour {

	public Image icon;
	public Text quantityNeededText;
	public Text quantityInInventoryText;
	
	private Image componentBackgroundImg;

    public void AddComponent(Item item, int quantityNeeded, int quantityInInventory) {
		icon.enabled = true;
		icon.sprite = item.icon;
		quantityNeededText.text = quantityNeeded.ToString();
		quantityInInventoryText.text = quantityInInventory.ToString();
		Debug.Log(item + quantityInInventory.ToString() + " >= " + quantityNeeded + " = " + (quantityInInventory >= quantityNeeded));
		enableComponent(quantityInInventory >= quantityNeeded);
    }
    
    private void enableComponent(bool enable){
    	if(enable) {
			getComponentBackgroundImg().color = new Color32(255,255,255,255);    	
    	} else {
			getComponentBackgroundImg().color = new Color32(205,205,205,225);
    	}
    }
    
    private Image getComponentBackgroundImg() {
		if(componentBackgroundImg == null)
			componentBackgroundImg = GetComponent<Image>();
		return componentBackgroundImg;
	}
}
