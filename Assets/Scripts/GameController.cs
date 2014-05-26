using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GUIText playerTurnText;

	//keeps track of whose turn it is
	public int playerTurn;
	// Use this for initialization
	void Start () {
		playerTurn = 1;
	}
	
	// Update is called once per frame
	void Update () {
		playerTurnText.text = "Player " + playerTurn + "'s turn";
	}
}
