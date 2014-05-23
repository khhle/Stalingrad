using UnityEngine;
using System.Collections;

public class turnChangeButton : MonoBehaviour {

	private cameraRotationScript came;
	private int turnNumber;
	// Use this for initialization
	void Start () {
		came = GameObject.Find("Render").GetComponent<cameraRotationScript>();
		 	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseDown()
	{
		turnNumber = came.turnNumber;
		if (turnNumber == 1)
			came.turnNumber = 2;
		else if(turnNumber == 2)
		 	came.turnNumber = 1;
	}
}
