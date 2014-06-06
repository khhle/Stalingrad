using UnityEngine;
using System.Collections;

public class turnChangeButton : MonoBehaviour {

	private cameraRotationScript came;
	private int turnNumber;

	public GameController gameController;
	// Use this for initialization
	void Start () {
		came = GameObject.Find("Render").GetComponent<cameraRotationScript>();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		 	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseDown()
	{
		//checks if it shoudl still be in placement phase
		if(gameController.russiansLeft ==0 && gameController.germansLeft ==0){
			//checks if they're starting the game after placement
			if (gameController.isPlacement) {
				//change from start game to russians here
				gameController.turnChange (1);
				came.turnNumber = 1;
				gameController.isPlacement = false;
			} else {
				turnNumber = came.turnNumber;
				switch (gameController.playerTurn) {
				case 1:
						gameController.turnChange (2);
					//change to german flag
						break;
				case 2:
						gameController.turnChange (1);
					//change to russian flag
						break;
				}
				if (turnNumber == 1)
						came.turnNumber = 2;
				else if (turnNumber == 2)
						came.turnNumber = 1;
			}
		}
	}
}
