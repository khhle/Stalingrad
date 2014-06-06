using UnityEngine;
using System.Collections;

public class splashScript : MonoBehaviour {

	public int attackPower;
	public bool isShowUp = false;
	private bool isCauseDamage = false;
	private ShotScript parentShot;
	// Use this for initialization
	void Start () {
		//find your daddy
		parentShot = this.transform.parent.GetComponent<ShotScript> ();;
	}
	
	// Update is called once per frame
	void Update () {
		if(isShowUp == true)
		{
			float x1 = this.transform.position.x;
			float y1 = this.transform.position.y;
			this.transform.position = new Vector3(x1,y1,0);
			isCauseDamage = true;
			Destroy(gameObject, 1);
		}
	}


	void OnTriggerEnter2D(Collider2D otherCollider){ 
		if(isCauseDamage == true)
		{
			//this is where the damage calculations take place. otherStats is the enemy unit's stats
			unitStatScript otherStats = otherCollider.gameObject.GetComponent<unitStatScript> ();
			if (otherStats != null && otherStats.playerOwner == 1 && parentShot.teamNumber == 2 ) {
				
				if((otherStats.isTank && parentShot.parentsStats.canShootTank) || (otherStats.isPlane && parentShot.parentsStats.canShootPlane)
				   || (otherStats.isInfantry && parentShot.parentsStats.canShootInfantry))
				{
					//deals damage equal to this attack power minus arandom value of defense.
					int tempDefense = Random.Range (0, otherStats.defense);
					if(attackPower > tempDefense)
						otherStats.health -= (attackPower - tempDefense);
					SpecialEffectsHelper.Instance.Explosion (transform.position);
					//if(!isCounter)
					//	counterAttack (otherStats);

					
					Debug.Log("Collide splash");
					isCauseDamage = false;
					//Destroy (gameObject);
				}
			}else if (otherStats != null && otherStats.playerOwner == 2  && parentShot.teamNumber == 1) {
				
				if((otherStats.isTank && parentShot.parentsStats.canShootTank) || (otherStats.isPlane && parentShot.parentsStats.canShootPlane)
				   || (otherStats.isInfantry && parentShot.parentsStats.canShootInfantry)){
					//deals damage equal to this attack power minus arandom value of defense.
					int tempDefense = Random.Range (0, otherStats.defense);
					if(attackPower > tempDefense)
						otherStats.health -= (attackPower - tempDefense);
					SpecialEffectsHelper.Instance.Explosion (transform.position);
					//if(!isCounter)
					//	counterAttack (otherStats);
					//Destroy (gameObject);
					Debug.Log("Collide splash");
					isCauseDamage = false;
				}

			}

		}

	}

}
