using UnityEngine;
using System.Collections;

public class unitStatScript : MonoBehaviour {

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
	public int angle_type;
	private bool isAttack = false;
	public bool isRepeat = false;
	public bool isInit = false;

	public GameController gameController;
	// Use this for initialization
	void Start () {
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
		}
		else if (!hasAttacked && isAttacking && GUI.Button(new Rect(100, 125, 125, 50), new GUIContent("Skip\nAttack Phase", "moveTag"))) {
			hasAttacked = true;
			isAttacking = false;
			gameController.hideAllMoves();
		}
	}

	public void attackEnemy(){
		
		if (weapon != null )
		{
			weapon.angle_type = angle_type;
			weapon.changeAngle();
			weapon.isRepeat = isRepeat;
		}
		
	}

}