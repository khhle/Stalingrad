using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public AudioClip hoverOver;
	private string lastTip = "";
	public GUIText instr;
	public GUIStyle start;
	public AudioClip fire;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void play_game(){
		Application.LoadLevel("Selection");
	}
	void OnGUI(){
		if(GUI.Button (new Rect (790, 520, 100, 50), "", start)){
			audio.PlayOneShot (fire);
			Invoke ("play_game", 1f);
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
