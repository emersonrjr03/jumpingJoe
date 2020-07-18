using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
	[HideInInspector]
	public bool pressed;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerDown(PointerEventData eventData) {
    	pressed = true;
    }
    
    public void OnPointerUp(PointerEventData eventData) {
    	pressed = false;
    }
    
    public void hideButton(){
    	GetComponent<Image>().enabled = false;
    }
    
    public void showButton(){
    	GetComponent<Image>().enabled = true;
    }
}
