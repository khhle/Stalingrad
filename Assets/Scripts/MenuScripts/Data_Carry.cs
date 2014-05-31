using UnityEngine;
using System.Collections;

public class Data_Carry : MonoBehaviour {
	//Units available to carry into gamemode
	public int russianAT; //Russian anti tank infantry
	public int russianBomber; //Russian plane bomber
	public int russianCannon; //Russian anti air turret
	public int russianFighter; //Russian fighter plane
	public int russianSquad; //Russian infantry squad
	public int t28; // Russian light armor anti infantry tank
	public int t34; //Russian long range tank destroying tank
	public int t60; //Russian anti air tank

	public int flak30; //Russian anti air turret
	public int germanAT; //Russian anti tank infantry
	public int germanBomber; //German Bomber plane
	public int germanFighter; //German fighter plane
	public int germanSquad; //German infantry squad
	public int panther; //German long range tank destroying tank
	public int panzer4; //German light armor anti infantry tank
	public int wirbelwind; //German anti air tank

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
