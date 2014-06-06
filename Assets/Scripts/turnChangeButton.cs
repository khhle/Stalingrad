using UnityEngine;
using System.Collections;

public class turnChangeButton : MonoBehaviour {

	private cameraRotationScript came;
	private int turnNumber;
	//public Sprite ger;
	public Sprite switchSprite;
	public GameController gameController;
	private Color mouseOverColor = Color.red;
	private Color originalColor ;
	private flagScript flag;
	private GameObject compass;
	// Use this for initialization
	void Start () {
		came = GameObject.Find("Render").GetComponent<cameraRotationScript>();
		flag = GameObject.Find("Flag").GetComponent<flagScript>();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		compass = GameObject.Find ("Compass");
		 	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseEnter()
	{
		originalColor = gameObject.renderer.material.GetColor ("_Color");
		gameObject.renderer.material.color = mouseOverColor;
		
	}
	
	void OnMouseExit()
	{
		gameObject.renderer.material.color = originalColor;
		
	}

	void OnMouseDown()
	{
		//checks if it shoudl still be in placement phase
		if(gameController.russiansLeft ==0 && gameController.germansLeft ==0){
			//checks if they're starting the game after placement
			if (gameController.isPlacement) {
				//change from start game to russians here
				GetComponent<SpriteRenderer>().sprite = switchSprite;
				flag.teamNumber = 1;
				gameController.turnChange (1);
				came.turnNumber = 1;
				gameController.isPlacement = false;
			} else {
				turnNumber = came.turnNumber;
				switch (gameController.playerTurn) {
				case 1:
						gameController.turnChange (2);
					//change to german flag
						//GetComponent<SpriteRenderer>().sprite = ger;
					compass.transform.Rotate(new Vector3(0,0,180));
						flag.teamNumber = 2;
						break;
				case 2:
						gameController.turnChange (1);
					//change to russian flag
					//GetComponent<SpriteRenderer>().sprite = rus;
					compass.transform.Rotate(new Vector3(0,0,180));
					flag.teamNumber = 1;
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
