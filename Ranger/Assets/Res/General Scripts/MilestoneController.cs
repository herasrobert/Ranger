using UnityEngine;
using System.Collections;

public class MilestoneController : MonoBehaviour {
	int numberOfZombiesKilled = 0;

	int mileOne = 0;
	int mileTwo = 0;
	int mileThree = 0;


	bool checkForRound = false;
	bool checkForZombies = false;
	
	public GameObject player;
	public GameObject ZombieSpawner;
	PlayerController playerScript;
	public GameObject storeObject;
	StoreController storeControllerScript;

	void Start () {
		playerScript = player.GetComponent<PlayerController> ();		
		storeControllerScript = storeObject.GetComponent<StoreController>();

		if (PlayerPrefs.HasKey ("mileOne")) {
			mileOne = PlayerPrefs.GetInt("mileOne");
		} else {
			PlayerPrefs.SetInt("mileOne", mileOne);
		}
		if (PlayerPrefs.HasKey ("mileTwo")) {
			mileTwo = PlayerPrefs.GetInt("mileTwo");
		} else {
			PlayerPrefs.SetInt("mileTwo", mileTwo);
		}
		if (PlayerPrefs.HasKey ("mileThree")) {
			mileThree = PlayerPrefs.GetInt("mileThree");
		} else {
			PlayerPrefs.SetInt("mileThree", mileThree);
		}
	}

	//void Update () {
	public void milestoneCheck(int currentRound){
		//currentRound = zombieSpawnerScript.getCurrentRound();
		numberOfZombiesKilled = playerScript.getNumberOfZombiesKilled();

		if (currentRound % 10 == 0) { // If 10,20,30, etc
			if(checkForRound == false){ //Check to run only once per round				
				playerScript.addScraps(currentRound * 20); // Add scrap based on current Round (200,400,600,800,etc)
				checkForRound = true;
			}
		} else {
			checkForRound = false;
		}
		if (currentRound % 7 == 0) { // If 5,10,15,20, etc
			if(checkForZombies == false){ //Check to run only once per round				
				playerScript.addScraps(numberOfZombiesKilled * 10); // Add scrap based on current Round - 20 for every 5 zombies killed
				checkForZombies = true;
			}
		} else {
			checkForZombies = false;
		}

		if(currentRound == 10 && mileOne == 0){
			mileOne = 1;
			PlayerPrefs.SetInt("mileOne", 1);
			storeControllerScript.addCurrentRP(325);
		}
		if(currentRound == 50 && mileTwo == 0){
			mileTwo = 1;
			PlayerPrefs.SetInt("mileTwo", 1);
			storeControllerScript.addCurrentRP(455);
		}
		if(currentRound == 100 && mileThree == 0){
			mileThree = 1;
			PlayerPrefs.SetInt("mileThree", 1);
			storeControllerScript.addCurrentRP(650);
		}
	}
}