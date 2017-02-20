using UnityEngine;
using System.Collections;

public class StudioSplashScreenController : MonoBehaviour {

	public Texture studioSplashScreen;
	float timer = 2f;
	float fbLoginTimer = 3f;

	void Start(){}

	void Update () {
		if (FB.IsInitialized || fbLoginTimer < 0) { // Wait for FB to initialize and then wait 2 seconds for splash, 
			//or if FB hasn't initialized within 3 seconds, start the 2 second timer of splash - Basically allows game to work without FB Initializing & outside of Canvas & Unity
			timer -= Time.deltaTime;
		} else {
			fbLoginTimer -= Time.deltaTime;
		}

		if(timer < 0){
			//GUI.color = Color.white;
			Application.LoadLevel("MainMenu");
		}
	}

	void OnGUI(){		
		//GUI.color = Color.Lerp(Color.white, Color.black, Time.time/2f);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), studioSplashScreen, ScaleMode.StretchToFill);
	

	}

	

}
