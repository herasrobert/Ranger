using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {
	string playerName = "Name?";
	//int currentRP = 0;
	int jumpScareToggle = 1;
	int adultOverlayShown = 0;

	bool displayOptions = false;
	bool clearCheck = false;

	GUIStyle myStyle;

	public Font defaultFont;

	public Texture mainMenubackground;
	public Texture playButtonImage;
	public Texture optionsButtonImage;
	public Texture adultOverlayBackground;
	public Texture checkMarkButton;
	public Texture xButton;
	public Texture tosButton;
	public Texture eulaButton;
	public Texture exitButtonImage;


	GameObject optionsOverlay;
	GameObject soundController;
	GameObject rpController;
	SoundController soundControllerScript;
	OptionsOverlayController optionsOverlayController;
	RPController rpControllerScript;
	
	Dictionary<string, string> profile = null;


	void Start () {
		Application.targetFrameRate = 60;
		Time.captureFramerate = 60;
		optionsOverlay = GameObject.Find ("OptionsOverlay");
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
		optionsOverlayController = optionsOverlay.GetComponent<OptionsOverlayController>();
		rpController = GameObject.Find ("RPController");

		if (PlayerPrefs.HasKey ("PlayerName")){		
			playerName = PlayerPrefs.GetString ("PlayerName");
		} else {
			if(FB.IsLoggedIn){ // If logged in - get name from FB
				FB.API("/me?fields=id,first_name",Facebook.HttpMethod.GET,NameCallBack);//playerName = Facebook name
			}else {
				playerName = "Name?";
			}
		}
		/*if (PlayerPrefs.HasKey ("RP") == false) {
			PlayerPrefs.SetInt("RP", currentRP);
		}*/
		if (PlayerPrefs.HasKey ("JumpScare") == false) {
			PlayerPrefs.SetInt("JumpScare", jumpScareToggle);
		}
		if (PlayerPrefs.HasKey ("AdultOverlay") == false) {
			PlayerPrefs.SetInt ("AdultOverlay", 0);
		} else {
			adultOverlayShown = PlayerPrefs.GetInt("AdultOverlay");
		}
		optionsOverlayController.displayOverlayOff();
		displayOptions = false;
	}

	void LogCallback(FBResult response) {
		Debug.Log(response.Text);
	}

	void Update(){}
	public int x = 100;
	public int y = 100;
	void OnGUI(){
		GUI.skin.font = defaultFont;
		myStyle = new GUIStyle (GUI.skin.textField); // Set Text GUI Style
		myStyle.alignment = TextAnchor.MiddleCenter; // Set Text to Center Alignment

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), mainMenubackground, ScaleMode.StretchToFill);

		if(adultOverlayShown == 0 || clearCheck){ // Disable Play Button and Options Button if a pop-up needs to come up
			GUI.enabled = false;
		}
		playerName = GUI.TextField(new Rect(347, 310, 105, 20), playerName, 10, myStyle); // Draw Player Name Text Field

		if (GUI.Button (new Rect (325, 350, 150, 33), playButtonImage,GUIStyle.none)){
			if (playerName.Equals("Name?")){//If name hasn't been changed from default text
				PlayerPrefs.SetString("PlayerName","Player");
			}else {//If Name has been changed
				PlayerPrefs.SetString("PlayerName",playerName);
			}
			//PlayerPrefs.SetInt("Gender", gender);//Sets Gender
			soundControllerScript.playSFX("bubbleClick");
			Application.LoadLevel("Instructions");
		}

		if(optionsOverlayController.getDisplayOverlay()){
			GUI.enabled = false;
		}

		if (GUI.Button (new Rect (325, 400, 150, 33), optionsButtonImage, GUIStyle.none)) {	// On Right
			optionsOverlayController.displayOverlayOn();
			displayOptions = true;
			soundControllerScript.playSFX("bubbleClick");
		}
		GUI.enabled = true;
		if(displayOptions){
			optionsOverlayController.display(); // Display Options Overlay - Calling it from here so it displayed above background texture
		}
		if (GUI.Button (new Rect (325, 450, 150, 33), exitButtonImage, GUIStyle.none)) {	// On Right
			soundControllerScript.playSFX("bubbleClick");
			Application.Quit();
		}
		GUI.enabled = true;
		if(adultOverlayShown == 0){
			GUI.depth = -1;
			//GUI.DrawTexture (new Rect (275, 285, 250, 170), adultOverlayBackground, ScaleMode.StretchToFill);
			GUI.Label (new Rect (105, 305, 600, 135), new GUIContent("This game may include content not suitable for children and \n deleting cookies and/or temporary files may delete game data.\n You also agree to the terms set forth in our End User License Agreement & Terms of Service. \n Are you ok with this?"),myStyle);//Draw Scraps Number

			if (GUI.Button (new Rect (120, 395, 39, 39), checkMarkButton, GUIStyle.none)) {	// On Right				
				optionsOverlayController.setAdultContentOn();
				PlayerPrefs.SetInt ("AdultOverlay", 1);
				adultOverlayShown = 1;
				soundControllerScript.playSFX("bubbleClick");
			}

			/*if (GUI.Button (new Rect (230, 400, 75, 33), tosButton, GUIStyle.none)) {	// On Right
				Application.ExternalEval("window.open('http://www.playeditstudios.com/termsofservice.html','_blank')");
				soundControllerScript.playSFX("bubbleClick");
			}

			if (GUI.Button (new Rect (500, 400, 75, 33), eulaButton, GUIStyle.none)) {	// On Right
				Application.ExternalEval("window.open('http://www.playeditstudios.com/Ranger/Ranger_EULA.html','_blank')");
				soundControllerScript.playSFX("bubbleClick");
			}*/

			if (GUI.Button (new Rect (650, 395, 39, 39), xButton, GUIStyle.none)) {	// On Right
				PlayerPrefs.SetInt ("AdultOverlay", 1);
				adultOverlayShown = 1;
				optionsOverlayController.setAdultContentOff();
				soundControllerScript.playSFX("bubbleClick");
			}
			
			GUI.depth = 0;
		}

		if(clearCheck){
			GUI.depth = -1;
			GUI.Label (new Rect (105, 305, 600, 135), new GUIContent("This will clear all data including items purchased with real money.\n Are you ok with this?"),myStyle);//Draw Scraps Number
			
			if (GUI.Button (new Rect (120, 395, 39, 39), checkMarkButton, GUIStyle.none)) {	// On Right				
				PlayerPrefs.DeleteAll();//Delete all PlayerPrefs
				if(rpController != null){
					rpControllerScript = rpController.GetComponent<RPController>();					
					rpControllerScript.setRP(2200);
				}
				Start();
				clearCheck = false;
				soundControllerScript.playSFX("bubbleClick");
			}
			
			if (GUI.Button (new Rect (650, 395, 39, 39), xButton, GUIStyle.none)) {	// On Right
				clearCheck = false;
				soundControllerScript.playSFX("bubbleClick");
			}
			GUI.depth = 0;
		}
		GUI.Label (new Rect (5, 575, 90, 20), "Ranger v1");//Draw Scraps Number

	}

	public void clear(){
		clearCheck = true;	
		optionsOverlayController.displayOverlayOff();
		displayOptions = false;
	}

	void NameCallBack(FBResult result){
		if (result.Error != null) { // If there is an Error because Error value did not return null, instead it returned an error
			Debug.Log ("Error getting player name from FB.");
			//FB.API("/me?fields=id,fist_name",Facebook.HttpMethod.GET,NameCallBack);//playerName = Facebook name
			return;
		}
		profile = Util.DeserializeJSONProfile (result.Text);
		playerName = profile ["first_name"];
	}



}

