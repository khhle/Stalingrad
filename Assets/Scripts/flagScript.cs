using UnityEngine;
using System.Collections;

public class flagScript : MonoBehaviour {

	public int teamNumber = 0;
	public Sprite ger;
	public Sprite rus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (teamNumber == 1)
			GetComponent<SpriteRenderer> ().sprite = rus;
		else if (teamNumber == 2)
			GetComponent<SpriteRenderer> ().sprite = ger;
		else if (teamNumber == 3)
			GetComponent<SpriteRenderer> ().enabled = false;
	}
}
