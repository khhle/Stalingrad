using UnityEngine;
using System.Collections;

public class unitStatScript : MonoBehaviour {

	public int id;
	public bool isClicked = false;

	//List of Russian Units
	GameObject unit_t28, unit_t34, unit_t60, unit_RussianSniper, unit_RussianSquad, unit_RussianAT, unit_RussianBomber, unit_RussianCannon, unit_RussianFighter;
	
	//List of German Units
	GameObject unit_Flak30, unit_GermanAT, unit_GermanBomber, unit_GermanFighter, unit_panther, unit_Wirbelwind, unit_GermanSniper, unit_GermanSquad, unit_Panzer4;


	public bool isMoving = false;
	public bool isAttacking = false;

	public int playerOwner;
	public int attack;
	public int defense;
	public int health;
	public int moves;
	public int range;

	public int movesRemaining;

	public bool canShootInfantry;
	public bool canShootTank;
	public bool canShootPlane;
	public bool hasSplash;

	public GUIText statText;

	//What kind of unit it is, ie tank, plane, infantry.
	public string unitType;

	//keeps track of how far the unit can move

	//checks if it's the player's turn to move
	public bool activeTurn = false;
	public bool hasAttacked = false;

	//weapon stuff, previously in tank_script
	private WeaponScript weapon;
	public float angle;
	private bool isAttack = false;
	public bool isRepeat = false;
	public bool isInit = false;

	//keeping track of the range that was just clicked for attacking
	public int rangeClicked;

	//keeps track of if this unit is the one the player pciked this turn
	public bool picked;

	public GameController gameController;
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

		weapon = transform.GetComponent<WeaponScript> ();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.playerTurn == 1)
		{
			statText.transform.position = new Vector2 ((transform.position.x + 8) /16f, (transform.position.y + 5) / 10f);
		}
		else
		{
			statText.transform.position = new Vector2 (1 - ((transform.position.x + 8) /16f), 1 - ((transform.position.y + 5) / 10f));
		}

		if (statText != null)
			statText.text = attack + "  "+ health +"  " + defense;

		if(gameController.playerTurn == playerOwner)
		{
			activeTurn = true;
		}
		else
			activeTurn = false;

		if (health <= 0) {
			Destroy(this.gameObject);
		}

	}

	//creates a button to skip movement phase
	void OnGUI(){
		if (isMoving && GUI.Button(new Rect(100, 125, 125, 50), new GUIContent("Skip\nMovement Phase", "moveTag"))) {
			gameController.attackStep = true;
			isMoving = false;
			gameController.hideAllMoves();
			picked = true;
			gameController.pickedObject(this);
		}
		else if (!hasAttacked && isAttacking && GUI.Button(new Rect(100, 125, 125, 50), new GUIContent("Skip\nAttack Phase", "moveTag"))) {
			hasAttacked = true;
			isAttacking = false;
			gameController.hideAllMoves();
		}
	}

	public void attackEnemy(int tempRange,float timeDestroy){
		
		if (weapon != null )
		{
			weapon.timeDestroy = timeDestroy;
			rangeClicked = tempRange;
			weapon.angleF = angle;
			weapon.changeAngle();
			weapon.isRepeat = isRepeat;
		}
		
	}


}