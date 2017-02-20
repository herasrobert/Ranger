using UnityEngine;
using System.Collections;

public class CreditSceneController : MonoBehaviour {

	public Texture2D backgroundImage;
	public Texture playButtonImage;
	public Texture mainMenuButtonImage;
	
	GameObject soundController;
	SoundController soundControllerScript;

	public Font zombieFont;	

	void Start () {
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
	}

	void OnGUI(){
		GUI.skin.font = zombieFont;//Sets Font
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundImage, ScaleMode.StretchToFill);	

		if (GUI.Button (new Rect (10, 560, 150, 33), mainMenuButtonImage, GUIStyle.none)) {//Main Menu Button
			soundControllerScript.playSFX ("bubbleClick");
			Application.LoadLevel ("MainMenu");
		}

		if (GUI.Button (new Rect (645, 560, 150, 33), playButtonImage, GUIStyle.none)) {
			soundControllerScript.playSFX ("bubbleClick");
			Application.LoadLevel ("Game");
		}	
	}
}
