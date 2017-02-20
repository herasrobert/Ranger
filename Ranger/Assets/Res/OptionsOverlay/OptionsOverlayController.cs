using UnityEngine;
using System.Collections;

public class OptionsOverlayController : MonoBehaviour {

	bool displayOverlay = false;
	int adultContent = 1;
	//int music = 1;
	//int sound = 1;

	public Texture overlayBackground;
	public Texture musicOnButtonImage;
	public Texture musicOffButtonImage;
	public Texture soundOnButtonImage;
	public Texture soundOffButtonImage;
	public Texture closeOverlayButtonImage;
	public Texture AdultContentOnImage;
	public Texture AdultContentOffImage;
	public Texture clearButton;

	GameObject MainMenuController;
	GameObject soundController;
	SoundController soundControllerScript;
	MainMenuController mainMenuControllerScript;

	void Start(){		
		soundController = GameObject.Find ("SoundController");		
		soundControllerScript = soundController.GetComponent<SoundController>();
		MainMenuController = GameObject.Find ("MainMenuController");
		mainMenuControllerScript = MainMenuController.GetComponent<MainMenuController>();

		if (PlayerPrefs.HasKey ("AdultContent") == false) {			
			PlayerPrefs.SetInt ("AdultContent", 1);
		} else {
			adultContent = PlayerPrefs.GetInt("AdultContent");
		}

		/*if (PlayerPrefs.HasKey ("Music") == false) {			
			PlayerPrefs.SetInt ("Music", 1);
		} else {
			music = PlayerPrefs.GetInt("Music");
		}*/

		/*if (PlayerPrefs.HasKey ("Sound") == false) {			
			PlayerPrefs.SetInt ("Sound", 1);
		} else {
			sound = PlayerPrefs.GetInt("Sound");
		}*/
	}
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1){ // Prevent Duplicated of this GameObject due to DontDestroyOnLoad
			Destroy(gameObject);
		}
	}

	void Update(){}

	public int x = 240;
	public int y = 300;
	public int i = 315;
	public int j = 213;

	/*void OnGUI(){
		if(displayOverlay){
			//GUI.DrawTexture (new Rect (240, 300, 315, 213), overlayBackground, ScaleMode.StretchToFill);
			GUI.DrawTexture (new Rect (500, 400, 205, 125), overlayBackground, ScaleMode.StretchToFill);

			if (GUI.Button (new Rect (675, 405, 23, 23), closeOverlayButtonImage, GUIStyle.none)) {	// Close Button
				displayOverlayOff();
			}

			if(music == 1){
				if (GUI.Button (new Rect (530, 425, 150, 33), musicOffButtonImage, GUIStyle.none)) {	// Close Button
					music = 0;
					PlayerPrefs.SetInt("Music", 0);
				}
			}else {
				if (GUI.Button (new Rect (530, 425, 150, 33), musicOnButtonImage, GUIStyle.none)) {	// Close Button
					music = 1;
					PlayerPrefs.SetInt("Music", 1);
				}
			}

			if(sound == 1){
				if (GUI.Button (new Rect (530, 475, 150, 33), soundOffButtonImage, GUIStyle.none)) {	// Close Button
					sound = 0;
					PlayerPrefs.SetInt("Sound", 0);
				}
			}else {
				if (GUI.Button (new Rect (530, 475, 150, 33), soundOnButtonImage, GUIStyle.none)) {	// Close Button
					sound = 1;
					PlayerPrefs.SetInt("Sound", 1);
				}
			}
		}
	}*/

	public void display(){
		if(displayOverlay){
			//GUI.DrawTexture (new Rect (240, 300, 315, 213), overlayBackground, ScaleMode.StretchToFill);
			GUI.DrawTexture (new Rect (500, 350, 205, 240), overlayBackground, ScaleMode.StretchToFill);


			if (GUI.Button (new Rect (675, 365, 23, 23), closeOverlayButtonImage, GUIStyle.none)) {	// Close Button
				soundControllerScript.playSFX("bubbleClick");
				displayOverlayOff();
			}
			if(soundControllerScript.getMusic() == 1){
				if (GUI.Button (new Rect (530, 395, 150, 33), musicOffButtonImage, GUIStyle.none)) {	// Close Button
					//music = 0;
					PlayerPrefs.SetInt("Music", 0);
					soundControllerScript.setMusicOff(); // Or Set Volume for Music AudioFile to Off
					soundControllerScript.playSFX("hardClick");
					soundControllerScript.stopMusic("backgroundMusic");
				}
			}else {
				if (GUI.Button (new Rect (530, 395, 150, 33), musicOnButtonImage, GUIStyle.none)) {	// Close Button
					//music = 1;
					PlayerPrefs.SetInt("Music", 1);
					soundControllerScript.setMusicOn();
					soundControllerScript.playSFX("hardClick");
					soundControllerScript.playMusic("backgroundMusic");
				}
			}
			
			if(soundControllerScript.getSound() == 1){
				if (GUI.Button (new Rect (530, 445, 150, 33), soundOffButtonImage, GUIStyle.none)) {	// Close Button
					//sound = 0;
					PlayerPrefs.SetInt("Sound", 0);
					soundControllerScript.setSoundOff();
					soundControllerScript.playSFX("hardClick");
				}
			}else {
				if (GUI.Button (new Rect (530, 445, 150, 33), soundOnButtonImage, GUIStyle.none)) {	// Close Button
					//sound = 1;
					PlayerPrefs.SetInt("Sound", 1);
					soundControllerScript.setSoundOn();
					soundControllerScript.playSFX("hardClick");
				}
			}

			if(adultContent == 1){
				if (GUI.Button (new Rect (530, 495, 150, 33), AdultContentOffImage, GUIStyle.none)) {	// Close Button
					PlayerPrefs.SetInt("AdultContent", 0);
					adultContent = 0;
					soundControllerScript.playSFX("hardClick");
				}
			}else {
				if (GUI.Button (new Rect (530, 495, 150, 33), AdultContentOnImage, GUIStyle.none)) {	// Close Button
					PlayerPrefs.SetInt("AdultContent", 1);
					adultContent = 1;
					soundControllerScript.playSFX("hardClick");
				}
			}

			if(Application.loadedLevelName.Equals("MainMenu") == false){
				GUI.enabled = false;
			}

			if (GUI.Button (new Rect (530, 545, 150, 33), clearButton, GUIStyle.none)){				
				MainMenuController = GameObject.Find ("MainMenuController");
				mainMenuControllerScript = MainMenuController.GetComponent<MainMenuController>();
				mainMenuControllerScript.clear();
				soundControllerScript.playSFX("bubbleClick");
			}
			GUI.enabled = true;

		}
	}


	public void displayOverlayOff(){
		displayOverlay = false;
	}
	public void displayOverlayOn(){
		if(!displayOverlay){
			displayOverlay = true;
		}
	}
	public bool getDisplayOverlay(){
		return displayOverlay;
	}
	public void setAdultContentOff(){
		PlayerPrefs.SetInt("AdultContent", 0);
		adultContent = 0;
	}
	public void setAdultContentOn(){
		PlayerPrefs.SetInt("AdultContent", 1);
		adultContent = 1;
	}
	public int getAdultContent(){
		return adultContent;
	}

}



/*
if (GUI.Button (new Rect (525, 305, 23, 23), closeOverlayButtonImage, GUIStyle.none)) {	// Close Button
				displayOverlay = false;
			}

			if(music == 1){
				if (GUI.Button (new Rect (330, 350, 150, 33), musicOffButtonImage, GUIStyle.none)) {	// Close Button
					music = 0;
					PlayerPrefs.SetInt("Music", 0);
				}
			}else {
				if (GUI.Button (new Rect (330, 350, 150, 33), musicOnButtonImage, GUIStyle.none)) {	// Close Button
					music = 1;
					PlayerPrefs.SetInt("Music", 1);
				}
			}

			if(sound == 1){
				if (GUI.Button (new Rect (330, 400, 150, 33), soundOffButtonImage, GUIStyle.none)) {	// Close Button
					sound = 0;
					PlayerPrefs.SetInt("Sound", 0);
				}
			}else {
				if (GUI.Button (new Rect (330, 400, 150, 33), soundOnButtonImage, GUIStyle.none)) {	// Close Button
					sound = 1;
					PlayerPrefs.SetInt("Sound", 1);
				}
			}




 */
