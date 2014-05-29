using UnityEngine;
using System.Collections;

public class AttackButton : MonoBehaviour {

	public GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		if(!gameController.attackStep){
			gameController.attackStep = true;
		}
	}
}
