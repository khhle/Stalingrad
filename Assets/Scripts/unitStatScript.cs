using UnityEngine;
using System.Collections;

public class unitStatScript : MonoBehaviour {

	public int playerOwner;
	public int attack;
	public int defense;

	public GUIText statText;

	//What kind of unit it is, ie tank, plane, general.
	public string unitType;

	//keeps track of how far the unit can move
	public int moves;

	//checks if it's the player's turn to move
	public bool activeTurn = false;

	public GameController gameController;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		statText.transform.position = new Vector2 ((transform.position.x + 8) /16f, (transform.position.y + 5) / 10f);
		if (statText != null)
			statText.text = attack + "         " + defense;

		if(gameController.playerTurn == playerOwner)
		{
			activeTurn = true;
		}
		else
			activeTurn = false;
	}
}
