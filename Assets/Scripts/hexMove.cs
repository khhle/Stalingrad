using UnityEngine;
using System.Collections;

public class hexMove : MonoBehaviour {
	private Color mouseOverColor = Color.cyan;
	private Color originalColor ;
	public bool isAvail = false ;
	private GameController gameController;
	private unitStatScript parentStats;

	//List of Russian Units
	GameObject unit_t28, unit_t34, unit_t60, unit_RussianSniper, unit_RussianSquad, unit_RussianAT, unit_RussianBomber, unit_RussianCannon, unit_RussianFighter;
	
	//List of German Units
	GameObject unit_Flak30, unit_GermanAT, unit_GermanBomber, unit_GermanFighter, unit_panther, unit_Wirbelwind, unit_GermanSniper, unit_GermanSquad, unit_Panzer4;
	



	void Start () {

		//initialize russian units
		unit_RussianAT = GameObject.Find ("unit_RussianAT");
		unit_RussianBomber = GameObject.Find ("unit_RussianBomber");
		unit_RussianCannon = GameObject.Find ("unit_RussianCannon");
		unit_RussianFighter = GameObject.Find ("unit_RussianFighter");
		unit_RussianSniper = GameObject.Find ("unit_RussianSniper");
		unit_RussianSquad = GameObject.Find ("unit_RussianSquad");
		unit_t28 = GameObject.Find("unit_t-28");
		unit_t34 = GameObject.Find("unit_t-34");
		unit_t60 = GameObject.Find("unit_t-60");
		
		//initialize german units
		unit_Flak30 = GameObject.Find ("unit_Flak30");
		unit_GermanAT = GameObject.Find ("unit_GermanAT");
		unit_GermanBomber = GameObject.Find ("unit_GermanBomber");
		unit_GermanFighter = GameObject.Find ("unit_GermanFighter");
		unit_GermanSniper = GameObject.Find ("unit_GermanSniper");
		unit_GermanSquad = GameObject.Find ("unit_GermanSquad");
		unit_panther = GameObject.Find ("unit_panther");
		unit_Panzer4 = GameObject.Find ("unit_Panzer4");
		unit_Wirbelwind = GameObject.Find ("unit_Wirbelwind");

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		parentStats = transform.parent.GetComponent<unitStatScript> ();
 	}
	void Update()
	{
		if (parentStats.isMoving == false)
			parentStats.isClicked = false;
		if(transform.parent.GetComponent<unitStatScript>().movesRemaining <= 0){
			parentStats.isMoving = false;
		}
	}

	
	void OnMouseDown()
	{
		if( parentStats.playerOwner == gameController.playerTurn)
		{
			foreach (Transform children in transform) {
				Transform thisChild = children;
				foreach (Transform child in thisChild) {
					moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
					//red_hex redhex = child.GetComponent<red_hex> ();
					if (movObj.isClick && movObj.grandParentStats.picked)
					{
						//if you click on the unit while in movement phase, remove skip button
						parentStats.isMoving = false;
						//if you click on the unit while in attack phase, remove skip button
						parentStats.isAttacking = false;

						movObj.isClick = false;
						movObj.recentlyClicked = true;
						//redhex.isClick = false;
					}
					else if(movObj.grandParentStats.picked)
					{
						//checks to see if it has any moves remaining
						if(transform.parent.GetComponent<unitStatScript>().movesRemaining > 0 && !gameController.attackStep){ //&& movObj.isGreen){

							parentStats.isClicked = true;
							OnMouseExit ();

							//if you click on unit in movement phase give the option for the skip
							parentStats.isMoving = true;

							if(movObj.isGreen || (movObj.transform.parent.GetComponent<ringScript>().rangeValue == parentStats.range && parentStats.range != 1)){
								movObj.isClick = true;
								movObj.isInit = false;
								movObj.isInitCollide = false;
								movObj.isCollide = false;
							}

						}
						else if(gameController.attackStep && !movObj.isGreen && !parentStats.hasAttacked){
							//if the button hasn't been pressed to skip move, hide the button
							parentStats.isMoving = false;

							//if you click on the unit in attack phase give the option for the skip
							parentStats.isAttacking = true;
							
							movObj.isClick = true;
							movObj.isInit = false;
							movObj.isInitCollide = false;
							movObj.isCollide = false;
						}
					}
				}
			}
		}


	}

	//use this for hiding the green hex objects.
	public void hideMoves()
	{
		foreach (Transform children in transform) {
			Transform thisChild = children;
			foreach (Transform child in thisChild) {
				moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
				//red_hex redhex = child.GetComponent<red_hex> ();
				movObj.isClick = false;
				movObj.recentlyClicked = true;
				//redhex.isClick = false;
			}
		}
	}

	//mouse hover overs unit
	void OnMouseEnter() {
		switch (parentStats.id){
			//Russian Units
			case 0:
				if(!parentStats.isClicked)
					unit_RussianAT.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 1:
				if(!parentStats.isClicked)
					unit_RussianBomber.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 2:
				if(!parentStats.isClicked)
					unit_RussianCannon.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 3:
				if(!parentStats.isClicked)
					unit_RussianFighter.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 4:
				if(!parentStats.isClicked)
					unit_RussianSniper.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 5:
				if(!parentStats.isClicked)
					unit_RussianSquad.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 6:
				if(!parentStats.isClicked)
					unit_t28.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 7:
				if(!parentStats.isClicked)
					unit_t34.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 8:
				if(!parentStats.isClicked)
					unit_t60.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			//German Units
			case 9:
				if(!parentStats.isClicked)
					unit_Flak30.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 10:
				if(!parentStats.isClicked)
					unit_GermanAT.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 11:
				if(!parentStats.isClicked)
					unit_GermanBomber.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 12:
				if(!parentStats.isClicked)
					unit_GermanFighter.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 13:
				if(!parentStats.isClicked)
					unit_GermanSniper.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 14:
				if(!parentStats.isClicked)
					unit_GermanSquad.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 15:
				if(!parentStats.isClicked)
					unit_panther.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 16:
				if(!parentStats.isClicked)
					unit_Panzer4.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			case 17:
				if(!parentStats.isClicked)
					unit_Wirbelwind.transform.position = new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height + .275f, -3);
				break;
			default:
				break;
		}
	}
	
	//mouse exits hover
	void OnMouseExit() {
		switch (parentStats.id){
			case 0:
				unit_RussianAT.transform.position = new Vector3(10, .345f, -3);
				break;
			case 1:
				unit_RussianBomber.transform.position = new Vector3(10, .345f, -3);
				break;
			case 2:
				unit_RussianCannon.transform.position = new Vector3(10, .345f, -3);
				break;
			case 3:
				unit_RussianFighter.transform.position = new Vector3(10, .345f, -3);
				break;
			case 4:
				unit_RussianSniper.transform.position = new Vector3(10, .345f, -3);
				break;
			case 5:
				unit_RussianSquad.transform.position = new Vector3(10, .345f, -3);
				break;
			case 6:
				unit_t28.transform.position = new Vector3(10, .345f, -3);
				break;
			case 7:
				unit_t34.transform.position = new Vector3(10, .345f, -3);
				break;
			case 8:
				unit_t60.transform.position = new Vector3(10, .345f, -3);
				break;

			case 9:
				unit_Flak30.transform.position = new Vector3(10, .345f, -3);
				break;
			case 10:
				unit_GermanAT.transform.position = new Vector3(10, .345f, -3);
				break;
			case 11:
				unit_GermanBomber.transform.position = new Vector3(10, .345f, -3);
				break;
			case 12:
				unit_GermanFighter.transform.position = new Vector3(10, .345f, -3);
				break;
			case 13:
				unit_GermanSniper.transform.position = new Vector3(10, .345f, -3);
				break;
			case 14:
				unit_GermanSquad.transform.position = new Vector3(10, .345f, -3);
				break;
			case 15:
				unit_panther.transform.position = new Vector3(10, .345f, -3);
				break;
			case 16:
				unit_Panzer4.transform.position = new Vector3(10, .345f, -3);
				break;
			case 17:
				unit_Wirbelwind.transform.position = new Vector3(10, .345f, -3);
				break;

			default:
				break;
		}

	}


}
