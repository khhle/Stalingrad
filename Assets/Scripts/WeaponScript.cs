using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
	//--------------------------------
	// 1 - Designer variables
	//--------------------------------

	public int angle_type;
	public bool isInit = false;
	public bool isRepeat = false;
	private float oldAngle = 0;
	private int teamNumber;

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform shotPrefab;
	
	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 0.25f;
	
	//--------------------------------
	// 2 - Cooldown
	//--------------------------------
	
	private float shootCooldown;
	private float x1;
	private float y1;
	private float z1;
	private int angle;
	public float angleF;
	private unitStatScript parentsStats;
	
	void Start()
	{
		shootCooldown = 0f;
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
			//isRepeat = false;
			//this.transform.Rotate(x1,y1,-angle, Space.World);

			//Debug.Log ("z! " + this.transform.rotation.z);
			float tempangle = this.transform.rotation.z *-1;
			//Debug.Log ("tempangle! " + tempangle);
			this.transform.Rotate(x1,y1,tempangle);
			this.transform.rotation = new Quaternion(0,0,0, 0);
			//this.transform.Rotate(x1,y1,-angleF, Space.World);

			//oldAngle = 0;
		}



		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
		//Attack (true);
	}



	public void changeAngle()
	{
		//if(angle_type == 0)
		//{
			//angle = angleF;
			this.transform.Rotate(x1,y1,angleF);
			//Debug.Log ("angelF! " + angleF);
			//Debug.Log ("z before 2nd rotation! " + this.transform.rotation.z);
			isInit = true;
			Attack(false);
		//}

		/*
		if(angle_type == 1)
		{
			angle = 0;
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 2)
		{
			angle = 30;

			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);

		}
		else if(angle_type == 3)
		{
			angle = 60;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 4)
		{
			angle = 90;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 5)
		{
			angle = 120;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 6)
		{
			angle = 150;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 7)
		{
			angle = 180;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 8)
		{
			angle = -150;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 9)
		{
			angle = -120;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 10)
		{
			angle = -90;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 11)
		{
			angle = -60;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}
		else if(angle_type == 12)
		{
			angle = -30;
			
			this.transform.Rotate(x1,y1,angle);
			isInit = true;
			Attack(false);
		}*/
	}
	
	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------
	
	/// <summary>
	/// Create a new projectile if possible
	/// </summary>
	public void Attack(bool isEnemy)
	{
		//Debug.Log ("Rotate " + angle_type);
		//if (CanAttack)
		//{
			shootCooldown = shootingRate;
			
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
				shot.teamNumber = teamNumber;
				shot.parentsStats = parentsStats;
			//assign a damage value; randomize this a bit later
				shot.attackPower = Random.Range(0, parentsStats.attack);
			}
			
			// Make the weapon shot always towards it
			moveScript2 move = shotTransform.gameObject.GetComponent<moveScript2>();
			if (move != null)
			{
				move.direction = this.transform.up; // towards in 2D space is the right of the sprite
			}
		}
		//}
	}
	
	/// <summary>
	/// Is the weapon ready to create a new projectile?
	/// </summary>
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}