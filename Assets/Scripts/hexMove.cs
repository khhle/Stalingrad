using UnityEngine;
using System.Collections;

public class hexMove : MonoBehaviour {
	private Color mouseOverColor = Color.cyan;
	private Color originalColor ;
	public bool isAvail = false ;
	private int counter = 60;
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

	}



	void OnMouseEnter()
	{
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
			foreach (Transform child in transform) {
				moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
				//red_hex redhex = child.GetComponent<red_hex> ();
				if (movObj.isClick)
				{
					movObj.isClick = false;
					//redhex.isClick = false;
				}
				else
				{
					//checks to see if it has any moves remaining
					if(transform.parent.GetComponent<unitStatScript>().movesRemaining > 0 || (gameController.attackStep && !parentStats.hasAttacked)){
						float x1 = child.transform.position.x;
						float y1 = child.transform.position.y;
						float z1 = child.transform.position.z;

						movObj.isClick = true;
						movObj.isInit = false;
						movObj.isInitCollide = false;
						movObj.isCollide = false;



					}
				}
			}
		}


	}

	//use this for hiding the green hex objects.
	public void hideMoves()
	{
		foreach (Transform child in transform) {
			moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
			//red_hex redhex = child.GetComponent<red_hex> ();
			movObj.isClick = false;
			//redhex.isClick = false;
		}
	}

}
