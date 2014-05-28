using UnityEngine;
using System.Collections;

public class UnitSelection : MonoBehaviour {

	//List of GameObject sprites
	GameObject data_Carry;

	//List of GuiTexts
	GameObject GT_t26, GT_panther, GT_p1_Credits_Remaining, GT_p2_Credits_Remaining;

	//Credits for how many units you can buy
	public int player1_Credits, player2_Credits;

	// Use this for initialization
	void Start () {
		GT_t26 = GameObject.Find ("T-26 cost");
		GT_panther = GameObject.Find ("Panther cost");
		data_Carry = GameObject.Find("Data Carry");
		GT_p1_Credits_Remaining = GameObject.Find ("p1 credits remaining");
		GT_p2_Credits_Remaining = GameObject.Find ("p2 credits remaining");
		
		//set default cost color to green
		GT_t26.guiText.color = Color.green;
		GT_panther.guiText.color = Color.green;
		
		//set show credits remaining to off
		GT_p2_Credits_Remaining.guiText.enabled = false;
		GT_p1_Credits_Remaining.guiText.enabled = false;
	}

	// Update is called once per frame
	void Update () {

		//updates any guiTexts that need updating
		setText ();

		//If player1 holds tab they can view their credits remaining
		if(Input.GetKeyDown(KeyCode.Tab)){
			GT_p1_Credits_Remaining.guiText.enabled = true;
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {
			GT_p1_Credits_Remaining.guiText.enabled = false;
		}

		//If player2 holds enter they can view their credits remaining
		if(Input.GetKeyDown(KeyCode.Return)){
			GT_p2_Credits_Remaining.guiText.enabled = true;
		}
		if (Input.GetKeyUp (KeyCode.Return)) {
			GT_p2_Credits_Remaining.guiText.enabled = false;
		}

		//depending on which hotkey pressed will select a unit for Russian Army
		if(Input.GetKeyDown(KeyCode.Q)){
			selectUnit(0);
		}
		if(Input.GetKeyDown(KeyCode.W)){
			selectUnit(1);
		}
		if(Input.GetKeyDown(KeyCode.E)){
			selectUnit(2);
		}
		if(Input.GetKeyDown(KeyCode.R)){
			selectUnit(3);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			selectUnit(4);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			selectUnit(5);
		}
		if(Input.GetKeyDown(KeyCode.D)){
			selectUnit(6);
		}
		if(Input.GetKeyDown(KeyCode.F)){
			selectUnit(7);
		}

		//depending on which hotkey pressed will select a unit for Russian Army
		if(Input.GetKeyDown(KeyCode.U)){
			selectUnit(8);
		}
		if(Input.GetKeyDown(KeyCode.I)){
			selectUnit(9);
		}
		if(Input.GetKeyDown(KeyCode.O)){
			selectUnit(10);
		}
		if(Input.GetKeyDown(KeyCode.P)){
			selectUnit(11);
		}
		if(Input.GetKeyDown(KeyCode.H)){
			selectUnit(12);
		}
		if(Input.GetKeyDown(KeyCode.J)){
			selectUnit(13);
		}
		if(Input.GetKeyDown(KeyCode.K)){
			selectUnit(14);
		}
		if(Input.GetKeyDown(KeyCode.L)){
			selectUnit(15);
		}

		if(player1_Credits == 0 && player2_Credits == 0){
			Invoke ("beginBattle", 3.0f);
		}
	}

	void selectUnit(int i){
		switch (i) {
			case 0:
				if (player1_Credits-5000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().t26++;
					player1_Credits -= 5000;
				}	
				break;
			case 8:
				if (player2_Credits-10000 >= 0) {
					data_Carry.GetComponent<Data_Carry>().panther++;
					player2_Credits -= 10000;
				}	
			break;
			default:
				break;
		}
	}

	void setText(){
		//Sets the guiText of remaining credits
		GT_p1_Credits_Remaining.guiText.text = "Credits   Remaining   " + player1_Credits.ToString();
		GT_p2_Credits_Remaining.guiText.text = "Credits   Remaining   " + player2_Credits.ToString();

		//if the player does not have enough credits, the cost turns red
		if (player1_Credits < 5000) {
			GT_t26.guiText.color = Color.red;
		}
		if (player2_Credits < 10000) {
			GT_panther.guiText.color = Color.red;
		}
	}

	void beginBattle(){
		Application.LoadLevel("s1");
	}
}
