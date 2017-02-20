using UnityEngine;
using System.Collections;

public class StoreController : MonoBehaviour{
	//int currentRP = 0;

	bool displayStore = false;
	bool storeAvailable = true;

	public GameObject player;
	PlayerController playerScript;
	public GameObject ZombieSpawner;
	public Texture2D storeOverlayBackground;
	public Vector2 inventorySlotOneDropDown = Vector2.zero;
	PrimaryButtonsController primaryButtonsController;
	SecondaryButtonsController secondaryButtonsController;
	SurvivorButtonsController survivorButtonsController;
	PlayerHUD hud;

	public Texture storePortrait;
	public Texture equipSquare;
	public Texture equipSquareSecondaries;
	public Texture closeStoreButtonIcon;
	public Texture buyRPButtonImage;
	
	GameObject soundController;
	SoundController soundControllerScript;
	GameObject rpController;
	RPController rpControllerScript;

	void Start (){
		playerScript = player.GetComponent<PlayerController> ();
		hud = player.GetComponent<PlayerHUD>();
		primaryButtonsController = this.GetComponent<PrimaryButtonsController>();
		secondaryButtonsController = this.GetComponent<SecondaryButtonsController>();
		survivorButtonsController = this.GetComponent<SurvivorButtonsController>();
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
		rpController = GameObject.Find ("RPController");
		rpControllerScript = rpController.GetComponent<RPController>();

		//if (PlayerPrefs.HasKey ("RP")) {
		//	currentRP = PlayerPrefs.GetInt ("RP");
		//}

		//rpControllerScript.refreshRP();
	}

	void Update(){}

	void OnGUI (){
		if (displayStore && storeAvailable) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), storeOverlayBackground, ScaleMode.StretchToFill);	
			GUI.DrawTexture (new Rect (65, 85, 150, 113), storePortrait, ScaleMode.StretchToFill);			
		
			if (GUI.Button (new Rect (770, 5, 23, 23), closeStoreButtonIcon, GUIStyle.none)) {	// On Right
				setDisplayStore(false);
				soundControllerScript.playSFX("bubbleClick");
			}

			/*if (GUI.Button (new Rect (740, 5, 23, 23), buyRPButtonImage,GUIStyle.none)){//Main Menu Button
				soundControllerScript.playSFX("bubbleClick");
				/*FB.Canvas.Pay(product: "https://www.playeditstudios.com/ranger/purchaserp.html",
				              action: "purchaseitem",
				              quantity: 1,
				              callback: PayCallback);
				playerScript.pauseGame();//pause game
				setDisplayStore(false);
				rpControllerScript.paymentDialog();//open pay dialog through rp controlelr
			}*/

			primaryButtonsController.displayPrimaryButtons();
			secondaryButtonsController.displaySecondaryButtons();
			survivorButtonsController.displaySurvivorButtons();
			hud.displayTopLeft();
		}//Display Store Ends

		if (GUI.tooltip == "Adds more space to bunker for more survivors") {
			GUI.Label(new Rect (400,410,130,75), GUI.tooltip); // tool tip Draw
		}else if(GUI.tooltip == "Engineers unlock equipment"){
			GUI.Label(new Rect (330,475,130,75), GUI.tooltip); // tool tip Draw
		}else if(GUI.tooltip == "Scrappers give scrap after every round"){
			GUI.Label(new Rect (475,475,130,75), GUI.tooltip); // tool tip Draw			
		}else if(GUI.tooltip == "Hoarders give ammo after every round"){
			GUI.Label(new Rect (620,475,130,75), GUI.tooltip); // tool tip Draw			
		}
			
		
	}//OnGui Ends
	/*
	public void addRP(int RP){
		currentRP += RP;
	}*/

	public void roundEnded(){
		storeAvailable = true; // If no round
		playerScript.roundEnded();//Set RoundEnded in Player Script
	}

	public void roundStarted(){
		storeAvailable = false; // If Round Started
	}
	public void DrawQuad(Rect position, Color color) {//Method to draw rectangle
		/*Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(position, GUIContent.none);*/
	}
	public bool getDisplayStore(){
		return displayStore;
	}
	public void setDisplayStore(bool value){
		displayStore = value;
	}
	public bool getStoreAvailable(){
		return storeAvailable;
	}
	/*public int getCurrentRP(){
		//return currentRP;
		return rpControllerScript.getCurrentRP();
	}*/
	public void subtractCurrentRP(int value){
		//RPController subtractCurrentRP(int Value){}
		//currentRP -= value;
		rpControllerScript.subtractRP(value);
	}
	public void addCurrentRP(int value){
		//RPController addCurrentRP(int Value){}
		//currentRP += value;
		rpControllerScript.addRP(value);
	}
	public void updateRPPref(){
		//RPController updateRPPref(){}
		//rpControllerScript.refreshRP();
		//PlayerPrefs.SetInt ("RP",rpControllerScript.getCurrentRP());
	}

	public void DrawEquippedSquare (int x, int y){
		GUI.DrawTexture (new Rect (x, y, 200, 90), equipSquare, ScaleMode.StretchToFill);
	}
	public void DrawEquippedSquareSecondaries (int x, int y){
		GUI.DrawTexture (new Rect (x, y, 60, 20), equipSquareSecondaries, ScaleMode.StretchToFill);
	}

	public void reset(){
		displayStore = false;
		storeAvailable = true;
		
		//rpControllerScript.refreshRP();
	}	

	/*void PayCallback(FBResult result){
		if (result != null){
			var response = Json.Deserialize(result.Text) as Dictionary<string, object>;
			if(Convert.ToString(response["status"]) == "completed"){
				rpControllerScript.addRP(2800);//Give RP
			}
			else{
				Debug.Log("PayCallback in StoreControllerScript: Payment Failed");//Payment Failed Message
			}
		}
	}*/
}

