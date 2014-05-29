using UnityEngine;
using System.Collections;

public class UnitSelection : MonoBehaviour {

	//List of GameObject sprites
	GameObject data_Carry, unit_t34, unit_panther;

	//List of GuiTexts
	GameObject GT_t26, GT_panther, GT_p1_Credits_Remaining, GT_p2_Credits_Remaining, GT_player1, GT_player2;

	//Credits for how many units you can buy
	public int player1_Credits, player2_Credits;

	//An array used to check the purchased units
	private bool[] unitsPurchased = {false, false};
	private static int[] amountPurchased = {0, 0};
	private bool isPlayer1 = true;
	private bool isPlayer2 = false;

	//offscreen1 = Purchasable buttons that are only adjusted in the update based on player's turn
	Rect t34_offscreen1 = new Rect (1000, 75, 70, 40);
	Rect t34_offscreen2 = new Rect (1000, 75, 70, 40);
	//offscreen2 = nonclickable hover-over stats button that are adjusted only in the onGui. 
	Rect panther_offscreen1 = new Rect (1000, 75, 70, 40);
	Rect panther_offscreen2 = new Rect (1000, 75, 70, 40);
	
	// Use this for initialization
	void Start () {
		unit_t34 = GameObject.Find("unit_t-34");
		unit_panther = GameObject.Find ("unit_panther");
		GT_t26 = GameObject.Find ("T-26 cost");
		GT_panther = GameObject.Find ("Panther cost");
		data_Carry = GameObject.Find("Data Carry");
		GT_p1_Credits_Remaining = GameObject.Find ("p1 credits remaining");
		GT_p2_Credits_Remaining = GameObject.Find ("p2 credits remaining");
		GT_player1 = GameObject.Find ("Player1Pick");
		GT_player2 = GameObject.Find ("Player2Pick");
		
		//set show credits remaining to off
		//GT_p2_Credits_Remaining.guiText.enabled = false;
		//GT_p1_Credits_Remaining.guiText.enabled = false;
	}

	// Update is called once per frame
	void Update () {

		//updates any guiTexts that need updating
		setText ();
		if (isPlayer1){
			t34_offscreen1 = new Rect (300, 75, 70, 40);
			panther_offscreen1 = new Rect (1000, 75, 70, 40);
			GT_player1.renderer.material.color = Color.green;
			GT_player2.renderer.material.color = Color.red;
		}
		if (isPlayer2) {
			panther_offscreen1 = new Rect (300, 75, 70, 40);
			t34_offscreen1 = new Rect (1000, 75, 70, 40);
			GT_player2.renderer.material.color = Color.green;
			GT_player1.renderer.material.color = Color.red;
		}
		//If player1 holds tab they can view their credits remaining
		/*if(Input.GetKeyDown(KeyCode.Tab)){
			GT_p1_Credits_Remaining.guiText.enabled = true;
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {
			GT_p1_Credits_Remaining.guiText.enabled = false;
		}*/

		//If player2 holds enter they can view their credits remaining
		/*if(Input.GetKeyDown(KeyCode.Return)){
			GT_p2_Credits_Remaining.guiText.enabled = true;
		}
		if (Input.GetKeyUp (KeyCode.Return)) {
			GT_p2_Credits_Remaining.guiText.enabled = false;
		}*/


		if(player1_Credits == 0 && player2_Credits == 0){
			Invoke ("beginBattle", 3.0f);
		}

	}

	bool selectUnit(int i){
		switch (i) {
			//t34 tank purchased
			case 0:
				if (player1_Credits-5000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().t34++;
					player1_Credits -= 5000;
					increment(i);
					if(player2_Credits > 0){
						isPlayer2 = true;
						isPlayer1 = false;
					}
					return true;
				}	
				break;
			//panther tank purchased
			case 1:
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
			default:
				break;
		}
			return false;
	}
	void increment(int i){
		if(unitsPurchased[i] == false){
			unitsPurchased[i] = true;
			amountPurchased[i]++;
		}
		else {
			//incremement guiText variable (ie x2, x3, x4)
			amountPurchased[i]++;
		}
	}
	//Function for hovering and clicking buttons, isHovering is a flag for this function only
	void OnGUI () {

		// Make the t34 button and if its clicked purchase it!
		if (isPlayer1 && GUI.Button (t34_offscreen1, new GUIContent("T-34\n$5000", "t34"))){
			if(selectUnit(0))
				t34_offscreen2 = new Rect(100, 125, 70, 40);
		}
		GUI.Button(t34_offscreen2, new GUIContent("T-34\nx" + amountPurchased[0], "t34"));

		//Make the panther button and if its clicked purchase it!
		if (isPlayer2 && GUI.Button (panther_offscreen1, new GUIContent("Panther\n$10000", "panther"))){
			selectUnit(1);
			panther_offscreen2 = new Rect(790, 125, 70, 40);
		}
		GUI.Button(panther_offscreen2, new GUIContent("Panther\nx" + amountPurchased[1], "panther"));
		if (GUI.tooltip == "t34") {
			unit_t34.transform.position = new Vector3((float).5, (float).345, -3);
		}else if(GUI.tooltip == "" ){
			unit_t34.transform.position = new Vector3(10, (float).345, -3);
		}
		if (GUI.tooltip == "panther") {
			unit_panther.transform.position = new Vector3((float)0.5, (float).345, -3);
		}else if(GUI.tooltip == "" ){
			unit_panther.transform.position = new Vector3(10, (float).345, -3);
		}

	}

	void setText(){
		//Sets the guiText of remaining credits
		GT_p1_Credits_Remaining.guiText.text = "Credits   Remaining   " + player1_Credits.ToString();
		GT_p2_Credits_Remaining.guiText.text = "Credits   Remaining   " + player2_Credits.ToString();
	}

	void beginBattle(){
		Application.LoadLevel("s1");
	}
}
