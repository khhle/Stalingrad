using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public AudioClip hoverOver;
	private string lastTip = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(GUI.Button (new Rect (430, 275, 100, 50), new GUIContent("PLAY", "play"))){
			Application.LoadLevel("Selection");
		}
		if(Event.current.type == EventType.Repaint && GUI.tooltip != lastTip){
			if (GUI.tooltip == "play")
				audio.PlayOneShot (hoverOver);
			if (lastTip != "")
				lastTip = "";
			else
				lastTip = "play";
		}
	}
}
