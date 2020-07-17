using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public HealthBar healthBar;
	GameController gameController;
	
    // Start is called before the first frame update
    void Start() {
        gameController = GameController.instance;
    }

    
}
