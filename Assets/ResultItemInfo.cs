using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultItemInfo : MonoBehaviour {
	public Image icon;
	public Text quantityCanBeMadeText;
	
	private Image resultBackgroundImg;
	private Button resultBackgroundBtn;

	public void AddResult(Item item, int quantityCanBeMade) {
		icon.enabled = true;
		icon.sprite = item.icon;
		quantityCanBeMadeText.text = quantityCanBeMade.ToString();
		
		enableComponent(quantityCanBeMade > 0);
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
