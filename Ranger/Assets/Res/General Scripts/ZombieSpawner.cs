using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {
	int currentRound = 1;
	int counter = 0;

	int zombiesSpawned = 1;
	int maxZombiesThisRound = 0;
	int zombiesKilled = 0;

	bool runOncePerRound = true;

	float spawnTimer = 0f;

	public GameObject player;
	public GameObject storeObject;
	StoreController storeControllerScript;
	SurvivorButtonsController survivorButtonsController;
	PlayerController playerScript;
	MilestoneController milestoneControllerScript;
	PoolingSystem pS;

	void Start () {
		pS = PoolingSystem.Instance;
		playerScript = player.GetComponent<PlayerController> ();
		milestoneControllerScript = player.GetComponent<MilestoneController>();
		storeControllerScript = storeObject.GetComponent<StoreController>();		
		survivorButtonsController = storeObject.GetComponent<SurvivorButtonsController>();
	}

	void Update () {
		if(!playerScript.getRoundEnded()){ // If Round Started

			if(runOncePerRound){ // Run Once Each round to reset max Zombies per round
				zombiesSpawned = 1; // Reset Zombies to Spawn
				zombiesKilled = 0; // Reset the Number of Zombies Killed this Round
				maxZombiesThisRound = 10 + (currentRound * 2);
				spawnTimer = 0f;
				storeControllerScript.roundStarted();
				runOncePerRound = false;
			}

			if(counter == 1){//Each Radiation Pool has a number. This sections moves from pool to pool to spawn Zombies
				this.transform.position = new Vector3(-17.88f,7.5f,0f);
			}else if (counter == 2){//If 2, Move to Radiation Pool 2
				this.transform.position = new Vector3(5.71f,23.11f,0f);
			}else if(counter == 3){//If 3, Move to Radiation Pool 3
				this.transform.position = new Vector3(18.23f,-1.69f,0f);
			}else if(counter == 4){//If 4, Move to Radiation Pool 4
				this.transform.position = new Vector3(-1.89f,-19.77f,0f);
			}else {//If 0, Move to Radiation Pool 0
				this.transform.position = new Vector3(0f,0f,0f);
			}

			if(zombiesSpawned <= maxZombiesThisRound){
				if(spawnTimer < .01f){ // .5 Delay Timer between Zombies
					pS.InstantiateAPS("Zombie", this.transform.position, this.transform.rotation);//Spawn Zombie				
					zombiesSpawned++;
					spawnTimer = .5f;
				}else {
					spawnTimer -= Time.deltaTime;
				}
			}

			if(zombiesKilled == maxZombiesThisRound){ // All Zombies Killed
				//playerScript.roundEnded(); - Redundant because StoreController.roundEnded() sets playerScript.roundEnded.
				storeControllerScript.roundEnded();
				milestoneControllerScript.milestoneCheck(currentRound);
				survivorButtonsController.survivorBonus(currentRound);//Survivor Bonus
				currentRound++;//Round Over
			}

			if(counter != 4){
				counter++;
			}else {
				counter = 0;
			}

		}else { // Round Over
			runOncePerRound = true;

		}
	}

	public void deadZombie(){
		zombiesKilled++;
	}
	public int getZombiesAlive(){
		return maxZombiesThisRound - zombiesKilled;
	}
	public int getCurrentRound(){
		return currentRound;
	}
	public void nextRound(){
		currentRound++;
	}
	public void previousRound(){
		currentRound--;
	}
	/*public void setRoundTimerToZero(){
		roundTimer = 0;
	}
	public int getNumberOfUnits(){
		return numberOfUnits;
	}*/

	public void reset(){		
		currentRound = 1;
		counter = 0;
		
		zombiesSpawned = 1;
		maxZombiesThisRound = 0;
		zombiesKilled = 0;
		
		runOncePerRound = true;
		
		spawnTimer = 0f;
	}

}
