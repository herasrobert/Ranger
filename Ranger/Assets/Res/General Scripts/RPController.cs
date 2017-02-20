using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;

public class RPController : MonoBehaviour {
	int currentRP = 0;	
	float timer =0f;
	public float maxTimer = 60f;
	/*string dataFromServer = "";
	string getRPURL = "";	
	string checkRecordURL = "";
	string updateRecordURL = "";*/

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
		
		if (FindObjectsOfType(GetType()).Length > 1){// Prevent Duplicated of this GameObject due to DontDestroyOnLoad
			Destroy(gameObject);
		}

		/*if (PlayerPrefs.HasKey ("InitialRPAdded")){
			//Do Nothing
		} else {
			addRP(15000); // Add RP			
			PlayerPrefs.SetInt("InitialRPAdded",currentRP); // Add Value to Key
		}*/

		if (PlayerPrefs.HasKey("RP")){
			currentRP = PlayerPrefs.GetInt("RP");
		}else{
			setRP(2200); // Add RP	
		}

		//StartCoroutine(CheckDBForRecord()); // For Web
		//StartCoroutine(GetRPFromDB()); // For Web
	}

	void Update(){
		timer += Time.deltaTime;
		if(timer > maxTimer){
			timer = 0f;
			addRP(100);
		}

	}

	/*void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 150, 33), "AddRP")) {	// On Right
			addRP(1);
		}
	}*/

	public void addRP(int rp){
		currentRP += rp;//Add RP
		PlayerPrefs.SetInt("RP", currentRP);//Save PlayerPref
	}
	public void subtractRP(int rp){
		currentRP -= rp;//Add RP
		PlayerPrefs.SetInt("RP", currentRP);//Save PlayerPref
	}
	public int getCurrentRP(){
		return currentRP;
	}
	public void setRP(int amount){
		currentRP = amount;
	}

	/*
	public void addRP(int rp){
		StartCoroutine (GetRPFromDB());
		currentRP += rp;//Add RP
		PlayerPrefs.SetInt ("RP", currentRP);//Save PlayerPref
		StartCoroutine (UpdateDBRPValue(currentRP));//Save Record to DB
	}
	public void subtractRP(int rp){
		StartCoroutine (GetRPFromDB());
		currentRP -= rp;//Add RP
		PlayerPrefs.SetInt ("RP", currentRP);//Save PlayerPref
		StartCoroutine (UpdateDBRPValue(currentRP));//Save Record to DB
	}
	public void checkRecord(){
		StartCoroutine(CheckDBForRecord());
	}
	public void refreshRP(){
		StartCoroutine (GetRPFromDB());		
	}
	public int getCurrentRP(){
		return currentRP;
	}
	public void paymentDialog(){
		FB.Canvas.Pay(product: "https://www.playeditstudios.com/ranger/purchaserp.html",
		              action: "purchaseitem",
		              quantity: 1,
		              callback: PayCallback);
	}

	IEnumerator GetRPFromDB(){
		if (FB.IsLoggedIn) {
			getRPURL = "https://playeditstudios.com/Ranger/getrp.php?userId=" + FB.UserId;
		} else {
			getRPURL = "https://playeditstudios.com/Ranger/getrp.php?userId=Hunter2379";
		}

		WWW www = new WWW(getRPURL);

		yield return www;		
		// check for errors
		if (www.error == null){
			dataFromServer = www.text;
			if(dataFromServer.Equals("0 results") == false){
				dataFromServer = dataFromServer.Substring(4,dataFromServer.IndexOf('<')-4);
				currentRP = int.Parse(dataFromServer);
				PlayerPrefs.SetInt("RP", currentRP);//Save PlayerPref
			}else {
				Debug.Log("GetRPFromServer Method - 0 Results Returned");
				checkRecord();
			}
		} else {
			Debug.Log("WWW GetRP Error: "+ www.error);
		}
	}

	IEnumerator CheckDBForRecord(){
		if (FB.IsLoggedIn) {
			checkRecordURL = "https://playeditstudios.com/Ranger/checkrecord.php?userId=" + FB.UserId;
		} else {
			checkRecordURL = "https://playeditstudios.com/Ranger/checkrecord.php?userId=Hunter2379";
		}

		WWW www = new WWW(checkRecordURL);
		
		yield return www;		
		// check for errors
		if (www.error == null){
			dataFromServer = www.text;
			if(dataFromServer.Equals("Found") == false){
				Debug.Log("CheckDBForRecord Method - Record Not Found, PHP Script Should have Added It.");
			}
		} else {
			Debug.Log("WWW CheckRecord Error: "+ www.error);
		}
	}

	IEnumerator UpdateDBRPValue(int rpAmount){
		if (FB.IsLoggedIn) {
			updateRecordURL = "https://playeditstudios.com/Ranger/addrp.php?userId=" + FB.UserId+"&rp=" + rpAmount;
		} else {
			updateRecordURL = "https://playeditstudios.com/Ranger/addrp.php?userId=Hunter2379&rp=" + rpAmount;
		}

		WWW www = new WWW(updateRecordURL);
		
		yield return www;		
		// check for errors
		if (www.error == null){
			dataFromServer = www.text;			
			StartCoroutine (GetRPFromDB());	
			if(dataFromServer.Equals("Done") == false){
				Debug.Log("AddRPToDB Method - Done was not printed");
			}
		} else {
			Debug.Log("WWW AddRP Error: "+ www.error);
		}
	}
	void PayCallback(FBResult result){
		if (result != null){
			var response = Json.Deserialize(result.Text) as Dictionary<string, object>;
			if(Convert.ToString(response["status"]) == "completed"){
				addRP(2800);//Give RP
			}
			else{
				Debug.Log("PayCallback in DeathControllerScript: Payment Failed");//Payment Failed Message
			}
		}
	}*/



}
