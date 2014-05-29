using UnityEngine;
using System.Collections;

public class Data_Carry : MonoBehaviour {
	//Units available to carry into gamemode
	public int t34; //Russian light armor infantry tank
	public int panther; //German light armor infantry tank

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
