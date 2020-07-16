using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	GameController gameController;
    // Start is called before the first frame update
    void Start() {
        gameController = GameController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void hitPlayer(int hitValue){
    	gameController.setPlayerHealth(gameController.getPlayerHealth() - hitValue);
    }
}
