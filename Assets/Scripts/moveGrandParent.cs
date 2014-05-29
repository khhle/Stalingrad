using UnityEngine;
using System.Collections;

public class moveGrandParent : MonoBehaviour {

	public bool col1;
	public bool isAvail = false;
	public bool isInit = false;
	public bool isInitCollide = false;
	private int counter = 60;
	public bool isCollide = false;
	public bool isReset = false;

	public bool isClick = false;

	private GameController gameController;
	private Color originalColor;

	// Use this for initialization
	void Start () {
		originalColor = transform.GetComponent<SpriteRenderer> ().material.color;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isReset == true) {
			float x1 = this.transform.position.x;
			float y1 = this.transform.position.y;
			float z1 = this.transform.position.z;
			this.transform.position = new Vector3(x1,y1,100);
			isReset = false;
			isCollide = false;
				}


		if (isClick == true && isInit == false)
		{
			float x1 = this.transform.position.x;
			float y1 = this.transform.position.y;
			float z1 = this.transform.position.z;
			if(isCollide == false)
				this.transform.position = new Vector3(x1,y1,0); //0
			//else
				//this.transform.position = new Vector3(x1,y1,z1+20);//0

			isInit = true;
		}
		if (gameController.attackStep) {
			transform.GetComponent<SpriteRenderer> ().material.color = new Color(1,0,0);
		}
		else{
			transform.GetComponent<SpriteRenderer> ().material.color  = originalColor;
		}


		//if (isClick == true)
			//counter--;

		if (!isClick) {

				float x1 = this.transform.position.x;
				float y1 = this.transform.position.y;
				float z1 = this.transform.position.z;
				if(isCollide == false)
					this.transform.position = new Vector3(x1,y1,20);


				//moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
				//if(movObj.isInit != true)
				//	child.transform.position = new Vector3(x1,y1,z1+20);

				//if (movObj.col1 == null) {
				//child.transform.position = new Vector3 (x1, y1, z1 - 20);
				//movObj.isAvail = false;
				//child is your child transform
			//isClick = false;
			//isReset = true;
			//isCollide = false;
			}
			
			

	}

	void OnMouseEnter()
	{
		//	originalColor = gameObject.renderer.material.GetColor ("_Color");
		//	gameObject.renderer.material.color = mouseOverColor;
		
		
	}
	
	
	
	void OnMouseExit()
	{
		
	}
	
	void OnMouseDown()
	{

		float x1 = gameObject.transform.position.x;
		float y1 = gameObject.transform.position.y;
		float z1 = gameObject.transform.position.z;
		if (isClick == true)
		{//GameObject obj_parent = gameObject.transform.parent.transform.parent.transform.position;
						//GameObject obj_grand_parent =  obj_parent.transform.parent;
			if(!gameController.attackStep){
				gameObject.transform.parent.transform.parent.transform.position = new Vector3 (x1, y1, 0);
				//remove this for multiple moves. Put it on a counter if the unit has multiple moves for now.
				transform.parent.GetComponent<hexMove>().hideMoves();
				transform.parent.transform.parent.GetComponent<unitStatScript>().movesRemaining -=1;
				if(transform.parent.transform.parent.GetComponent<unitStatScript>().movesRemaining <=0 )
				{
					gameController.attackStep = true;
				}
			}
			//this.transform.position = new Vector3(x1,y1,z1+20);
			//isAvail = false;

		}

	}


	void OnTriggerEnter2D(Collider2D coll) {
		//col1 = true;
		//Debug.Log ("Collide!");
		if (isClick == true  ) {
			isCollide = true;
						//}
		}

		if (isClick == true && isInitCollide == false)
		{
			float x1 = this.transform.position.x;
			float y1 = this.transform.position.y;
			float z1 = this.transform.position.z;
			//Debug.Log ("z= " + z1);
			this.transform.position = new Vector3(x1,y1,z1+20);//0
			isInitCollide = true;
		}

	}

	void OnTriggerStay2D(Collider2D coll)
	{
		//Debug.Log ("Collide Stay!");
		if (isClick == true  ) {
			isCollide = true;
			//}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		Debug.Log ("Escape colide");

			float x1 = this.transform.position.x;
			float y1 = this.transform.position.y;
			float z1 = this.transform.position.z;
			
			this.transform.position = new Vector3(x1+100,y1,z1-20);//0



		//if (coll != null) {
		//float x1 = gameObject.transform.position.x;
		//float y1 = gameObject.transform.position.y;
		//float z1 = gameObject.transform.position.z;
		
		//this.transform.position = new Vector3 (x1, y1, z1 - 20);
	}
	


}
