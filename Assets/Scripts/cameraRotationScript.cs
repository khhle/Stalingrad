using UnityEngine;
using System.Collections;

public class cameraRotationScript : MonoBehaviour {
	public Vector3 cameraPos;
	public GameObject gameController;

	private Animator animator;
	public int turnNumber = 1;
	private int oldTurn = 7;

	public GameObject hud, flag, turnChange, compass;

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
			if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 10){
				Camera.main.orthographicSize++;
				hud.transform.localScale += new Vector3(.5f,.5f,0);
				flag.GetComponent<SpriteRenderer>().enabled = false;
				turnChange.GetComponent<SpriteRenderer>().enabled = false;
				compass.GetComponent<SpriteRenderer>().enabled = false;
			}
			else if(Input.GetAxis ("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 5){
				Camera.main.orthographicSize--;
				hud.transform.localScale -= new Vector3(.5f,.5f,0);
			}
			
			if (Camera.main.orthographicSize == 5){
				flag.GetComponent<SpriteRenderer>().enabled = true;
				turnChange.GetComponent<SpriteRenderer>().enabled = true;
				compass.GetComponent<SpriteRenderer>().enabled = true;
			}

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

			if(turnNumber == 1){
				if(Input.mousePosition.x > Screen.width - .1f){
					if(cameraPos.x < 15)
						cameraPos.x+= .25f;
				}else if(Input.mousePosition.x < .1f){
					if(cameraPos.x > -15)
						cameraPos.x-= .25f;
				}

				if(Input.mousePosition.y > Screen.height - .1f){
					if(cameraPos.y < 10)
						cameraPos.y+= .25f;
				}else if(Input.mousePosition.y < .1f){
					if(cameraPos.y > -10)
						cameraPos.y-= .25f;
				}
			}else{
				if(Input.mousePosition.x > Screen.width - .1f){
					if(cameraPos.x > -15)
						cameraPos.x-= .25f;
				}else if(Input.mousePosition.x < .1f){
					if(cameraPos.x < 15)
						cameraPos.x+= .25f;
				}

				if(Input.mousePosition.y > Screen.height - .1f){
					if(cameraPos.y > -10)
						cameraPos.y-= .25f;
				}else if(Input.mousePosition.y < .1f){
					if(cameraPos.y < 10)
						cameraPos.y+= .25f;
				}
			}
			
			Camera.main.transform.position = cameraPos;
			
	}
}
