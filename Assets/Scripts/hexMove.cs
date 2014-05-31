using UnityEngine;
using System.Collections;

public class hexMove : MonoBehaviour {
	private Color mouseOverColor = Color.cyan;
	private Color originalColor ;
	public bool isAvail = false ;
	private GameController gameController;
	private unitStatScript parentStats;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		parentStats = transform.parent.GetComponent<unitStatScript> ();
 	}
	void Update()
	{
		if(transform.parent.GetComponent<unitStatScript>().movesRemaining <= 0)
			parentStats.isMoving = false;
	}



	void OnMouseEnter()
	{
		Debug.Log ("Collide!");
		originalColor = gameObject.renderer.material.GetColor ("_Color");
		gameObject.renderer.material.color = mouseOverColor;

		
	}



	void OnMouseExit()
	{
		gameObject.renderer.material.color = originalColor;
	}
	
	void OnMouseDown()
	{

		if( transform.parent.GetComponent<unitStatScript>().activeTurn)
		{
			foreach (Transform children in transform) {
				Transform thisChild = children;
				foreach (Transform child in thisChild) {
					moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
					//red_hex redhex = child.GetComponent<red_hex> ();
					if (movObj.isClick)
					{
						//if you click on the unit while in movement phase, remove skip button
						parentStats.isMoving = false;
						//if you click on the unit while in attack phase, remove skip button
						parentStats.isAttacking = false;

						movObj.isClick = false;
						//redhex.isClick = false;
					}
					else
					{
						//checks to see if it has any moves remaining
						if(transform.parent.GetComponent<unitStatScript>().movesRemaining > 0 && !gameController.attackStep && movObj.isGreen){

							//if you click on unit in movement phase give the option for the skip
							parentStats.isMoving = true;

							movObj.isClick = true;
							movObj.isInit = false;
							movObj.isInitCollide = false;
							movObj.isCollide = false;

						}
						else if(gameController.attackStep && !movObj.isGreen && !parentStats.hasAttacked){
							//if the button hasn't been pressed to skip move, hide the button
							parentStats.isMoving = false;

							//if you click on the unit in attack phase give the option for the skip
							parentStats.isAttacking = true;
							
							movObj.isClick = true;
							movObj.isInit = false;
							movObj.isInitCollide = false;
							movObj.isCollide = false;
						}
				}
				}
			}
		}


	}

	//use this for hiding the green hex objects.
	public void hideMoves()
	{
		foreach (Transform children in transform) {
			Transform thisChild = children;
			foreach (Transform child in thisChild) {
				moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
				//red_hex redhex = child.GetComponent<red_hex> ();
				movObj.isClick = false;
				//redhex.isClick = false;
			}
		}
	}

}
