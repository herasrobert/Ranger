using UnityEngine;
using System.Collections;

public class FacebookManager : MonoBehaviour {
	void Awake(){		
		FB.Init(SetInit,OnHideUnity);	
		setResolution();
				
		/*DontDestroyOnLoad(transform.gameObject);
		
		if (FindObjectsOfType(GetType()).Length > 1){// Prevent Duplicated of this GameObject due to DontDestroyOnLoad
			Destroy(gameObject);
		}*/
	}

	private void SetInit(){		
		Debug.Log("The Facebook Initialization has completed");		
		//Check if user is logged into Facebook		
		if (FB.IsLoggedIn){			
			Debug.Log("User is logged in");			
		}else{			
			FacebookLogin();			
		}
	}	
	
	void FacebookLogin(){		
		FB.Login("user_about_me, user_birthday", AuthCallback);	
	}
	
	void AuthCallback(FBResult result){		
		if(FB.IsLoggedIn){			
			Debug.Log("Facebook login has worked");			
		}else{			
			Debug.Log ("Facebook login has failed");			
		}		
	}                                                                                        
	
	private void OnHideUnity(bool isGameShown){                                                                                            
		Debug.Log("OnHideUnity");                                                              
		if (!isGameShown)                                                                        
		{                                                                                        
			// pause the game - we will need to hide                                             
			Time.timeScale = 0;                                                                  
		}                                                                                        
		else                                                                                     
		{                                                                                        
			// start the game back up - we're getting focus again                                
			Time.timeScale = 1;                                                                  
		}                                                                                        
	}

	///Set Resolution Section Start
	public string Width = "800";
	public string Height = "600";
	public bool CenterHorizontal = true;
	public bool CenterVertical = false;
	public string Top = "10";
	public string Left = "10";

	public void setResolution(){
		int width;
		if (!int.TryParse(Width, out width)){
			width = 800;
		}
		int height;
		if (!int.TryParse(Height, out height)){
			height = 600;
		}
		float top;
		if (!float.TryParse(Top, out top)){
			top = 0.0f;
		}
		float left;
		if (!float.TryParse(Left, out left)){
			left = 0.0f;
		}
		if (CenterHorizontal && CenterVertical){
			FB.Canvas.SetResolution(width, height, false, 0, FBScreen.CenterVertical(), FBScreen.CenterHorizontal());
		}else if (CenterHorizontal){
			FB.Canvas.SetResolution(width, height, false, 0, FBScreen.Top(top), FBScreen.CenterHorizontal());
		}else if (CenterVertical){
			FB.Canvas.SetResolution(width, height, false, 0, FBScreen.CenterVertical(), FBScreen.Left(left));
		}else{
			FB.Canvas.SetResolution(width, height, false, 0, FBScreen.Top(top), FBScreen.Left(left));
		}
	}
	///Set Resolution Section End




}