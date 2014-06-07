using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	//Russian GameObjects
	public GameObject russianAT, russianBomber, russianCannon, russianFighter, russianSniper, russianSquad, russianT28, russianT34, russianT60;

	//German GameObjects
	public GameObject germanFlak30, germanAT, germanBomber, germanFighter, germanSniper, germanSquad, germanPanther, germanPanzer4, germanWirbelwind;

	public GameObject ger_warning, rus_warning;
	//Arrays containing number of each unit that was selected
	private int[] rusUnitAmount = {0, 0, 0, 0, 0, 0, 0, 0, 0};
	private int[] gerUnitAmount = {0, 0, 0, 0, 0, 0, 0, 0, 0};
	private string[] rusName = {"RUS AT Infantry", "RUS Bomber", "RUS AA Infantry", "RUS Fighter", "RUS Sniper", "RUS Assault Infantry", "RUS HE Tank", "RUS AP Tank", "RUS AA Tank"};
	private string[] gerName = {"GER AT Infantry", "GER Bomber", "GER AA Infantry", "GER Fighter", "GER Sniper", "GER Assault Infantry", "GER AP Tank", "GER HE Tank", "GER AA Tank"};

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
			rusUnitAmount[0] = carry.GetComponent<Data_Carry>().russianAT*2;
			rusUnitAmount[1] = carry.GetComponent<Data_Carry>().russianBomber;
			rusUnitAmount[2] = carry.GetComponent<Data_Carry>().russianCannon;
			rusUnitAmount[3] = carry.GetComponent<Data_Carry>().russianFighter;
			rusUnitAmount[4] = carry.GetComponent<Data_Carry>().russianSniper;
			rusUnitAmount[5] = carry.GetComponent<Data_Carry>().russianSquad*3;
			rusUnitAmount[6] = carry.GetComponent<Data_Carry>().t28;
			rusUnitAmount[7] = carry.GetComponent<Data_Carry>().t34;
			rusUnitAmount[8] = carry.GetComponent<Data_Carry>().t60;

			gerUnitAmount[0] = carry.GetComponent<Data_Carry>().germanAT;
			gerUnitAmount[1] = carry.GetComponent<Data_Carry>().germanBomber;
			gerUnitAmount[2] = carry.GetComponent<Data_Carry>().flak30;
			gerUnitAmount[3] = carry.GetComponent<Data_Carry>().germanFighter;
			gerUnitAmount[4] = carry.GetComponent<Data_Carry>().germanSniper;
			gerUnitAmount[5] = carry.GetComponent<Data_Carry>().germanSquad*2;
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

	private bool selecting = false;

	void OnGUI(){
		int rusUnitAdded = -1;
		//Russian Units
		for(int i = 0; i < rusUnitAmount.Length; i++){
			if (rusUnitAmount[i] > 0)
				rusUnitAdded++;
			if (!selecting && rusUnitAmount[i] > 0 && GUI.Button (new Rect(785, 40 + 40*rusUnitAdded, 160, 30), new GUIContent(rusName[i] + " x" + rusUnitAmount[i]))){
				rusUnitAmount[i]--;
				russiansLeft--;
				placed_unit = false;
				selecting = true;
				if(rusName[i] == "RUS AT Infantry"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianAT, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "RUS Bomber"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianBomber, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "RUS AA Infantry"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianCannon, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "RUS Fighter"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianFighter, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "RUS Sniper"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianSniper, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "RUS Assault Infantry"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianSquad, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "RUS HE Tank"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianT28, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "RUS AP Tank"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(russianT34, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(rusName[i] == "RUS AA Tank"){
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
			if (!selecting && gerUnitAmount[i] > 0 && GUI.Button (new Rect(13, 28 + 40*gerUnitAdded, 160, 30), new GUIContent(gerName[i]  + " x" + gerUnitAmount[i]))){
				//put prefab to mouse
				gerUnitAmount[i]--;
				germansLeft--;
				placed_unit = false;
				selecting = true;
				if(gerName[i] == "GER AA Infantry"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanFlak30, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "GER AT Infantry"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanAT, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "GER Bomber"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanBomber, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "GER Fighter"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanFighter, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "GER Sniper"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanSniper, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "GER Assault Infantry"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanSquad, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "GER AP Tank"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanPanther, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "GER HE Tank"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanPanzer4, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}
				else if(gerName[i] == "GER AA Tank"){
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
					unitt = Instantiate(germanWirbelwind, ray.origin + new Vector3(0, 0, 1f), Quaternion.identity) as GameObject;
					unitFlag = true;
				}


				if(gerUnitAmount[i] == 0)
					gerUnitAdded--;
			}
		}
	}

	bool unitFlag = false;
	GameObject unitt;
	void rusWarningOff(){
		rus_warning.guiText.enabled = false;
	}
	void gerWarningOff(){
		ger_warning.guiText.enabled = false;
	}

	bool placed_unit = false;
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		if(unitt != null){
			if(Input.GetMouseButtonDown(0))
			{
				if(unitt.transform.GetComponent<unitStatScript>().playerOwner == 1 && ray.origin.x >= 6.6)
				{
					unitFlag = false;
					placed_unit = true;
					selecting = false;
				}
				else if(unitt.transform.GetComponent<unitStatScript>().playerOwner == 2 && ray.origin.x <= -6.6)
				{
					unitFlag = false;
					placed_unit = true;
					selecting = false;
				}else if(unitt.transform.GetComponent<unitStatScript>().playerOwner == 1 && ray.origin.x < 6.6 ){
					//if russians try to place too to the left
					if(!placed_unit){
						rus_warning.guiText.enabled = true;
						Invoke("rusWarningOff", 1f);
					}
				}else if(unitt.transform.GetComponent<unitStatScript>().playerOwner == 2 && ray.origin.x > -6.6){
					//if germans try to place too to the right
					if(!placed_unit){
						ger_warning.guiText.enabled = true;
						Invoke("gerWarningOff", 1f);
					}
				}

			}
			if(unitFlag){
				unitt.transform.position = ray.origin + new Vector3(0, 0, .11f);	
			}
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
		if (player1Units == 0 && !isPlacement)
		{
			GameOver (2);
		}
		else if( player2Units == 0 && !isPlacement)
		{
			GameOver (1);
		}
		// settingphase text
		if (isPlacement) {
			playerTurnText.text = "Placement Phase: Nazis place in the West, Soviets in the East";
		}
		else if(attackStep){
			playerTurnText.text = "Attack Phase";
		}
		else{
			playerTurnText.text = "Movement Phase";
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
