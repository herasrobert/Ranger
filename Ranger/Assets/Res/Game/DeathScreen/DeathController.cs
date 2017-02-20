using UnityEngine;
using System.Collections;
public class DeathController : MonoBehaviour {

	int displayFreeRPBubble = 1;

	string playerName = "Player";

	GameObject player;
	PlayerController playerControllerScript;
	public Texture2D backgroundImage;
	public Texture playButtonImage;
	public Texture mainMenuButtonImage;
	public Texture creditSceneButtonImage;
	public Texture shareButtonImage;
	public Texture inviteButtonImage;
	public Texture freeRPBubbleImage;
	public Texture buyRPButtonImage;
	
	GameObject soundController;
	SoundController soundControllerScript;	
	GameObject rpController;
	RPController rpControllerScript;
	GameObject zombieSpawner;
	ZombieSpawner zombieSpawnerScript;
	SecondaryController secondaryController;	
	PrimaryController primaryController;
	GameObject storeObject;
	StoreController storeControllerScript;
	PrimaryButtonsController primaryButtonsController;
	SecondaryButtonsController secondaryButtonsController;
	SurvivorButtonsController survivorButtonsController;
	public Font zombieFont;

	void Start () {
		player = GameObject.Find("Player"); //Reference to Player Game Object
		playerControllerScript = player.GetComponent<PlayerController>(); // Reference to PlayerController Script
		secondaryController = player.GetComponent<SecondaryController>();
		primaryController = player.GetComponent<PrimaryController>();

		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
		rpController = GameObject.Find ("RPController");
		rpControllerScript = rpController.GetComponent<RPController>();
		
		zombieSpawner = GameObject.Find ("ZombieSpawner");
		zombieSpawnerScript = zombieSpawner.GetComponent<ZombieSpawner>();

		storeObject = GameObject.Find ("Store");
		storeControllerScript = storeObject.GetComponent<StoreController>();
		primaryButtonsController = storeObject.GetComponent<PrimaryButtonsController>();
		secondaryButtonsController = storeObject.GetComponent<SecondaryButtonsController>();
		survivorButtonsController = storeObject.GetComponent<SurvivorButtonsController>();

		if(PlayerPrefs.HasKey("PlayerName")){			
			playerName = PlayerPrefs.GetString("PlayerName");
		}

		if (PlayerPrefs.HasKey ("DisplayFreeRPBubble")) {			
			displayFreeRPBubble = PlayerPrefs.GetInt ("DisplayFreeRPBubble");
		} else {
			displayFreeRPBubble = 1;
		}
	}

	void Update(){}

	public int x = 110;
	public int y = 345;
	public int i = 81;
	public int j = 71;

	void OnGUI(){
		if(playerControllerScript.isAlive() == false){
			GUI.skin.font = zombieFont;//Sets Font
			Time.timeScale = 0.0f;//Pause Game
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundImage, ScaleMode.StretchToFill);	
			/*GUI.Label (new Rect (345, 315, 300, 30), "YOUR RESULTS");
			GUI.Label (new Rect (352, 345, 300, 30), "SCORE - " + playerControllerScript.getCurrentScore());
			GUI.Label (new Rect (328, 375, 300, 30), "ZOMBIES KILLED - " + playerControllerScript.howManyZombiesKilled());
			GUI.Label (new Rect (317, 405, 300, 30), "ROUNDS COMPLETED - " + (playerControllerScript.getCurrentRound()-1));*/

			
			var centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperCenter;
			GUI.Label (new Rect (Screen.width/2-150, 315, 300, 50), ""+playerName.ToUpper()+ " RESULTS", centeredStyle);

			//GUI.Label (new Rect (325, 315, 300, 30), ""+playerName.ToUpper()+ " RESULTS"); //Player Name
			GUI.Label (new Rect (Screen.width/2-150, 345, 300, 30), "SCORE", centeredStyle);
			GUI.Label (new Rect (Screen.width/2-150, 375, 300, 30), ""+playerControllerScript.getCurrentScore(), centeredStyle);
			GUI.Label (new Rect (Screen.width/2-150, 405, 300, 30), "ZOMBIES KILLED", centeredStyle);
			GUI.Label (new Rect (Screen.width/2-150, 435, 300, 30), "" + playerControllerScript.howManyZombiesKilled(), centeredStyle);
			GUI.Label (new Rect (Screen.width/2-150, 465, 300, 30), "ROUNDS COMPLETED", centeredStyle);
			GUI.Label (new Rect (Screen.width/2-150, 495, 300, 30), "" + (playerControllerScript.getCurrentRound()-1), centeredStyle);



			if (GUI.Button (new Rect (10, 560, 150, 33), mainMenuButtonImage,GUIStyle.none)){//Main Menu Button
				soundControllerScript.playSFX("bubbleClick");
				Application.LoadLevel("MainMenu");
			}

			if (GUI.Button (new Rect (645, 560, 150, 33), playButtonImage,GUIStyle.none)){
				soundControllerScript.playSFX("bubbleClick");
				//Application.LoadLevel("Game");
				//Start ();//Reset Death Controller
				playerControllerScript.reset();//Reset Player
				zombieSpawnerScript.reset();//Reset Zombie Spawner
				primaryController.reset();//Reset Player Primary
				secondaryController.reset();//Reset Player Secondary
				storeControllerScript.reset();//Reset Store
				primaryButtonsController.reset();//Reset Store Primary
				secondaryButtonsController.reset();//Reset Store Secondary
				survivorButtonsController.reset();//Reset Store Survivors
				Time.timeScale = 1f;
			}	
			/*if (GUI.Button (new Rect (330, 560, 150, 33), buyRPButtonImage,GUIStyle.none)){//Main Menu Button
				soundControllerScript.playSFX("bubbleClick");
				/*FB.Canvas.Pay(product: "https://www.playeditstudios.com/ranger/purchaserp.html",
				              action: "purchaseitem",
				              quantity: 1,
				              callback: PayCallback);
				rpControllerScript.paymentDialog();
			}*/

			if (GUI.Button (new Rect (770, 5, 23, 23), creditSceneButtonImage,GUIStyle.none)){
				soundControllerScript.playSFX("bubbleClick");
				Application.LoadLevel("creditScene");
			}

			if(FB.IsLoggedIn){
				if (GUI.Button (new Rect (170, 560, 150, 33), shareButtonImage,GUIStyle.none)){//Main Menu Button
					soundControllerScript.playSFX("bubbleClick");
					ShareWithFriends();
				}
				displayFreeRPBubble = 1;
				if(displayFreeRPBubble == 1){
					GUI.DrawTexture (new Rect (235, 485, 81, 71), freeRPBubbleImage, ScaleMode.StretchToFill);	
				}

				if (GUI.Button (new Rect (487, 560, 150, 33), inviteButtonImage,GUIStyle.none)){//Main Menu Button
					soundControllerScript.playSFX("bubbleClick");
					InviteFriends();
				}
			}

			centeredStyle.alignment = TextAnchor.UpperLeft;
		}	
	}

	public void ShareWithFriends(){
		FB.Feed(
			linkCaption: "Check out this awesome game!",
			picture: "http://playeditstudios.com/Ranger/RangerLogo.jpg",
			linkName: "I survived "+ (playerControllerScript.getCurrentRound()-1) +" rounds, I scored " + playerControllerScript.getCurrentScore() +" points and I killed " + playerControllerScript.howManyZombiesKilled() + " zombies!",
			link: "http://apps.facebook.com/"+FB.AppId+"/?challenge_brag="+(FB.IsLoggedIn ? FB.UserId : "guest")
			);
	}

	public void InviteFriends(){
		FB.AppRequest (
			message: "This game is awesome, join me. now.",
			title: "Invite your friends to join you"
			);
	}
	
	void ShareCallBack(FBResult result){
		if (result.Error != null) { // If there is an Error because Error value did not return null, instead it returned an error
			Debug.Log ("Error sharing on FB.");
			//FB.API("/me?fields=id,fist_name",Facebook.HttpMethod.GET,NameCallBack);//playerName = Facebook name
			return;
		} else { // No Error, so give RP
			if(displayFreeRPBubble == 1){
				rpControllerScript.addRP(280);
				displayFreeRPBubble = 0;
				PlayerPrefs.SetInt ("DisplayFreeRPBubble",displayFreeRPBubble);
			}
		}
	}
	/*void PayCallback(FBResult result){
		if (result != null){
			var response = Json.Deserialize(result.Text) as Dictionary<string, object>;
			if(Convert.ToString(response["status"]) == "completed"){
				rpControllerScript.addRP(2800);//Give RP
			}
			else{
				Debug.Log("PayCallback in DeathControllerScript: Payment Failed");//Payment Failed Message
			}
		}
	}*/

}