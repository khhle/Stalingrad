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
		turnNumber = came.turnNumber;
		switch(gameController.playerTurn)
		{
		case 1:
			gameController.turnChange (2);
			break;
		case 2:
			gameController.turnChange (1);
			break;
		}
		if (turnNumber == 1)
			came.turnNumber = 2;
		else if(turnNumber == 2)
		 	came.turnNumber = 1;
		gameController.hideAllMoves ();
	}
}
