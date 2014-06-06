
//For perfect combination:
//screen background: position x:05, y:1
//					scale: x:3, y:2.58

//tank: pos: x: 1, y: 0.5
//		scale: x:3, y: 3






using UnityEngine;
using System.Collections;
using System;
public class fixPosition : MonoBehaviour {

	public Vector3 prevPosition;

	void Start(){
		prevPosition = new Vector3 (0, 0, 0);
	}
	// Update is called once per frame
	void Update () {

		if(prevPosition != this.transform.position){
			float x1 = gameObject.transform.position.x;
			float y1 = gameObject.transform.position.y;
			float z1 = gameObject.transform.position.z;
			float deci;
			x1 = (float)Math.Round (x1);
			y1 = (float)Math.Round (y1,1);
			//if it's in an odd column, round to the nearest 0.5, else round to the nearest whole number
			int tempx = (int)Math.Abs (x1);
			if(tempx % 2 == 1)
			{
				deci = y1 * 10 % 10;
				if(deci != 5 || deci != -5){
					if(y1 > 0)
					{
						y1 = (float) Math.Truncate(y1);
						y1 += 0.5f;
					}
					else
					{
						y1 = (float) Math.Truncate(y1);
						y1 -= 0.5f;
					}
				}
			}
			else
			{
				y1 = (float)Math.Round (y1);
			}
			this.transform.position = new Vector3 (x1, y1, z1);
		}
		prevPosition = this.transform.position;
	}
}
