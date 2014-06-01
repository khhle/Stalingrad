using UnityEngine;

/// Launch projectile

public class WeaponScript : MonoBehaviour
{
	//--------------------------------
	// 1 - Designer variables
	//--------------------------------

	public bool isInit = false;
	public bool isRepeat = false;
	private float oldAngle = 0;
	private int teamNumber;

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
	
	void Start()
	{
		parentsStats = transform.GetComponent<unitStatScript> ();
		teamNumber = parentsStats.playerOwner;
	}
	
	void Update()
	{
		x1 = this.transform.rotation.x;
		y1 = this.transform.rotation.y;

		if(isInit == true  )
		{
			Attack(false);
			isInit = false;

			//Debug.Log ("z! " + this.transform.rotation.z);
			float tempangle = this.transform.rotation.z *-1;
			//Debug.Log ("tempangle! " + tempangle);
			this.transform.Rotate(x1,y1,tempangle);
			this.transform.rotation = new Quaternion(0,0,0, 0);
		}
	}



	public void changeAngle()
	{

		this.transform.Rotate(x1,y1,angleF);
		//Debug.Log ("angelF! " + angleF);
		//Debug.Log ("z before 2nd rotation! " + this.transform.rotation.z);
		isInit = true;
		Attack(false);
	}
	
	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------

	/// Create a new projectile if possible
	public void Attack(bool isEnemy)
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
				shot.shotRange = parentsStats.rangeClicked; //give it the range of the ring that was clicked on
				shot.teamNumber = teamNumber;
				shot.parentsStats = parentsStats;
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