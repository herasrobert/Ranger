using UnityEngine;
using System.Collections;

public class InstructionsSceneController : MonoBehaviour {

	public Texture playButtonImage;
	public Texture instructionScenebackground;
	
	GameObject soundController;
	SoundController soundControllerScript;

	void Start(){
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
	}

	void Update(){}
	void OnGUI(){
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), instructionScenebackground, ScaleMode.StretchToFill);

		if (GUI.Button (new Rect (645, 560, 150, 33), playButtonImage,GUIStyle.none)){
			soundControllerScript.playSFX("bubbleClick");
			Application.LoadLevel("Game");
		}
	}


}
