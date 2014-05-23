using UnityEngine;
using System.Collections;

public class hexMove : MonoBehaviour {
	private Color mouseOverColor = Color.cyan;
	private Color originalColor ;
	public bool isAvail = false ;
	private int counter = 60;

	void Update()
	{
		/*if (isAvail == true)
			counter--;
		if (counter < 0) {
			foreach (Transform child in transform)
			{
				float x1 = child.transform.position.x;
				float y1 = child.transform.position.y;
				float z1 = child.transform.position.z;
				//child.transform.position = new Vector3(x1,y1,z1+20);


				moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
				if(movObj.isInit != true)
					child.transform.position = new Vector3(x1,y1,z1+20);

				//if (movObj.col1 == null) {
				//child.transform.position = new Vector3 (x1, y1, z1 - 20);
				movObj.isAvail = false;
				//child is your child transform
			}
			counter = 60;
			isAvail = false;
		}*/

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
		//if (isAvail == false) {
			foreach (Transform child in transform) {
					float x1 = child.transform.position.x;
					float y1 = child.transform.position.y;
					float z1 = child.transform.position.z;

					moveGrandParent movObj = child.GetComponent<moveGrandParent> ();
					//if (movObj.col1 == false) {
						//child.transform.position = new Vector3 (x1, y1, z1 - 20);
						movObj.isClick = true;
			movObj.isInit = false;
			movObj.isInitCollide = false;
			movObj.isCollide = false;
					//} else if (movObj.col1 == false)
					//{
					//	child.transform.position = new Vector3 (x1, y1+10, z1 + 20);
					//	movObj.isAvail = false;
					//}
					//child is your child transform
			}

			//isAvail = true;
		//}
	}

}
