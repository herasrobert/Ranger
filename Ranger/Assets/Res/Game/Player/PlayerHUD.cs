using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;


public class PlayerHUD : MonoBehaviour {
	int roundIconX = 0;
	int zombieIconX = 0;

	bool runOnce = true;
	bool displayOptions = false;

	string playerName = "Player";

	GameObject player;
	GameObject ZombieSpawner;
	//Spawner spawnerScript;
	PlayerController playerScript;
	PrimaryController primaryController;
	public GameObject storeObject;
	StoreController storeControllerScript;
	public Texture playerHUD;
	GameObject optionsOverlay;
	OptionsOverlayController optionsOverlayController;

	public Texture portraitOne;
	public Texture portraitTwo;
	public Texture portraitThree;
	public Texture portraitFour;

	/// <Health Bar Variables Start>
	float barDisplay; //current progress
	Vector2 pos = new Vector2(65,10);
	Vector2 size = new Vector2(100,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	/// </Health Bar Variables End>	
	
	/// <Laser Cannon Charge Bar Variables Start>
	float laserBarDisplay; //current progress
	Vector2 laserBarPos = new Vector2(166,43);
	Vector2 laserBarSize = new Vector2(98,20);
	public Texture2D laserEmptyTex;
	public Texture2D laserFullTex;
	/// </Laser Cannon Charge Bar Variables End>	

	/// <Icons Variables Start>
	public Texture scrapsIcon;
	public Texture rpIcon;
	public Texture ammoIcon;

	public Texture pistolIcon;
	public Texture rifleIcon;
	public Texture shotGunIcon;
	public Texture rocketIcon;
	public Texture uziIcon;
	public Texture grenadeIcon;
	public Texture gatlingIcon;
	public Texture crossBowIcon;
	public Texture bolterIcon;
	public Texture dCannonIcon;
	public Texture lCannonIcon;
	public Texture roundIcon;
	public Texture zombieCountIcon;
	public Texture startButtonIcon;
	public Texture nextWaveButtonIcon;
	public Texture timeTillWaveIcon;
	public Texture pauseButton;
	public Texture unPauseButtonImage;
	public Texture optionsButtonImage;
	public Texture mainMenuButtonImage;
	public Texture buyRPButtonImage;
	/// </Icons Variables End>
	Texture avatarImage;

	public Font zombieFont;

	ZombieSpawner zombieSpawnerScript;
	GameObject soundController;
	SoundController soundControllerScript;
	GameObject rpController;
	RPController rpControllerScript;


	void Start () {		
		player = GameObject.Find("Player"); //Reference to Player Game Object
		playerScript = player.GetComponent<PlayerController>();
		primaryController = player.GetComponent<PrimaryController>();		
		storeControllerScript = storeObject.GetComponent<StoreController>();
		ZombieSpawner = GameObject.Find("ZombieSpawner");
		//spawnerScript = ZombieSpawner.GetComponent<Spawner>();
		zombieSpawnerScript = ZombieSpawner.GetComponent<ZombieSpawner>();

		optionsOverlay = GameObject.Find ("OptionsOverlay");
		optionsOverlayController = optionsOverlay.GetComponent<OptionsOverlayController>();
		optionsOverlayController.displayOverlayOff();
		
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();

		rpController = GameObject.Find ("RPController");
		rpControllerScript = rpController.GetComponent<RPController>();


		if(PlayerPrefs.HasKey("PlayerName")){			
			playerName = PlayerPrefs.GetString("PlayerName");
		}


		//FB.API (Util.GetPictureURL ("me", 128, 128), Facebook.HttpMethod.GET, PictureCallBack);


	}

	/*void Update(){
		if (FB.IsLoggedIn) {
			if (avatarImage == null) { // If avatarImage is null, keep trying to get avatarImage
				FB.API (Util.GetPictureURL ("me", 128, 128), Facebook.HttpMethod.GET, PictureCallBack);
			}
		}
	}*/
	public int x = 0;
	public int y = 0;
	public int i = 48;
	public int j = 48;
	public void displayHUD(bool roundEndedCheck, float timeUntilRoundStarts){

		displayTopLeft ();

		if(storeControllerScript.getDisplayStore() == false){
			//GUI.Label (new Rect (380, 250, 150, 25), playerName);
			//GUI.Label (new Rect (380, 250, 200, 50), "" + FB.UserId);//Time before next round begins

			if (GUI.Button (new Rect (770, 5, 23, 23), pauseButton, GUIStyle.none)) { // Pause Button Top-Right
				soundControllerScript.playSFX("bubbleClick");
				playerScript.pauseGame();//When Opening menu, options overlay should start closed - even if it had been opened previously
				optionsOverlayController.displayOverlayOff();
				displayOptions = false;
			}
		}

		if(playerScript.getPaused()){ // If Paused
			if (GUI.Button (new Rect (325, 250, 150, 50), unPauseButtonImage, GUIStyle.none)) { // Pause Button Top-Right
				playerScript.pauseGame();
				soundControllerScript.playSFX("bubbleClick");
			}

			if (GUI.Button (new Rect (325, 325, 150, 33), mainMenuButtonImage, GUIStyle.none)) {	// On Right
				optionsOverlayController.displayOverlayOff();
				soundControllerScript.playSFX("bubbleClick");
				//Load Main Menu but do not duplicate Sound Controller or OptionsOverlay
				Application.LoadLevel("MainMenu");
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
				optionsOverlayController.display();
			}
			/*if (GUI.Button (new Rect (325, 475, 150, 33), buyRPButtonImage,GUIStyle.none)){//Main Menu Button
				soundControllerScript.playSFX("bubbleClick");
				/*FB.Canvas.Pay(product: "https://www.playeditstudios.com/ranger/purchaserp.html",
				              action: "purchaseitem",
				              quantity: 1,
				              callback: PayCallback);
				rpControllerScript.paymentDialog();//rp controller call for pay
			}*/
		}	
		///Top Buttons Start
		if (storeControllerScript.getDisplayStore() == false) {	
			if (roundEndedCheck == false) {//If In the Middle of a Round
				drawZombieIcon();//Draw Zombie Icon On Right
				drawCurrentRound ();//Draw Current Round Icon on Left
			} else { //Round Over
				if (runOnce) {
					if (GUI.Button (new Rect (400, 0, 64, 64), startButtonIcon, GUIStyle.none)) { // On Right
						playerScript.setRoundEndedCheck (false);
						playerScript.setStartRoundTimer (true);
						runOnce = false;
						soundControllerScript.playSFX("bubbleClick");
					}
				} else {
					if (GUI.Button (new Rect (400, 0, 64, 64), nextWaveButtonIcon, GUIStyle.none)) {	// On Right
						//spawnerScript.setRoundTimerToZero();
						playerScript.resetTimeUntilRoundStarts();

						soundControllerScript.playSFX("bubbleClick");
					}
				}
				drawTimerTillRoundStart(timeUntilRoundStarts);//Draw Timer Until Round Starts on Left
			}
		}
		///Top Buttons End

		//HUD Tool Tips Start
		if(storeControllerScript.getDisplayStore() == false){		
			if (GUI.tooltip == "Scraps are earned from killing zombies, you can spend the scraps in the store.") {
				GUI.Label(new Rect (10,60,130,75), GUI.tooltip); // tool tip Draw
			}else if(GUI.tooltip == "Research Points (RP)- gathered every minute, to spend visit the store"){
				GUI.Label(new Rect (60,60,130,75), GUI.tooltip); // tool tip Draw
			}else if(GUI.tooltip == "Ammo"){
				GUI.Label(new Rect (200,55,130,75), GUI.tooltip); // tool tip Draw			
			}
		}
		//HUD Tool Tips End

	}//On GUI End

	public void displayTopLeft(){
		GUI.DrawTexture(new Rect(0,0,275,75),playerHUD,ScaleMode.StretchToFill);//Draw HUD Background
		drawPortrait();//Draw Portrait
		drawHealthBar();//Draw Health Bar
		drawScraps();//Draw Scraps
		drawRP();//Draw Current RP
		drawPrimary();//Draw Primary
		drawAmmo();//Draw Ammo Underneath
	}

	void drawPortrait(){
		if (FB.IsLoggedIn) { //  If Logged into Facebook
			if(avatarImage != null){ //If Avatar Image is not null, load it at portrait, else load regular portrait
				GUI.DrawTexture(new Rect(9,7,52,50),avatarImage,ScaleMode.StretchToFill);
			}else {
				GUI.DrawTexture(new Rect(10,10,48,48),portraitOne,ScaleMode.StretchToFill);
				FB.API (Util.GetPictureURL ("me", 128, 128), Facebook.HttpMethod.GET, PictureCallBack);
			}
		} else {
			if (playerScript.getCurrentHealth() > 74) { // If 75-100
				GUI.DrawTexture(new Rect(10,10,48,48),portraitOne,ScaleMode.StretchToFill);//Draw Portrait One
			} else if (playerScript.getCurrentHealth() > 49) { //If 50-75
				GUI.DrawTexture(new Rect(10,10,48,48),portraitTwo,ScaleMode.StretchToFill);//Draw Portrait Two
			} else if (playerScript.getCurrentHealth() > 24) {//If 25-50
				GUI.DrawTexture(new Rect(10,10,48,48),portraitThree,ScaleMode.StretchToFill);//Draw Portrait Three
			} else { // If Below 25
				GUI.DrawTexture(new Rect(10,10,48,48),portraitFour,ScaleMode.StretchToFill);//Draw Portrait Four
			}		
		}
	}
	void drawHealthBar(){
		GUI.skin.box.normal.background = null; //Resets it to Null so it doesn't get affected by other GUI's such as Store DrawQuad
		barDisplay = playerScript.getCurrentHealth()/100f;
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);		
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
			GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
			GUI.EndGroup();
		GUI.EndGroup();
		GUI.skin.box.normal.background = null;//Resets it to Null so it doesn't affect other GUI's such as Store
	}
	void drawScraps(){GUI.skin.box.normal.background = null;//Resets it to Null so it doesn't affect other GUI's such as Store
		playerScript.DrawQuad (new Rect(70,35,90,0),Color.black);
		GUI.Label (new Rect (72, 25, 90, 20),new GUIContent("" + playerScript.getCurrentScraps(), "Scraps are earned from killing zombies, you can spend the scraps in the store."));//Draw Scraps Number
		//GUI.Label (new Rect (72, 25, 65, 20), "" + playerScript.getCurrentScraps());//Draw Scraps Number
		GUI.DrawTexture(new Rect(147,29,12,12),scrapsIcon,ScaleMode.StretchToFill);//Draw Scraps Icon
		GUI.skin.box.normal.background = null;//Resets it to Null so it doesn't affect other GUI's such as Store
	}
	void drawCurrentRound(){
		GUI.skin.font = zombieFont;//Sets Font
		/*if(playerScript.getCurrentRound() < 10){ // If statements to Adjust for multiple digits
			roundIconX = 300;
		}else if (playerScript.getCurrentRound() < 100){
			roundIconX = 306;
		}else if (playerScript.getCurrentRound() < 1000){
			roundIconX = 308;
		}*/
		roundIconX = 300;
		GUI.DrawTexture(new Rect(roundIconX,0,64,64),roundIcon,ScaleMode.StretchToFill);//Draw Gun - possibly 172 for X value
		GUI.Label (new Rect (roundIconX+28, 33, 50, 50), ""+playerScript.getCurrentRound());		
		GUI.skin.font = null;//Resets it to Null so it doesn't affect other GUI's such as Store
	}
	void drawRP(){
		playerScript.DrawQuad (new Rect(70,52,90,0),Color.black);
		GUI.Label (new Rect (72, 42, 90, 20), new GUIContent(""+rpControllerScript.getCurrentRP(),"Research Points (RP)- gathered every minute, to spend visit the store"));//Draw Scraps Number
		GUI.DrawTexture(new Rect(147,46,12,12),rpIcon,ScaleMode.StretchToFill);//Draw Scraps Icon
		GUI.skin.box.normal.background = null;//Resets it to Null so it doesn't affect other GUI's such as Store
	}
	void drawPrimary(){
		GUI.skin.box.normal.background = null;//Resets it to Null so it doesn't affect other GUI's such as Store

		if(primaryController.getCurrentlyEquipedWeapon() == 0){
			GUI.DrawTexture(new Rect(170,10,90,35),pistolIcon,ScaleMode.StretchToFill);//Draw Gun - possibly 172 for X value
		}else if(primaryController.getCurrentlyEquipedWeapon() == 1){
			GUI.DrawTexture(new Rect(170,10,90,35),rifleIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 2){
			GUI.DrawTexture(new Rect(170,10,90,35),shotGunIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 3){
			GUI.DrawTexture(new Rect(170,10,90,35),rocketIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 4){
			GUI.DrawTexture(new Rect(170,10,90,35),uziIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 5){
			GUI.DrawTexture(new Rect(170,10,90,35),grenadeIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 6){
			GUI.DrawTexture(new Rect(170,10,90,35),gatlingIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 7){
			GUI.DrawTexture(new Rect(170,10,90,35),crossBowIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 8){
			GUI.DrawTexture(new Rect(170,10,90,35),bolterIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 9){
			GUI.DrawTexture(new Rect(170,10,90,35),dCannonIcon,ScaleMode.StretchToFill);//Draw Gun
		}else if(primaryController.getCurrentlyEquipedWeapon() == 10){
			GUI.DrawTexture(new Rect(170,10,90,35),lCannonIcon,ScaleMode.StretchToFill);//Draw Gun
		}
		GUI.skin.box.normal.background = null;//Resets it to Null so it doesn't affect other GUI's such as Store
	}
	void drawAmmo(){
		GUI.skin.box.normal.background = null;//Resets it to Null so it doesn't affect other GUI's such as Store

		if(primaryController.getCurrentlyEquipedWeapon() == 0){ // If Pistol, Infinite Bullets
			playerScript.DrawQuad (new Rect(170,52,90,0),Color.black);
			GUI.Label (new Rect (172, 42, 90, 20), new GUIContent("Infinite","Ammo"));//Draw Scraps Number
			GUI.DrawTexture (new Rect (247, 46, 12, 12), ammoIcon, ScaleMode.StretchToFill);//Draw Scraps Icon
		}else if (primaryController.getCurrentlyEquipedWeapon () != 10) { // Draw for any weapon that isn't laser cannon since laser cannon has charge not ammo
			playerScript.DrawQuad (new Rect(170,52,90,0),Color.black);
			GUI.Label (new Rect (172, 42, 90, 20), new GUIContent("" + primaryController.getAmmo (primaryController.getCurrentlyEquipedWeapon ()),"Ammo"));//Draw Scraps Number
			GUI.DrawTexture (new Rect (247, 46, 12, 12), ammoIcon, ScaleMode.StretchToFill);//Draw Scraps Icon
		}else {//Draw for Laser Cannon
			laserBarDisplay = primaryController.getLaserCharge() / primaryController.getLaserRate();
			GUI.BeginGroup(new Rect(laserBarPos.x, laserBarPos.y, laserBarSize.x, laserBarSize.y));
			GUI.Box(new Rect(0,0, laserBarSize.x, laserBarSize.y), laserEmptyTex);		
				//draw the filled-in part:
				GUI.BeginGroup(new Rect(0,0, laserBarSize.x * laserBarDisplay, laserBarSize.y));
				GUI.Box(new Rect(0,0, laserBarSize.x, laserBarSize.y), laserFullTex);
			GUI.EndGroup();
			GUI.EndGroup();
		}
		GUI.skin.box.normal.background = null;//Resets it to Null so it doesn't affect other GUI's such as Store
	}
	void drawZombieIcon(){
		GUI.skin.font = zombieFont;//Sets Font
		/*if(playerScript.getCurrentRound() < 10){ // If statements to Adjust for multiple digits
			zombieIconX = 400;
		}else if (playerScript.getCurrentRound() < 100){
			zombieIconX = 406;
		}else if (playerScript.getCurrentRound() < 1000){
			zombieIconX = 408;
		}*/
		zombieIconX = 400;
		GUI.DrawTexture(new Rect(zombieIconX,0,64,64),zombieCountIcon,ScaleMode.StretchToFill);//Draw Gun - possibly 172 for X value
		GUI.Label (new Rect (zombieIconX+24, 33, 50, 50), ""+zombieSpawnerScript.getZombiesAlive());		
		GUI.skin.font = null;//Resets it to Null so it doesn't affect other GUI's such as Store
	}
	void drawTimerTillRoundStart(float timeUntilRoundStarts){
		GUI.skin.font = zombieFont;//Sets Font
		roundIconX = 300;
		GUI.DrawTexture(new Rect(roundIconX,0,64,64),timeTillWaveIcon,ScaleMode.StretchToFill);//Draw Gun - possibly 172 for X value
		if (playerScript.getCurrentRound () == 1 && playerScript.getSetSpawnOnOnce() == false) { // If Round 1 and BEFORE Round 1/RunOnce
			GUI.Label (new Rect (roundIconX + 20, 33, 50, 50), "" + (120 - (int)playerScript.getCurrentTimePassed())); // time Before Firs Round Begins
		} else {
			GUI.Label (new Rect (roundIconX + 20, 33, 50, 50), "" + (int)timeUntilRoundStarts);//Time before next round begins
		}
		GUI.skin.font = null;//Resets it to Null so it doesn't affect other GUI's such as Store
	}

	void PictureCallBack(FBResult result){
		if (result.Error != null) { // If there is an Error because Error value did not return null, instead it returned an error
			Debug.Log ("Error getting profile picture.");
			//FB.API(Util.GetPictureURL("me",48,48),Facebook.HttpMethod.GET,PictureCallBack);//Profile Picture - Can Casue infinite loop
			return;
		}
		avatarImage = result.Texture;
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
