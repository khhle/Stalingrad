using UnityEngine;
using System.Collections;

public class cameraRotationScript : MonoBehaviour {

	private Animator animator;
	public int turnNumber = 1;
	private int oldTurn = 7;
	//khan is a khan
	void Awake()
	{
		// ...
		animator = GetComponent<Animator>();
		//...
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (turnNumber == 1)
			animator.SetInteger ("turnNumber", 1);
		else if(turnNumber == 2)
			animator.SetInteger ("turnNumber", 2);
	
	}
}
