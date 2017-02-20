using UnityEngine;
using System.Collections;

public class SurvivorButtonsController : MonoBehaviour {

	int unallocatedSurvivors = 0;
	int foundSurvivors = 0;
	//int allocatedSurvivors = 0;
	int engineerSurvivors = 0;
	int scrapSurvivors = 0;
	int ammoSurvivors = 0;
	int bunkerLevel = 1;
	int bunkerLevelCounter = 0;
	int maxSurvivors = 0;

	int totalSurvivorDeathCounter = 0;

	int x = 335;
	int y = 460;

	public GameObject player;
	public GameObject storeObject;
	PrimaryController primaryController;
	PlayerController playerScript;
	StoreController storeControllerScript;
	
	GameObject soundController;
	SoundController soundControllerScript;

	GameObject rpController;
	RPController rpControllerScript;


	void Start () {
		player = GameObject.Find("Player");
		soundController = GameObject.Find ("SoundController");
		playerScript = player.GetComponent<PlayerController> ();
		primaryController = player.GetComponent<PrimaryController> ();		
		storeControllerScript = storeObject.GetComponent<StoreController>();
		soundControllerScript = soundController.GetComponent<SoundController>();
		rpController = GameObject.Find ("RPController");
		rpControllerScript = rpController.GetComponent<RPController>();

		if (PlayerPrefs.HasKey ("PurchasedbunkerLevels")) {
			bunkerLevelCounter = PlayerPrefs.GetInt ("PurchasedbunkerLevels");
		}

		bunkerLevel += bunkerLevelCounter;		
		maxSurvivors = bunkerLevel * 2;
	}

	void Update () {
		if(foundSurvivors > maxSurvivors){
			foundSurvivors = maxSurvivors;
		}
	}

	public void displaySurvivorButtons(){
		//Survivors Secion Start
		if (playerScript.getCurrentScraps() > 499){			
			if (GUI.Button (new Rect (x, y, 190, 20), new GUIContent("Upgrade Bunker: 500 Scraps","Adds more space to bunker for more survivors"))) {//Display bunker Button
				bunkerLevel+=1;
				playerScript.subtractScraps(500);
				soundControllerScript.playSFX("buy");
			}
		}else {
			GUI.enabled = false;
			if (GUI.Button (new Rect (x, y, 190, 20), "Not Enough Scraps (500)")){}//Display bunker Button}
		}
		GUI.enabled = true;
		if (rpControllerScript.getCurrentRP() > 649){			
			if (GUI.Button (new Rect (x+220, y, 190, 20), "Upgrade Bunker: 650 RP")) {//Display bunker Button
				bunkerLevel+=3;
				//currentRP -= 650;
				storeControllerScript.subtractCurrentRP(650);
				storeControllerScript.updateRPPref();
				bunkerLevelCounter = PlayerPrefs.GetInt("PurchasedbunkerLevels");
				bunkerLevelCounter += 2;
				PlayerPrefs.SetInt("PurchasedbunkerLevels", bunkerLevelCounter);
				soundControllerScript.playSFX("buy");
			}
		}else {
			GUI.enabled = false;
			if (GUI.Button (new Rect (x+220, y, 190, 20), "Not Enough RP (650)")){}//Display bunker Button}
		}
		
		maxSurvivors = bunkerLevel * 2;
		
		GUI.enabled = false;
		
		if (GUI.Button (new Rect (x+35, y+25, 170, 20), foundSurvivors + "/" + maxSurvivors + " survivors housed")){}//Display Current Survivors / Max
		if (GUI.Button (new Rect (x+215, y+25, 170, 20), unallocatedSurvivors + "/" + foundSurvivors + " survivors un-allocated")){}//Display Current Survivors / Max
		
		GUI.enabled =  false;
		if (GUI.Button (new Rect (x-5, y+50, 140, 20), new GUIContent(engineerSurvivors + " Engineers","Engineers unlock equipment"))){}
		if (GUI.Button (new Rect (x+140, y+50, 140, 20), new GUIContent(scrapSurvivors + " Scrappers","Scrappers give scrap after every round"))){}
		if (GUI.Button (new Rect (x+285, y+50, 140, 20), new GUIContent(ammoSurvivors + " Hoarders","Hoarders give ammo after every round"))){}
		
		GUI.enabled =  true;
		
		if (unallocatedSurvivors > 0){
			if (GUI.Button (new Rect (x-5, y+75, 140, 20), "Allocate Engineers")){
				engineerSurvivors++;
				unallocatedSurvivors--;	
				soundControllerScript.playSFX("buy");			
			}
			if (GUI.Button (new Rect (x+140, y+75, 140, 20), "Allocate Scrapper")){
				scrapSurvivors++;
				unallocatedSurvivors--;	
				soundControllerScript.playSFX("buy");			
			}
			if (GUI.Button (new Rect (x+285, y+75, 140, 20), "Allocate Hoarders")){
				ammoSurvivors++;
				unallocatedSurvivors--;	
				soundControllerScript.playSFX("buy");			
			}
			GUI.enabled = true;
		}else {
			GUI.enabled = false;
			if (GUI.Button (new Rect (x-5, y+75, 140, 20), "No Survivors")){}
			if (GUI.Button (new Rect (x+140, y+75, 140, 20), "No Survivors")){}
			if (GUI.Button (new Rect (x+285, y+75, 140, 20), "No Survivors")){}
		}
		
		
		GUI.enabled = true;
		
		if(engineerSurvivors > 0){
			if (GUI.Button (new Rect (x-5, y+100, 140, 20), "Un-Allocate Engineers")){
				engineerSurvivors--;
				unallocatedSurvivors++;
				soundControllerScript.playSFX("buy");
			}
		}else {
			GUI.enabled = false;
			if (GUI.Button (new Rect (x-5, y+100, 140, 20), "No Engineers")){}
		}
		
		GUI.enabled = true;
		
		if(scrapSurvivors > 0){
			if (GUI.Button (new Rect (x+140, y+100, 140, 20), "Un-Allocate Scrappers")){
				scrapSurvivors--;
				unallocatedSurvivors++;
				soundControllerScript.playSFX("buy");
			}
		}else {
			GUI.enabled = false;
			if (GUI.Button (new Rect (x+140, y+100, 140, 20), "No Scrappers")){}
		}
		
		GUI.enabled = true;
		
		if(ammoSurvivors > 0){
			if (GUI.Button (new Rect (x+285, y+100, 140, 20), "Un-Allocate Hoarders")){
				ammoSurvivors--;
				unallocatedSurvivors++;
				soundControllerScript.playSFX("buy");
			}
		}else {
			GUI.enabled = false;
			if (GUI.Button (new Rect (x+285, y+100, 140, 20), "No Hoarders")){}
		}
		GUI.enabled = true;
		//Survivors Section End

	}

	public void survivorBonus(int roundEnded){
		if(engineerSurvivors > 0){
			for(int q=0; q<= engineerSurvivors; q++){//Test Death for unAllocatedSurvivors
				int randomEngineerDeath = (int)Random.Range(0f,10f);
				if(randomEngineerDeath > 0 && randomEngineerDeath < 2){ // If 1
					totalSurvivorDeathCounter++;
					engineerSurvivors--;
				}
			}
		}
		if (scrapSurvivors > 0){//Add Scraps End-of-Round Bonuses
			int scrapGenerated = 0;
			int randomScrap = (int)Random.Range(0f,10f);
			scrapGenerated = (scrapSurvivors + roundEnded + randomScrap) * 10;
			playerScript.addScraps(scrapGenerated);
			
			for(int q=0; q<= scrapSurvivors; q++){//Test Death for unAllocatedSurvivors
				int randomScrapDeath = (int)Random.Range(0f,10f);			
				if(randomScrapDeath > 0 && randomScrapDeath < 2){ // If 1
					totalSurvivorDeathCounter++;
					scrapSurvivors--;
				}
			}
		}
		if(ammoSurvivors > 0){//Add Ammo End-of-Round Bonuses
			int randomAmmo = (int)Random.Range((0f+ammoSurvivors+roundEnded)*2,25f);
			primaryController.addRandomAmmo(randomAmmo);
			
			for(int q=0; q<= ammoSurvivors; q++){//Test Death for unAllocatedSurvivors
				int randomAmmoDeath = (int)Random.Range(0f,10f);
				if(randomAmmoDeath > 0 && randomAmmoDeath < 2){ // If 1
					totalSurvivorDeathCounter++;
					ammoSurvivors--;
				}
			}
		}
		if(totalSurvivorDeathCounter > 0){ // If there are dead survivors
			foundSurvivors -= totalSurvivorDeathCounter;//Reduces foundSurvivors by total lost
			totalSurvivorDeathCounter = 0;
		}
	}
	public void addUnallocatedSurvivors(){
		unallocatedSurvivors++;
	}
	public void addUnallocatedSurvivors(int value){
		unallocatedSurvivors += value;
	}
	public void addFoundSurvivors(){
		foundSurvivors++;
	}
	public void addFoundSurvivors(int value){
		foundSurvivors += value;
	}
	public int getFoundSurvivors(){
		return foundSurvivors;
	}
	public int getMaxSurvivors(){
		return maxSurvivors;
	}

	public int getEngineerSurvivors(){
		return engineerSurvivors;
	}

	public void reset(){
		 unallocatedSurvivors = 0;
		 foundSurvivors = 0;
		//int allocatedSurvivors = 0;
		 engineerSurvivors = 0;
		 scrapSurvivors = 0;
		 ammoSurvivors = 0;
		 bunkerLevel = 1;
		 bunkerLevelCounter = 0;
		 maxSurvivors = 0;
		
		 totalSurvivorDeathCounter = 0;

		
		 x = 335;
		 y = 460;

		if (PlayerPrefs.HasKey ("PurchasedbunkerLevels")) {
			bunkerLevelCounter = PlayerPrefs.GetInt ("PurchasedbunkerLevels");
		}
		
		bunkerLevel += bunkerLevelCounter;		
		maxSurvivors = bunkerLevel * 2;
	}

}
