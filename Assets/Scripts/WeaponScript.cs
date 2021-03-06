﻿using UnityEngine;

/// Launch projectile

public class WeaponScript : MonoBehaviour
{
	//--------------------------------
	// 1 - Designer variables
	//--------------------------------

	public bool isInit = false;
	public bool isRepeat = false;
	private int teamNumber;
	public float timeDestroy;

	
	public AudioClip infantryshot;
	public AudioClip tankshot;
	public AudioClip planeshot;

	/// Projectile prefab for shooting
	public Transform shotPrefab;
	
	//--------------------------------
	// 2 - Cooldown
	//--------------------------------

	private float x1;
	private float y1;
	private float z1;
	private int angle;
	public float angleF;
	private unitStatScript parentsStats;

	private GameController gameController;

	public bool hasSplash = false;
	
	void Start()
	{
		parentsStats = transform.GetComponent<unitStatScript> ();
		teamNumber = parentsStats.playerOwner;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	void Update()
	{
		x1 = this.transform.rotation.x;
		y1 = this.transform.rotation.y;

		if(isInit == true  )
		{
			Attack(false, false);
			isInit = false;

			//Debug.Log ("z! " + this.transform.rotation.z);
			float tempangle = this.transform.rotation.z *-1;
			//Debug.Log ("tempangle! " + tempangle);
			this.transform.Rotate(x1,y1,tempangle);
			if(gameController.playerTurn == 1)
				this.transform.rotation = new Quaternion(0,0,0, 0);
			else
				this.transform.rotation = new Quaternion(180,0,0, 0);
		}
	}



	public void changeAngle(bool counter)
	{

		this.transform.Rotate(x1,y1,angleF);
		//Debug.Log ("angelF! " + angleF);
		//Debug.Log ("z before 2nd rotation! " + this.transform.rotation.z);
		isInit = true;
		if(counter)
		{
			Attack(false, true);
		}
		else
			Attack(false, false);
	}
	
	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------

	/// Create a new projectile if possible
	public void Attack(bool isEnemy, bool counter)
	{
			
		if(!parentsStats.hasAttacked){
			parentsStats.hasAttacked = true;
			// Create a new shot
			var shotTransform = Instantiate(shotPrefab) as Transform;
			
			// Assign position
			shotTransform.position = transform.position;
			
			// The is enemy property
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				if(parentsStats.isTank){
					audio.PlayOneShot(tankshot);
				}else if(parentsStats.isPlane){
					audio.PlayOneShot(planeshot);
				}else if(parentsStats.isInfantry){
					audio.PlayOneShot(infantryshot);
				}
				shot.isSplash = hasSplash;
				shot.timeDestroy = timeDestroy;
				shot.shotRange = parentsStats.rangeClicked; //give it the range of the ring that was clicked on
				shot.teamNumber = teamNumber;
				shot.parentsStats = parentsStats;
				shot.angle = angleF;
				shot.direction = this.transform.up;
				if(counter)
					shot.isCounter = true;
				//assign a randomized damage value based off of unit's attack
				shot.attackPower = Random.Range(0, parentsStats.attack);
			}
			
			// Make the weapon shot always towards it
			moveScript2 move = shotTransform.gameObject.GetComponent<moveScript2>();
			if (move != null)
			{
				move.direction = this.transform.up; // towards in 2D space is the right of the sprite
			}
		}
	}

}