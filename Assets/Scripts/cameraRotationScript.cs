using UnityEngine;
using System.Collections;

public class cameraRotationScript : MonoBehaviour {
	public Vector3 cameraPos;
	public GameObject gameController;

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
		gameController = GameObject.Find ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		if (turnNumber == 1)
			animator.SetInteger ("turnNumber", 1);
		else if(turnNumber == 2)
			animator.SetInteger ("turnNumber", 2);

			cameraPos = Camera.main.transform.position;

			//User is scrolling Back
			if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 10)
				Camera.main.orthographicSize++;
			else if(Input.GetAxis ("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 1)
				Camera.main.orthographicSize--;
			
			if(turnNumber == 1){
			if(Input.GetKey ("up"))
				if(cameraPos.y < 10)
					cameraPos.y+= 0.25f;
			if(Input.GetKey ("down"))
				if(cameraPos.y > -10)
					cameraPos.y-= 0.25f;
			if(Input.GetKey ("left"))
				if(cameraPos.x > -15)
					cameraPos.x-= 0.25f;
			if(Input.GetKey ("right"))
				if(cameraPos.x < 15)
				cameraPos.x+= 0.25f;

			}else{
			if(Input.GetKey ("up"))
				if(cameraPos.y > -10)
					cameraPos.y-= 0.25f;
			if(Input.GetKey ("down"))
				if(cameraPos.y < 10)
					cameraPos.y+= 0.25f;
			if(Input.GetKey ("left"))
				if(cameraPos.x < 15)
					cameraPos.x+= 0.25f;
			if(Input.GetKey ("right"))
				if(cameraPos.x > -15)
					cameraPos.x-= 0.25f;

			}
			
			Camera.main.transform.position = cameraPos;
			
	}
}
