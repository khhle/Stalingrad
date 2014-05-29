using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GUIText playerTurnText;
	public GUIText GameOverText;
	GameObject[] unitCounter;
	public int player1Units = 0;
	public int player2Units = 0;
	public bool isGameOver = false;

	//keeps track of whose turn it is and if it's the attack step
	public int playerTurn;
	public bool attackStep = false;

	// Use this for initialization
	void Start () {
		//find out how many units there are per player initially to easily keep track of loss
		unitCounter = GameObject.FindGameObjectsWithTag ("Unit");
		for (int i = 0; i < unitCounter.Length; i++) {
			if(unitCounter[i].GetComponent<unitStatScript>().playerOwner == 1)
			{
				player1Units += 1;
				unitCounter[i].GetComponent<unitStatScript>().movesRemaining = unitCounter[i].GetComponent<unitStatScript>().moves;
			}
			else if(unitCounter[i].GetComponent<unitStatScript>().playerOwner == 2)
			{
				player2Units += 1;
				unitCounter[i].GetComponent<unitStatScript>().movesRemaining = unitCounter[i].GetComponent<unitStatScript>().moves;
			}
		}
		playerTurn = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(isGameOver && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.R)))
		{
			//restart the game, change this to the unit selection/title scene later
			Application.LoadLevel(0);
		}
		//Continually check units remaining for accurate counter
		unitCounter = GameObject.FindGameObjectsWithTag ("Unit");
		int temp1Units = 0, temp2Units = 0;
		for (int i = 0; i < unitCounter.Length; i++) {
			if(unitCounter[i].GetComponent<unitStatScript>().playerOwner == 1)
			{
				temp1Units += 1;
			}
			else if(unitCounter[i].GetComponent<unitStatScript>().playerOwner == 2)
			{
				temp2Units += 1;
			}
		}
		player1Units = temp1Units;
		player2Units = temp2Units;
		if (player1Units == 0)
		{
			GameOver (2);
		}
		else if( player2Units == 0)
		{
			GameOver (1);
		}
		if(attackStep){
			playerTurnText.text = "Player " + playerTurn + "'s turn: Attack Phase";
		}
		else{
			playerTurnText.text = "Player " + playerTurn + "'s turn: Movement Phase";
		}
	}

	public void turnChange (int player)
	{
		playerTurn = player;
		unitCounter = GameObject.FindGameObjectsWithTag ("Unit");
		for (int i = 0; i < unitCounter.Length; i++) {
			if(unitCounter[i].GetComponent<unitStatScript>().playerOwner == player)
			{
				unitCounter[i].GetComponent<unitStatScript>().movesRemaining = unitCounter[i].GetComponent<unitStatScript>().moves;
				unitCounter[i].GetComponent<unitStatScript>().hasAttacked = false;
			}
		}
		hideAllMoves ();
		attackStep = false;
	}

	public void hideAllMoves()
	{
		for (int i = 0; i < unitCounter.Length; i++){
			unitCounter[i].GetComponentInChildren<hexMove>().hideMoves();
		}
	}

	public void GameOver(int winner)
	{
		isGameOver = true;
		GameOverText.text = "Player " + winner + " wins!\n" +
						"Press 'R' to begin a new game";
	}
}
