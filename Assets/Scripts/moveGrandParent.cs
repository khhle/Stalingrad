using UnityEngine;
using System.Collections;

public class moveGrandParent : MonoBehaviour {

	public bool col1;
	public bool isAvail = false;
	public bool isInit = false;
	public bool isInitCollide = false;
	public bool isCollide = false;
	public bool isReset = false;

	public bool isClick = false;

	public bool isGreen = true;
	public int angle_type;
	private Color originalColor;
	public GameController gameController;
	// Use this for initialization
	void Start () {
		originalColor = transform.GetComponent<SpriteRenderer> ().material.color;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isReset == true) {
			float x1 = this.transform.position.x;
			float y1 = this.transform.position.y;
			//float z1 = this.transform.position.z;
			this.transform.position = new Vector3(x1,y1,100);
			isReset = false;
			isCollide = false;
				}


		if (isClick == true && isInit == false)
		{
			float x1 = this.transform.position.x;
			float y1 = this.transform.position.y;
			//float z1 = this.transform.position.z;
			if(isCollide == false)
				if(isGreen == true)
					this.transform.position = new Vector3(x1,y1,0); //0
				else 
					this.transform.position = new Vector3(x1,y1,-5); //0
			
			isInit = true;
		}
		if (gameController.attackStep) {
			transform.GetComponent<SpriteRenderer> ().material.color = new Color(1,0,0);
		}
		else{
			transform.GetComponent<SpriteRenderer> ().material.color  = originalColor;
		}




		if (!isClick) {
				float x1 = this.transform.position.x;
				float y1 = this.transform.position.y;
				//float z1 = this.transform.position.z;
				if(isCollide == false)
					this.transform.position = new Vector3(x1,y1,20);
		}
			
			

	}

	void OnMouseEnter()
	{
		//	originalColor = gameObject.renderer.material.GetColor ("_Color");
		//	gameObject.renderer.material.color = mouseOverColor;
		
		
	}
	
	
	
	void OnMouseExit()
	{
		
	}
	
	void OnMouseDown()
	{
		if (isGreen == true)
		{
			float x1 = gameObject.transform.position.x;
			float y1 = gameObject.transform.position.y;
			//float z1 = gameObject.transform.position.z;
			if (isClick == true) {
				if(!gameController.attackStep){
				gameObject.transform.parent.transform.parent.transform.position = new Vector3 (x1, y1, 0);
				//remove this for multiple moves. Put it on a counter if the unit has multiple moves for now.
				transform.parent.GetComponent<hexMove>().hideMoves();
				transform.parent.transform.parent.GetComponent<unitStatScript>().movesRemaining -=1;
				if(transform.parent.transform.parent.GetComponent<unitStatScript>().movesRemaining <=0 )
				{
					gameController.attackStep = true;
				}
			}


			}
		}
		else
		{
			//WeaponScript weapon = GetComponent<WeaponScript> ();
			//if (weapon != null) {
			tank_script tankObj =	gameObject.transform.parent.transform.parent.GetComponent<tank_script>();
			tankObj.angle_type = angle_type;
			tankObj.isRepeat = true;
			//tankObj.isInit = false;
			tankObj.attack();
			transform.parent.GetComponent<hexMove>().hideMoves();

				// false because the player is not an enemy
				//weapon.Attack (false);


		}

	}


	void OnTriggerEnter2D(Collider2D coll) {
		ShotScript shot = coll.gameObject.GetComponent<ShotScript> ();
		if (shot != null)
			return;

		stone_script stone = coll.gameObject.GetComponent<stone_script> ();
		if (stone != null) {
						//Debug.Log ("Collide!");
						if (isClick == true) {
								isCollide = true;
					
						}

						if (isClick == true && isInitCollide == false) {
								float x1 = this.transform.position.x;
								float y1 = this.transform.position.y;
								float z1 = this.transform.position.z;
								//Debug.Log ("z= " + z1);
								this.transform.position = new Vector3 (x1, y1, z1 + 20);//0
								isInitCollide = true;
						}
				}

	}

	void OnTriggerStay2D(Collider2D coll)
	{
		//Debug.Log ("Collide Stay!");
		stone_script stone = coll.gameObject.GetComponent<stone_script> ();
		if (stone != null) {
						if (isClick == true) {
								isCollide = true;
								//}
						}
				}
	}

	void OnTriggerExit2D(Collider2D coll)
	{

	
	}


}
