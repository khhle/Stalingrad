using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GUIText playerTurnText;
	public GUIText GameOverText;
	GameObject[] unitCounter;
	public int player1Units = 0;
	public int player2Units = 0;
	public bool isGameOver = false;

	//keeps track of whose turn it is
	public int playerTurn;
	// Use this for initialization
	void Start () {
		//find out how many units there are per player initially to easily keep track of loss
		if(isGameOver && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.R)))
		{
			//restart the game, change this to the unit selection/title scene later
			Application.LoadLevel(0);
		}
		unitCounter = GameObject.FindGameObjectsWithTag ("Unit");
		for (int i = 0; i < unitCounter.Length; i++) {
			if(unitCounter[i].GetComponent<unitStatScript>().playerOwner == 1)
			{
				player1Units += 1;
			}
			else if(unitCounter[i].GetComponent<unitStatScript>().playerOwner == 2)
			{
				player2Units += 1;
			}
		}
		playerTurn = 1;
	}
	
	// Update is called once per frame
	void Update () {
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
		playerTurnText.text = "Player " + playerTurn + "'s turn";
	}

	void GameOver(int winner)
	{
		isGameOver = true;
		GameOverText.text = "Player " + winner + " wins!\n" +
						"Press 'R' to begin a new game";
	}
}
