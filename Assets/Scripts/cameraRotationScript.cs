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
			
			if(Input.mousePosition.x >= Screen.width - Screen.width * 0.01f && cameraPos.x < 250)
				cameraPos.x+= 0.25f;
			else if(Input.mousePosition.x <= Screen.width * 0.01f && cameraPos.x > -250)
				cameraPos.x-= 0.25f;
			if(Input.mousePosition.y >= Screen.height - Screen.height * 0.01f && cameraPos.y < 250)
				cameraPos.y+= 0.25f;
			else if(Input.mousePosition.y <= Screen.height * 0.01f && cameraPos.y > -250)
				cameraPos.y-= 0.25f;
			
			Camera.main.transform.position = cameraPos;
	
	}
}
