using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWhenCloser : MonoBehaviour
{
	public GameObject objectToHideAndShow;
	
    private void OnTriggerEnter(Collider other){
    	if(other.tag == "Player"){
    		objectToHideAndShow.SetActive(true);
    	}
	}
	public void OnTriggerExit(Collider other){
    	if(other.tag == "Player"){
			objectToHideAndShow.SetActive(false);
		}
	}
}
