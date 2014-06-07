using UnityEngine;
using System.Collections;

public class unitStatScript : MonoBehaviour {

	public int id;
	public bool isClicked = false;

	public bool isMoving = false;
	public bool isAttacking = false;
	public bool canPlace = true;

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
	public bool isTank;
	public bool isInfantry;
	public bool isPlane;

	public GUIText statText;
	//What kind of unit it is, ie tank, plane, infantry.
	public string unitType;

	//keeps track of how far the unit can move

	//checks if it's the player's turn to move
	public bool hasAttacked = false;

	//weapon stuff, previously in tank_script
	private WeaponScript weapon;
	public float angle;
	private bool isAttack = false;
	public bool isRepeat = false;
	public bool isInit = false;

	public Vector3 tempPos;

	//keeping track of the range that was just clicked for attacking
	public int rangeClicked;

	//keeps track of if this unit is the one the player pciked this turn
	public bool picked;

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
		tempPos = gameController.mainCam.WorldToScreenPoint (this.transform.position);
		if(gameController.playerTurn == 1)
		{
			statText.transform.position = new Vector2 (tempPos.x/ 960f, tempPos.y / 600f);
		}
		else
		{
			statText.transform.position = new Vector2 ((tempPos.x / 960f), (tempPos.y / 600f));
		}


		if (statText != null)
			statText.text = "" + health;

		if (health <= 0) {
			Destroy(this.gameObject);
		}

	}

	//creates a button to skip movement phase
	void OnGUI(){
		if (isMoving && GUI.Button(new Rect(13, 28, 125, 50), new GUIContent("Skip\nMovement Phase", "moveTag"))) {
			gameController.attackStep = true;
			isMoving = false;
			gameController.hideAllMoves();
			picked = true;
			gameController.pickedObject(this);
		}
		else if (!hasAttacked && isAttacking && GUI.Button(new Rect(13, 28, 125, 50), new GUIContent("Skip\nAttack Phase", "moveTag"))) {
			hasAttacked = true;
			isAttacking = false;
			gameController.hideAllMoves();
		}
	}

	public void attackEnemy(int tempRange,float timeDestroy, bool counter){
		
		if (weapon != null )
		{
			weapon.timeDestroy = timeDestroy;
			rangeClicked = tempRange;
			weapon.angleF = angle;
			if(counter){
				weapon.changeAngle(true);
			}
			else
				weapon.changeAngle(false);
			weapon.isRepeat = isRepeat;
			this.transform.rotation *= Quaternion.AngleAxis (180, transform.right);
		}
		
	}


}