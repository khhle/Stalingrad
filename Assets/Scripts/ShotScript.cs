﻿using UnityEngine;

/// Projectile behavior
public class ShotScript : MonoBehaviour
{
	public int attackPower;
	public int teamNumber;
	public bool isCounter = false;
	public int shotRange;
	public float timeDestroy = 1;

	public unitStatScript parentsStats;
	private GameController gameController;
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		Debug.Log ("time!" + timeDestroy);
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
		if (otherStats != null && otherStats.playerOwner == 1 && teamNumber == 2) {
			counterAttack(otherStats);
			SpecialEffectsHelper.Instance.Explosion (transform.position);
			//deals damage equal to this attack power minus arandom value of defense.
			int tempDefense = Random.Range (0, otherStats.defense);
			if(attackPower > tempDefense)
				otherStats.health -= (attackPower - tempDefense);
		}else if (otherStats != null && otherStats.playerOwner == 2  && teamNumber == 1) {

			SpecialEffectsHelper.Instance.Explosion (transform.position);
			counterAttack (otherStats);
			//deals damage equal to this attack power minus arandom value of defense.
			int tempDefense = Random.Range (0, otherStats.defense);
			if(attackPower > tempDefense)
				otherStats.health -= (attackPower - tempDefense);
		}
	}

	void counterAttack( unitStatScript otherStats)
	{
		if (otherStats.range >= shotRange){
			int tempDefense = Random.Range (0, parentsStats.defense);
			int tempAttack = Random.Range (0, otherStats.attack);
			int damage = tempAttack - tempDefense;
			if(damage > 0)
				parentsStats.health -= damage;
		}
	}



}