﻿using UnityEngine;
using System.Collections;

public class moveGrandParent : MonoBehaviour {

	public bool col1;
	public bool isAvail = false;
	public bool isInit = false;
	public bool isInitCollide = false;
	public bool isCollide = false;
	public bool isReset = false;

	public bool isClick = false;
	public bool recentlyClicked = false;

	public bool isGreen = true;
	public int angle_type;
	public float angle;
	public unitStatScript grandParentStats;
	public GameController gameController;

	public GUIText moveText;

	private float x1;
	private float y1;

	// Use this for initialization
	void Start () {
		grandParentStats = transform.parent.parent.parent.GetComponent<unitStatScript> ();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}

		x1 = this.transform.position.x;
		y1 = this.transform.position.y;

		if (x1 >= grandParentStats.transform.position.x) {
			angle = Mathf.Rad2Deg * Mathf.Atan ((y1 - grandParentStats.transform.position.y) / (x1 - grandParentStats.transform.position.x));
			angle -= 90;
		}
		else 
		{
			angle = Mathf.Rad2Deg * Mathf.Atan ((y1 - grandParentStats.transform.position.y) / (x1 - grandParentStats.transform.position.x));
			angle += 90;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isGreen && isClick){
			 Vector3 tempPos = gameController.mainCam.WorldToScreenPoint (this.transform.position);
			if(gameController.playerTurn == 1)
			{
				moveText.transform.position = new Vector2 (tempPos.x/ 960f, tempPos.y / 600f);
			}
			else
			{
				moveText.transform.position = new Vector2 ((tempPos.x / 960f), (tempPos.y / 600f));
			}
			moveText.text = "" + grandParentStats.movesRemaining;
		}
		else if ( isGreen && moveText.text != "")
			moveText.text = "";

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
		else if (!isClick && recentlyClicked) {
			float x1 = this.transform.position.x;
			float y1 = this.transform.position.y;
			//float z1 = this.transform.position.z;
			recentlyClicked = false;
			if(isCollide == false){
				this.transform.position = new Vector3(x1,y1,20);
			}
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
			if (isClick == true && !gameController.attackStep) {
				grandParentStats.picked = true;
				gameController.pickedObject(grandParentStats);
				gameObject.transform.parent.transform.parent.parent.transform.position = new Vector3 (x1, y1, 0);
				//remove this for multiple moves. Put it on a counter if the unit has multiple moves for now.
				transform.parent.parent.GetComponent<hexMove>().hideMoves();
				gameController.hideAllMoves();

				transform.parent.transform.parent.parent.GetComponent<unitStatScript>().movesRemaining -=1;
				if(transform.parent.transform.parent.parent.GetComponent<unitStatScript>().movesRemaining <=0 )
				{
					gameController.attackStep = true;
				}
			}
		}
		else
		{
			if (isClick == true && gameController.attackStep) {
				grandParentStats.angle = angle;
				grandParentStats.isRepeat = true;

				//gettimeDestroy for each bullet
				float timeDestroy1 = transform.parent.GetComponent<ringScript>().rangeValue * 0.5f;
				Debug.Log ("destr" + timeDestroy1);
				//Turns Skip Attack Button off after attacking
				transform.parent.parent.parent.GetComponent<unitStatScript> ().isAttacking = false;
				grandParentStats.attackEnemy(transform.parent.GetComponent<ringScript>().rangeValue,timeDestroy1, false);
				transform.parent.parent.GetComponent<hexMove>().hideMoves();
			}
		}
		recentlyClicked = true;
	}


	void OnTriggerEnter2D(Collider2D coll) {
		ShotScript shot = coll.gameObject.GetComponent<ShotScript> ();
		if (shot != null)
			return;

		stone_script stone = coll.gameObject.GetComponent<stone_script> ();
		/*unitStatScript collStats = coll.gameObject.GetComponent<unitStatScript> ();
		hexMove hexColl = coll.gameObject.GetComponent<hexMove> ();*/
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

		/*if ((collStats != null || hexColl != null) && isGreen) {
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
		}*/

	}

	void OnTriggerStay2D(Collider2D coll)
	{
		//Debug.Log ("Collide Stay!");
		stone_script stone = coll.gameObject.GetComponent<stone_script> ();
		if (stone != null) {
			if (isClick == true) {
				isCollide = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		
	}

}