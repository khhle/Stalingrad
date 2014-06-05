using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	//Russian GameObjects
	public GameObject russianAT, russianBomber, russianCannon, russianFighter, russianSniper, russianSquad, russianT28, russianT34, russianT60;

	//German GameObjects
	public GameObject germanFlak30, germanAT, germanBomber, germanFighter, germanSniper, germanSquad, germanPanther, germanPanzer4, germanWirbelwind;

	//Arrays containing number of each unit that was selected
	private int[] rusUnitAmount = {0, 0, 0, 0, 0, 0, 0, 0, 0};
	private int[] gerUnitAmount = {0, 0, 0, 0, 0, 0, 0, 0, 0};
	private string[] rusName = {"Russian AT", "Russian Bomber", "Russian Cannon", "Russian Fighter", "Russian Sniper", "Russian Squad", "T28", "T34", "T60"};
	private string[] gerName = {"Flak30", "German AT", "German Bomber", "German Fighter", "German Sniper", "German Squad", "Panther", "Panzer4", "Wirbelwind"};

	public GUIText playerTurnText;
	public GUIText GameOverText;
	GameObject[] unitCounter;
	public int player1Units = 0;
	public int player2Units = 0;
	public bool isGameOver = false;
	private bool placement = true; 
	//keeps track of whose turn it is and if it's the attack step
	public int playerTurn;
	public bool attackStep = false;

	public Camera mainCam;

	//placementphase variables
	public bool isPlacement = true;
	public int germansLeft;
	public int russiansLeft;


	// Use this for initialization
	void Start () {
		//find out how many units there are per player initially to easily keep track of loss
		unitCounter = GameObject.FindGameObjectsWithTag ("Unit");
		for (int i = 0; i < unitCounter.Length; i++) {
			unitCounter[i].transform.rotation *= Quaternion.AngleAxis (180, transform.right);
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
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera").camera;
		playerTurn = 1;
		turnChange (playerTurn);
		Invoke ("getCarry", .1f);
	}

	//function that gets data carry values from selection scene into maingame
	void getCarry(){
		GameObject carry = GameObject.Find("Data Carry");
		russiansLeft = 0; 
		germansLeft = 0;
		if(carry != null)
		{
			rusUnitAmount[0] = carry.GetComponent<Data_Carry>().russianAT;
			rusUnitAmount[1] = carry.GetComponent<Data_Carry>().russianBomber;
			rusUnitAmount[2] = carry.GetComponent<Data_Carry>().russianCannon;
			rusUnitAmount[3] = carry.GetComponent<Data_Carry>().russianFighter;
			rusUnitAmount[4] = carry.GetComponent<Data_Carry>().russianSniper;
			rusUnitAmount[5] = carry.GetComponent<Data_Carry>().russianSquad;
			rusUnitAmount[6] = carry.GetComponent<Data_Carry>().t28;
			rusUnitAmount[7] = carry.GetComponent<Data_Carry>().t34;
			rusUnitAmount[8] = carry.GetComponent<Data_Carry>().t60;

			gerUnitAmount[0] = carry.GetComponent<Data_Carry>().flak30;
			gerUnitAmount[1] = carry.GetComponent<Data_Carry>().germanAT;
			gerUnitAmount[2] = carry.GetComponent<Data_Carry>().germanBomber;
			gerUnitAmount[3] = carry.GetComponent<Data_Carry>().germanFighter;
			gerUnitAmount[4] = carry.GetComponent<Data_Carry>().germanSniper;
			gerUnitAmount[5] = carry.GetComponent<Data_Carry>().germanSquad;
			gerUnitAmount[6] = carry.GetComponent<Data_Carry>().panther;
			gerUnitAmount[7] = carry.GetComponent<Data_Carry>().panzer4;
			gerUnitAmount[8] = carry.GetComponent<Data_Carry>().wirbelwind;
		}
		for (int i = 0; i< 9; i++) {
			russiansLeft += rusUnitAmount[i];
		}
		for (int i = 0; i< 9; i++) {
			germansLeft += gerUnitAmount[i];
		}
	}

	void OnGUI(){
		int rusUnitAdded = -1;
		//Russian Units
		for(int i = 0; i < rusUnitAmount.Length; i++){
			if (rusUnitAmount[i] > 0)
				rusUnitAdded++;
			if (rusUnitAmount[i] > 0 && GUI.Button (new Rect(0, 0 + 30*rusUnitAdded, 130, 20), new GUIContent(rusName[i] + " x" + rusUnitAmount[i]))){
				rusUnitAmount[i]--;
				russiansLeft--;
				if(rusName[i] == "Russian AT"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianAT, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "Russian Bomber"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianBomber, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "Russian Cannon"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianCannon, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "Russian Fighter"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianFighter, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "Russian Sniper"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianSniper, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "Russian Squad"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianSquad, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "T28"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianT28, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "T34"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianT34, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "T60"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianT60, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}

				if(rusUnitAmount[i] == 0)
					rusUnitAdded--;
			}
		}

		int gerUnitAdded = -1;
		//German Units
		for(int i = 0; i < gerUnitAmount.Length; i++){
			if (gerUnitAmount[i] > 0)
				gerUnitAdded++;
			if (gerUnitAmount[i] > 0 && GUI.Button (new Rect(830, 0 + 30*gerUnitAdded, 130, 20), new GUIContent(gerName[i]  + " x" + gerUnitAmount[i]))){
				//put prefab to mouse
				gerUnitAmount[i]--;
				germansLeft--;

				if(gerName[i] == "Flak30"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanFlak30, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "German AT"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanAT, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "German Bomber"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanBomber, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "German Fighter"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanFighter, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "German Sniper"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanSniper, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "German Squad"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanSquad, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "Panther"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanPanther, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "Panzer4"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanPanther, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "Wirbelwind"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanWirbelwind, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}

				if(rusUnitAmount[i] == 0)
					gerUnitAdded--;
			}
		}
	}

	bool unitFlag = false;
	GameObject unitt;

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		if(unitt != null){
			if(Input.GetMouseButtonDown(0))
				unitFlag = false;
			if(unitFlag)
				unitt.transform.position = ray.origin + new Vector3(0, 0, .11f);	
			unitt.transform.position =  new Vector3(unitt.transform.position.x, unitt.transform.position.y, 0);
		}

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
		if (player1Units == 0 && !placement)
		{
			GameOver (2);
		}
		else if( player2Units == 0 && !placement)
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
		bool changedplayer;
		if (playerTurn == player)
				changedplayer = false;
		else
				changedplayer = true;
		playerTurn = player;
		unitCounter = GameObject.FindGameObjectsWithTag ("Unit");
		for (int i = 0; i < unitCounter.Length; i++) {
			if(changedplayer){
				unitCounter[i].transform.rotation *= Quaternion.AngleAxis (180, transform.right);
			}
			if(unitCounter[i].GetComponent<unitStatScript>().playerOwner == player)
			{
				unitCounter[i].GetComponent<unitStatScript>().movesRemaining = unitCounter[i].GetComponent<unitStatScript>().moves;
				unitCounter[i].GetComponent<unitStatScript>().hasAttacked = false;
				unitCounter[i].GetComponent<unitStatScript>().picked = true;
			}
			else{
				unitCounter[i].GetComponent<unitStatScript>().picked = false;
			}
			unitCounter[i].GetComponent<unitStatScript>().isMoving = false;
			unitCounter[i].GetComponent<unitStatScript>().isAttacking = false;
		}
		hideAllMoves ();
		attackStep = false;
	}

	public void pickedObject(unitStatScript pickedObject){
		for (int i = 0; i < unitCounter.Length; i++) {
			if(unitCounter[i].GetComponent<unitStatScript>() != pickedObject)
			{
				unitCounter[i].GetComponent<unitStatScript>().picked = false;
			}
		}
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
