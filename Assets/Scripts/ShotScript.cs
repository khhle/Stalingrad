using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{
	// 1 - Designer variables
	
	/// <summary>
	/// Damage inflicted
	/// </summary>
	//public int damage = 1;
	
	/// <summary>
	/// Projectile damage player or enemies?
	/// </summary>
	//public bool isEnemyShot = false;
	public int attackPower;
	public int teamNumber;
	
	private GameController gameController;
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		
		Destroy(gameObject, 1);
	}

	void OnTriggerEnter2D(Collider2D otherCollider){ 
			stone_script stone = otherCollider.gameObject.GetComponent<stone_script> ();
			if (stone != null) {
				//Debug.Log ("Collide stone!");	
				SpecialEffectsHelper.Instance.Explosion (transform.position);
				Destroy (gameObject);
			}

		teamA tA = otherCollider.gameObject.GetComponent<teamA> ();
		if (tA != null && teamNumber == 2) {

			SpecialEffectsHelper.Instance.Explosion (transform.position);
			//deals damage equal to this attack power, subtract defense later.
			tA.gameObject.transform.parent.GetComponent<unitStatScript>().health -= attackPower;
		}

		teamB tB = otherCollider.gameObject.GetComponent<teamB> ();
		if (tB != null && teamNumber == 1) {

			SpecialEffectsHelper.Instance.Explosion (transform.position);
			//deals damage equal to this attack power, subtract defense later.
			tB.gameObject.transform.parent.GetComponent<unitStatScript>().health -= attackPower;
		}
	}





}