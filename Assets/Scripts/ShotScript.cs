using UnityEngine;
using System.Collections;
/// Projectile behavior
public class ShotScript : MonoBehaviour
{
	public int attackPower;
	public int teamNumber;
	public bool isCounter = false;
	public int shotRange;
	public float angle;
	public float timeDestroy = 1;
	public unitStatScript parentsStats;
	private GameController gameController;
	public Vector2 direction;
	public Transform shotPrefab;
	public bool isSplash = false;
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		//Debug.Log ("time!" + timeDestroy);
		Destroy(gameObject, timeDestroy);
	}

	void OnTriggerEnter2D(Collider2D otherCollider){ 
		stone_script stone = otherCollider.gameObject.GetComponent<stone_script> ();
		if (stone != null) {
			//Debug.Log ("Collide stone!");	
			SpecialEffectsHelper.Instance.Explosion (transform.position);
			Destroy (gameObject);
		}
		//this is where the damage calculations take place. otherStats is the enemy unit's stats
		unitStatScript otherStats = otherCollider.gameObject.GetComponent<unitStatScript> ();
		if (otherStats != null && otherStats.playerOwner == 1 && teamNumber == 2 ) {

			if((otherStats.isTank && parentsStats.canShootTank) || (otherStats.isPlane && parentsStats.canShootPlane)
			   || (otherStats.isInfantry && parentsStats.canShootInfantry))
			{
				//deals damage equal to this attack power minus arandom value of defense.
				int tempDefense = Random.Range (0, otherStats.defense);
				if(attackPower > tempDefense)
					otherStats.health -= (attackPower - tempDefense);
				SpecialEffectsHelper.Instance.Explosion (transform.position);
				if(!isCounter)
					counterAttack (otherStats);




				if(isSplash == true)
					StartCoroutine(waitAndCall());
				else
					Destroy (gameObject);
			}
		}else if (otherStats != null && otherStats.playerOwner == 2  && teamNumber == 1) {

			if((otherStats.isTank && parentsStats.canShootTank) || (otherStats.isPlane && parentsStats.canShootPlane)
				   || (otherStats.isInfantry && parentsStats.canShootInfantry)){
				//deals damage equal to this attack power minus arandom value of defense.
				int tempDefense = Random.Range (0, otherStats.defense);
				if(attackPower > tempDefense)
					otherStats.health -= (attackPower - tempDefense);
				SpecialEffectsHelper.Instance.Explosion (transform.position);
				if(!isCounter)
					counterAttack (otherStats);


				if(isSplash == true)
					StartCoroutine(waitAndCall());
				else
					Destroy (gameObject);
			}
		}

	}

	void counterAttack( unitStatScript otherStats)
	{
		angle += 180;
		otherStats.angle = angle;
		var shotTransform = Instantiate(shotPrefab) as Transform;
		
		// Assign position
		shotTransform.position = otherStats.transform.position;
		
		// The is enemy property
		ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			shot.isSplash = false;
			shot.timeDestroy = otherStats.range * 0.5f;
			shot.shotRange = otherStats.range;
			shot.teamNumber = otherStats.playerOwner;
			shot.parentsStats = otherStats;
			shot.angle = angle;
			shot.isCounter = true;
			//assign a randomized damage value based off of unit's attack
			shot.attackPower = Random.Range(0, otherStats.attack);
		}
		
		// Make the weapon shot always towards it
		moveScript2 move = shotTransform.gameObject.GetComponent<moveScript2>();
		if (move != null)
		{
			move.direction = new Vector2(direction.x * -1, direction.y * -1); // towards in 2D space is the right of the sprite
		}
	}



	IEnumerator waitAndCall()
	{

		foreach (Transform child in transform)
		{
			splashScript splash = child.GetComponent<splashScript> ();
			splash.isShowUp = true;
			//splash damage calculate 
			splash.attackPower = attackPower ;
		}

		yield return new WaitForSeconds (0.1f);
		
	}


}