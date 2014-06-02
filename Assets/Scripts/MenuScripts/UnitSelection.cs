using UnityEngine;
using System.Collections;

public class UnitSelection : MonoBehaviour {

	//List of GameObject sprites
	GameObject data_Carry;

	//List of Russian Units
	GameObject unit_t28, unit_t34, unit_t60, unit_RussianSniper, unit_RussianSquad, unit_RussianAT, unit_RussianBomber, unit_RussianCannon, unit_RussianFighter;

	//List of German Units
	GameObject unit_Flak30, unit_GermanAT, unit_GermanBomber, unit_GermanFighter, unit_panther, unit_Wirbelwind, unit_GermanSniper, unit_GermanSquad, unit_Panzer4;

	//List of GuiTexts
	GameObject GT_battleBegin, GT_insuff, GT_p1_Credits_Remaining, GT_p2_Credits_Remaining, GT_player1, GT_player2;

	//Credits for how many units you can buy
	public int player1_Credits, player2_Credits;

	//List of Audio sources
	public AudioClip insufficientFunds;
	public AudioClip sufficientFunds;
	public AudioClip hoverOver;


	//An array used to check the purchased units
	private bool[] unitsPurchased = {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
	private bool[] unitIconMade = {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
	private static int[] amountPurchased = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
	private string[] lastTip = {" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "};
	private bool isPlayer1 = true;
	private bool isPlayer2 = false;
	private int yCoordSelectionP1 = 100;
	private int yCoordSelectionP2 = 100;

	//offscreen1 = Purchasable buttons that are only adjusted in the update based on player's turn
	//offscreen2 = nonclickable hover-over stats button that are adjusted only in the onGui. 
	//Russian Units
	Rect RussianAT_offscreen1 = new Rect (1000, 75, 70, 40);//0
	Rect RussianAT_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect RussianBomber_offscreen1 = new Rect (1000, 75, 70, 40);//1
	Rect RussianBomber_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect RussianCannon_offscreen1 = new Rect (1000, 75, 70, 40);//2
	Rect RussianCannon_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect RussianFighter_offscreen1 = new Rect (1000, 75, 70, 40);//3
	Rect RussianFighter_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect RussianSniper_offscreen1 = new Rect (1000, 75, 70, 40);//4
	Rect RussianSniper_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect RussianSquad_offscreen1 = new Rect (1000, 75, 70, 40);//5
	Rect RussianSquad_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect t28_offscreen1 = new Rect (1000, 75, 70, 40);//6
	Rect t28_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect t34_offscreen1 = new Rect (1000, 75, 70, 40);//7
	Rect t34_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect t60_offscreen1 = new Rect (1000, 75, 70, 40);//8
	Rect t60_offscreen2 = new Rect (1000, 75, 70, 40);

	//German Units
	Rect Flak30_offscreen1 = new Rect (1000, 75, 70, 40);//9
	Rect Flak30_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect GermanAT_offscreen1 = new Rect (1000, 75, 70, 40);//10
	Rect GermanAT_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect GermanBomber_offscreen1 = new Rect (1000, 75, 70, 40);//11
	Rect GermanBomber_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect GermanFighter_offscreen1 = new Rect (1000, 75, 70, 40);//12
	Rect GermanFighter_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect GermanSniper_offscreen1 = new Rect (1000, 75, 70, 40);//13
	Rect GermanSniper_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect GermanSquad_offscreen1 = new Rect (1000, 75, 70, 40);//14
	Rect GermanSquad_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect panther_offscreen1 = new Rect (1000, 75, 70, 40);//15
	Rect panther_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect Panzer4_offscreen1 = new Rect (1000, 75, 70, 40);//16
	Rect Panzer4_offscreen2 = new Rect (1000, 75, 70, 40);
	Rect Wirbelwind_offscreen1 = new Rect (1000, 75, 70, 40);//17
	Rect Wirbelwind_offscreen2 = new Rect (1000, 75, 70, 40);


	// Use this for initialization
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

		data_Carry = GameObject.Find("Data Carry");
		GT_p1_Credits_Remaining = GameObject.Find ("p1 credits remaining");
		GT_p2_Credits_Remaining = GameObject.Find ("p2 credits remaining");
		GT_player1 = GameObject.Find ("Player1Pick");
		GT_player2 = GameObject.Find ("Player2Pick");
		GT_insuff = GameObject.Find ("Insufficient Funds");
		GT_battleBegin = GameObject.Find ("Battle Begins");
	}

	//Used for when game is starting to display a guiText
	private int countdownTime = 5;
	private int frames = 0;

	// Update is called once per frame
	void Update () {

		//updates any guiTexts that need updating
		setText ();

		//3 if statements for guiText starting the game cool style!
		if(GT_battleBegin.guiText.enabled == true){
			frames++;
		}
		if(countdownTime == 0){
			Application.LoadLevel("s1");
		}
		if(frames == 60){
			countdownTime--;
			frames = 0;
		}


		if (isPlayer1){
			RussianAT_offscreen1 = new Rect(310, 75, 110, 40);
			RussianBomber_offscreen1 = new Rect(425, 75, 110, 40);
			RussianCannon_offscreen1 = new Rect(540, 75, 110, 40);
			RussianFighter_offscreen1 = new Rect(310, 135, 110, 40);
			RussianSniper_offscreen1 = new Rect(425, 135, 110, 40);
			RussianSquad_offscreen1 = new Rect(540, 135, 110, 40);
			t28_offscreen1 = new Rect (310, 195, 110, 40);
			t34_offscreen1 = new Rect (425, 195, 110, 40);
			t60_offscreen1 = new Rect (540, 195, 110, 40);

			Flak30_offscreen1 = new Rect (1000, 75, 110, 40);
			GermanAT_offscreen1 = new Rect (1000, 75, 110, 40);
			GermanBomber_offscreen1 = new Rect (1000, 75, 110, 40);
			GermanFighter_offscreen1 = new Rect (1000, 75, 110, 40);
			GermanSniper_offscreen1 = new Rect (1000, 75, 110, 40);
			GermanSquad_offscreen1 = new Rect (1000, 75, 110, 40);
			panther_offscreen1 = new Rect (1000, 75, 110, 40);
			Panzer4_offscreen1 = new Rect (1000, 75, 110, 40);
			Wirbelwind_offscreen1 = new Rect (1000, 75, 110, 40);

			GT_player1.renderer.material.color = Color.green;
			GT_player2.renderer.material.color = Color.red;
		}
		if (isPlayer2) {
			Flak30_offscreen1 = new Rect(310, 75, 110, 40);
			GermanAT_offscreen1 = new Rect(425, 75, 110, 40);
			GermanBomber_offscreen1 = new Rect(540, 75, 110, 40);
			GermanFighter_offscreen1 = new Rect(310, 135, 110, 40);
			GermanSniper_offscreen1 = new Rect(425, 135, 110, 40);
			GermanSquad_offscreen1 = new Rect(540, 135, 110, 40);
			panther_offscreen1 = new Rect (310, 195, 110, 40);
			Panzer4_offscreen1 = new Rect (425, 195, 110, 40);
			Wirbelwind_offscreen1 = new Rect (540, 195, 110, 40);

			RussianAT_offscreen1 = new Rect(1000, 75, 110, 40);
			RussianBomber_offscreen1 = new Rect(1000, 75, 110, 40);
			RussianCannon_offscreen1 = new Rect(1000, 75, 110, 40);
			RussianFighter_offscreen1 = new Rect(1000, 75, 110, 40);
			RussianSniper_offscreen1 = new Rect(1000, 75, 110, 40);
			RussianSquad_offscreen1 = new Rect(1000, 75, 110, 40);
			t28_offscreen1 = new Rect (1000, 75, 110, 40);
			t34_offscreen1 = new Rect (1000, 75, 110, 40);
			t60_offscreen1 = new Rect (1000, 75, 110, 40);

			GT_player2.renderer.material.color = Color.green;
			GT_player1.renderer.material.color = Color.red;
		}

		if(player1_Credits == 0 && player2_Credits == 0){
			GT_battleBegin.guiText.enabled = true;
		}

	}

	bool selectUnit(int i){
		switch (i) {
			//Russian Units
			//RussianAT purchased
			case 0:
				if (player1_Credits-7500 >= 0) {
					data_Carry.GetComponent<Data_Carry>().russianAT++;
					player1_Credits -= 7500;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;
			//RussianBomber purchased
			case 1:
				if (player1_Credits-7500 >= 0) {
					data_Carry.GetComponent<Data_Carry>().russianBomber++;
					player1_Credits -= 7500;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;
			//RussianCannon purchased
			case 2:
				if (player1_Credits-7500 >= 0) {
					data_Carry.GetComponent<Data_Carry>().russianCannon++;
					player1_Credits -= 7500;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;
			//RussianFighter purchased
			case 3:
				if (player1_Credits-7500 >= 0) {
					data_Carry.GetComponent<Data_Carry>().russianFighter++;
					player1_Credits -= 7500;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;
			//RussianSniper purchased
			case 4:
				if (player1_Credits-2500 >= 0) {
					data_Carry.GetComponent<Data_Carry>().russianSniper++;
					player1_Credits -= 2500;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;
			//RussianSquad purchased
			case 5:
				if (player1_Credits-7500 >= 0) {
					data_Carry.GetComponent<Data_Carry>().russianSquad++;
					player1_Credits -= 7500;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;
			//t28 tank purchased
			case 6:
				if (player1_Credits-7500 >= 0) {
					data_Carry.GetComponent<Data_Carry>().t28++;
					player1_Credits -= 7500;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;
			//t34 tank purchased
			case 7:
				if (player1_Credits-5000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().t34++;
					player1_Credits -= 5000;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;
			//t60 tank purchased
			case 8:
				if (player1_Credits-5000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().t60++;
					player1_Credits -= 5000;
					increment(i);
					if(player2_Credits > 0){
						isPlayer1 = false;
						isPlayer2 = true;
					}
					return true;
				}	
				break;

			//German Units
			//German AA turret purchased
			case 9:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().flak30++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			//German anti tank infantry purchased
			case 10:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().germanAT++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			//German bomber plane purchased
			case 11:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().germanBomber++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			//German fighter plane purchased
			case 12:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().germanFighter++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			//German sniper purchased
			case 13:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().germanSniper++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			//German infantry squad purchased
			case 14:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().germanSquad++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			//German tank good against other tanks purchased
			case 15:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().panther++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			//German tank good against infantry purchased
			case 16:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().panzer4++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			//wirbelwind purchased (german AA tank)
			case 17:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().wirbelwind++;
					player2_Credits -= 10000;
					increment(i);
					if(player1_Credits > 0){
						isPlayer1 = true;
						isPlayer2 = false;
					}
					return true;
				}	
			break;
			default:
				break;
		}
		//Could not afford the unit clicked on
		GT_insuff.guiText.enabled = true;
		Invoke ("disable", 1.0f);
		audio.PlayOneShot (insufficientFunds);
		return false;
	}

	void disable(){
		GT_insuff.guiText.enabled = false;
	}

	//Function that basically means unit was able to be purchased with sufficient funds
	void increment(int i){
		audio.PlayOneShot (sufficientFunds);
		if(unitsPurchased[i] == false){
			unitsPurchased[i] = true;
			amountPurchased[i]++;
			if (i < 9)
				yCoordSelectionP1 += 30;
			else
				yCoordSelectionP2 += 30;
		}
		else {
			//incremement guiText variable (ie x2, x3, x4)
			amountPurchased[i]++;
		}
	}
	//Function for hovering and clicking buttons, isHovering is a flag for this function only
	void OnGUI () {
		//Russian Units
		// Make the RussianAT button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (RussianAT_offscreen1, new GUIContent("Russian AT\n$7500", "RussianAT"))){
			if(selectUnit(0) && unitIconMade[0] == false){
				RussianAT_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[0] = true;
			}
		}
		GUI.Button(RussianAT_offscreen2, new GUIContent("Russian AT x" + amountPurchased[0], "RussianAT"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[0]) {
			if(GUI.tooltip == "RussianAT"){
				unit_RussianAT.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[0] != ""){
				unit_RussianAT.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[0] = GUI.tooltip;
		}


		// Make the RussianBomber button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (RussianBomber_offscreen1, new GUIContent("Russian Bomber\n$7500", "RussianBomber"))){
			if(selectUnit(1) && unitIconMade[1] == false){
				RussianBomber_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[1] = true;
			}
		}
		GUI.Button(RussianBomber_offscreen2, new GUIContent("Russian Bomber x" + amountPurchased[1], "RussianBomber"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[1]) {
			if(GUI.tooltip == "RussianBomber"){
				unit_RussianBomber.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[1] != ""){
				unit_RussianBomber.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[1] = GUI.tooltip;
		}


		// Make the RussianCannon button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (RussianCannon_offscreen1, new GUIContent("Russian Cannon\n$7500", "RussianCannon"))){
			if(selectUnit(2) && unitIconMade[2] == false){
				RussianCannon_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[2] = true;
			}
		}
		GUI.Button(RussianCannon_offscreen2, new GUIContent("Russian Cannon x" + amountPurchased[2], "RussianCannon"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[2]) {
			if(GUI.tooltip == "RussianCannon"){
				unit_RussianCannon.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[2] != ""){
				unit_RussianCannon.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[2] = GUI.tooltip;
		}


		// Make the RussianFighter button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (RussianFighter_offscreen1, new GUIContent("Russian Fighter\n$7500", "RussianFighter"))){
			if(selectUnit(3) && unitIconMade[3] == false){
				RussianFighter_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[3] = true;
			}
		}
		GUI.Button(RussianFighter_offscreen2, new GUIContent("Russian Fighter x" + amountPurchased[3], "RussianFighter"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[3]) {
			if(GUI.tooltip == "RussianFighter"){
				unit_RussianFighter.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[3] != ""){
				unit_RussianFighter.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[3] = GUI.tooltip;
		}

		// Make the RussianSniper button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (RussianSniper_offscreen1, new GUIContent("Russian Sniper\n$2500", "RussianSniper"))){
			if(selectUnit(4) && unitIconMade[4] == false){
				RussianSniper_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[4] = true;
			}
		}
		GUI.Button(RussianSniper_offscreen2, new GUIContent("Russian Sniper x" + amountPurchased[4], "RussianSniper"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[4]) {
			if(GUI.tooltip == "RussianSniper"){
				unit_RussianSniper.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[4] != ""){
				unit_RussianSniper.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[4] = GUI.tooltip;
		}

		// Make the RussianSquad button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (RussianSquad_offscreen1, new GUIContent("Russian Squad\n$7500", "RussianSquad"))){
			if(selectUnit(5) && unitIconMade[5] == false){
				RussianSquad_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[5] = true;
			}
		}
		GUI.Button(RussianSquad_offscreen2, new GUIContent("Russian Squad x" + amountPurchased[5], "RussianSquad"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[5]) {
			if(GUI.tooltip == "RussianSquad"){
				unit_RussianSquad.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[5] != ""){
				unit_RussianSquad.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[5] = GUI.tooltip;
		}

		// Make the t28 button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (t28_offscreen1, new GUIContent("T-28\n$7500", "t28"))){
			if(selectUnit(6) && unitIconMade[6] == false){
				t28_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[6] = true;
			}
		}
		GUI.Button(t28_offscreen2, new GUIContent("T-28 x" + amountPurchased[6], "t28"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[6]) {
			if(GUI.tooltip == "t28"){
				unit_t28.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[6] != ""){
				unit_t28.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[6] = GUI.tooltip;
		}


		// Make the t34 button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (t34_offscreen1, new GUIContent("T-34\n$5000", "t34"))){
			if(selectUnit(7) && unitIconMade[7] == false){
				t34_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[7] = true;
			}
		}
		GUI.Button(t34_offscreen2, new GUIContent("T-34 x" + amountPurchased[7], "t34"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[7]) {
			if(GUI.tooltip == "t34"){
				unit_t34.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[7] != ""){
				unit_t34.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[7] = GUI.tooltip;
		}


		// Make the t60 button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (t60_offscreen1, new GUIContent("T-60\n$5000", "t60"))){
			if(selectUnit(8) && unitIconMade[8] == false){
				t60_offscreen2 = new Rect(60, yCoordSelectionP1, 130, 20);
				unitIconMade[8] = true;
			}
		}
		GUI.Button(t60_offscreen2, new GUIContent("T-60 x" + amountPurchased[8], "t60"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[8]) {
			if(GUI.tooltip == "t60"){
				unit_t60.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[8] != ""){
				unit_t60.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[8] = GUI.tooltip;
		}


		//German Units
		//Make the Flak30 button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (Flak30_offscreen1, new GUIContent("Flak30\n$10000", "Flak30"))){
			if(selectUnit(9) && unitIconMade[9] == false){
				Flak30_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[9] = true;
			}
		}
		GUI.Button(Flak30_offscreen2, new GUIContent("Flak30 x" + amountPurchased[9], "Flak30"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[9]) {
			if(GUI.tooltip == "Flak30"){
				unit_Flak30.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[9] != ""){
				unit_Flak30.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[9] = GUI.tooltip;
		}


		//Make the GermanAT button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (GermanAT_offscreen1, new GUIContent("German AT\n$10000", "GermanAT"))){
			if(selectUnit(10) && unitIconMade[10] == false){
				GermanAT_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[10] = true;
			}
		}
		GUI.Button(GermanAT_offscreen2, new GUIContent("German AT x" + amountPurchased[10], "GermanAT"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[10]) {
			if(GUI.tooltip == "GermanAT"){
				unit_GermanAT.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[10] != ""){
				unit_GermanAT.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[10] = GUI.tooltip;
		}


		//Make the GermanBomber button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (GermanBomber_offscreen1, new GUIContent("German Bomber\n$10000", "GermanBomber"))){
			if(selectUnit(11) && unitIconMade[11] == false){
				GermanBomber_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[11] = true;
			}
		}
		GUI.Button(GermanBomber_offscreen2, new GUIContent("German Bomber x" + amountPurchased[11], "GermanBomber"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[11]) {
			if(GUI.tooltip == "GermanBomber"){
				unit_GermanBomber.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[11] != ""){
				unit_GermanBomber.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[11] = GUI.tooltip;
		}


		//Make the GermanFighter button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (GermanFighter_offscreen1, new GUIContent("German Fighter\n$10000", "GermanFighter"))){
			if(selectUnit(12) && unitIconMade[12] == false){
				GermanFighter_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[12] = true;
			}
		}
		GUI.Button(GermanFighter_offscreen2, new GUIContent("German Fighter x" + amountPurchased[12], "GermanFighter"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[12]) {
			if(GUI.tooltip == "GermanFighter"){
				unit_GermanFighter.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[12] != ""){
				unit_GermanFighter.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[12] = GUI.tooltip;
		}


		//Make the GermanSniper button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (GermanSniper_offscreen1, new GUIContent("German Sniper\n$10000", "GermanSniper"))){
			if(selectUnit(13) && unitIconMade[13] == false){
				GermanSniper_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[13] = true;
			}
		}
		GUI.Button(GermanSniper_offscreen2, new GUIContent("German Sniper x" + amountPurchased[13], "GermanSniper"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[13]) {
			if(GUI.tooltip == "GermanSniper"){
				unit_GermanSniper.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[13] != ""){
				unit_GermanSniper.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[13] = GUI.tooltip;
		}


		//Make the GermanSquad button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (GermanSquad_offscreen1, new GUIContent("German Squad\n$10000", "GermanSquad"))){
			if(selectUnit(14) && unitIconMade[14] == false){
				GermanSquad_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[14] = true;
			}
		}
		GUI.Button(GermanSquad_offscreen2, new GUIContent("German Squad x" + amountPurchased[14], "GermanSquad"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[14]) {
			if(GUI.tooltip == "GermanSquad"){
				unit_GermanSquad.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[14] != ""){
				unit_GermanSquad.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[14] = GUI.tooltip;
		}


		//Make the panther button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (panther_offscreen1, new GUIContent("Panther\n$10000", "panther"))){
			if(selectUnit(15) && unitIconMade[15] == false){
				panther_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[15] = true;
			}
		}
		GUI.Button(panther_offscreen2, new GUIContent("Panther x" + amountPurchased[15], "panther"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[15]) {
			if(GUI.tooltip == "panther"){
				unit_panther.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[15] != ""){
				unit_panther.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[15] = GUI.tooltip;
		}


		//Make the Panzer4 button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (Panzer4_offscreen1, new GUIContent("Panzer4\n$10000", "Panzer4"))){
			if(selectUnit(16) && unitIconMade[16] == false){
				Panzer4_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[16] = true;
			}
		}
		GUI.Button(Panzer4_offscreen2, new GUIContent("Panzer4 x" + amountPurchased[16], "Panzer4"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[16]) {
			if(GUI.tooltip == "Panzer4"){
				unit_Panzer4.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[16] != ""){
				unit_Panzer4.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[16] = GUI.tooltip;
		}


		//Make the Wirbelwind button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (Wirbelwind_offscreen1, new GUIContent("Wirbelwind\n$10000", "Wirbelwind"))){
			if(selectUnit(17) && unitIconMade[17] == false){
				Wirbelwind_offscreen2 = new Rect(765, yCoordSelectionP2, 130, 20);
				unitIconMade[17] = true;
			}
		}
		GUI.Button(Wirbelwind_offscreen2, new GUIContent("Wirbelwind x" + amountPurchased[17], "Wirbelwind"));
		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTip[17]) {
			if(GUI.tooltip == "Wirbelwind"){
				unit_Wirbelwind.transform.position = new Vector3((float)0.5, (float).345, -3);
				audio.PlayOneShot (hoverOver);
			}
			if(lastTip[17] != "" && GUI.tooltip != "Wirbelwind"){
				unit_Wirbelwind.transform.position = new Vector3(10, (float).345, -3);
			}
			lastTip[17] = GUI.tooltip;
		}

	}

	void setText(){
		//Sets the guiText of remaining credits
		GT_p1_Credits_Remaining.guiText.text = "Credits   Remaining   " + player1_Credits.ToString();
		GT_p2_Credits_Remaining.guiText.text = "Credits   Remaining   " + player2_Credits.ToString();
		GT_battleBegin.guiText.text = "the  battle  begins  in  " + countdownTime;
	}
}
