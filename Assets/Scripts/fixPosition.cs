
//For perfect combination:
//screen background: position x:05, y:1
//					scale: x:3, y:2.58

//tank: pos: x: 1, y: 0.5
//		scale: x:3, y: 3






using UnityEngine;
using System.Collections;
using System;
public class fixPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		float x1 = gameObject.transform.position.x;
		float y1 = gameObject.transform.position.y;
		float z1 = gameObject.transform.position.z;
		float deci;
		x1 = (float)Math.Round (x1);
		y1 = (float)Math.Round (y1,1);
		//if(x1 % 2 == 1)
		//{
			deci = y1 * 10 % 10;
			if (deci >= 3 && deci <= 7) {
				y1 = (float)Math.Round (y1);
				y1 += 0.5f;
			}
			else
			{
				y1 = (float)Math.Round (y1);
			}
		//}
		Debug.Log (x1 + " " + y1);
		this.transform.position = new Vector3 (x1, y1, z1);


	}
}
