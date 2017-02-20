using UnityEngine;
using System.Collections;

public class SecondaryButtonsController : MonoBehaviour {
	//int barricadePurchased = 0;
	int landMinePurchased = 0;
	int bioBombPurchased = 0;
	int sentryTurretPurchased = 0;
	int rotatingLaserBeamPurchased = 0;
	int laserLinePurchased = 0;

	public GameObject player;
	public GameObject storeObject;
	public GameObject ZombieSpawner;
	PlayerController playerScript;
	StoreController storeControllerScript;
	//Spawner spawnerScript;
	ZombieSpawner zombieSpawnerScript;
	SurvivorButtonsController survivorButtonsController;
	SecondaryController secondaryController;
	
	GameObject soundController;
	SoundController soundControllerScript;

	void Start () {
		player = GameObject.Find("Player");
		ZombieSpawner = GameObject.Find("ZombieSpawner");
		soundController = GameObject.Find ("SoundController");
		playerScript = player.GetComponent<PlayerController> ();
		secondaryController = player.GetComponent<SecondaryController> ();
		
		storeControllerScript = storeObject.GetComponent<StoreController>();
		survivorButtonsController = storeObject.GetComponent<SurvivorButtonsController>();
		//spawnerScript = ZombieSpawner.GetComponent<Spawner> ();		
		zombieSpawnerScript = ZombieSpawner.GetComponent<ZombieSpawner> ();
		soundControllerScript = soundController.GetComponent<SoundController>();
	}

	void Update(){}

	//int yOne = 200;//Was 265
	int yTwo = 265;//190+65=
	int yThree = 330;
	int yFour = 398;
	int yFive = 460;
	int ySix = 525;

	public void displaySecondaryButtons(){
		drawEquipedSquare();
		//---------------------------------------Secondaries Start
		//if(secondariesPage == 1){
			/// Barricade Section Start		
			/*if(survivorButtonsController.getEngineerSurvivors() > 0){
				if(spawnerScript.currentRound >= 2){
					if(barricadePurchased == 1){
						if (GUI.Button (new Rect (65, yOne, 120, 30), "Equip Barricade")){
							secondaryController.equipSecondary(0);
						}
						if(secondaryController.getStock(0) == 0){
							if (playerScript.getCurrentScraps() < 100) {
								GUI.enabled = false;//Grey out GUI
								if (GUI.Button (new Rect (55, yOne+35, 135, 20), "Not Enough Scrap")) {}
							} else if (GUI.Button (new Rect (55, yOne+35, 135, 20), "Buy More: 100 scrap")) {	
								if (playerScript.getCurrentScraps() >= 100) {
									playerScript.subtractScraps(100);
									secondaryController.addStock(0,1);
								}
							}	
						}else {
							GUI.enabled = false;//Grey out GUI
							if (GUI.Button (new Rect (55, yOne+35, 135, 20), "Max Carrying")) {}
						}
						GUI.enabled = true;
					}else {						
						if (playerScript.getCurrentScraps() < 500) {
							GUI.enabled = false;
							if (GUI.Button (new Rect (65, yOne, 120, 30), "Not Enough Scrap\n500 Scrap")) {}
						} else {					
							if (GUI.Button (new Rect (65, yOne, 120, 30), "Unlock Barricade\n500 Scrap")) {	
								playerScript.subtractScraps(500);
								barricadePurchased = 1;								
							}						
						}
					}
				}else {
					GUI.enabled = false;
					if (GUI.Button (new Rect (65, yOne, 120, 20), "Not Round 2")){}//Button for Round
				}
			}else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (65, yOne, 120, 20), survivorButtonsController.getEngineerSurvivors() + "/1 Engineers")){}//Button for engineer surivors
			}
			GUI.enabled = true;
			/// Barricade Section End	
		*/
			
			/// Land Mine Section Start
			if(survivorButtonsController.getEngineerSurvivors() > 1){
				if(zombieSpawnerScript.getCurrentRound() >= 2){
					if(landMinePurchased == 1){
						if(secondaryController.getCurrentlyEquipedSecondary() == 1){
							GUI.enabled = false;
						}
						if (GUI.Button (new Rect (65, yTwo, 120, 30), "Equip LandMine")){
							secondaryController.equipSecondary(1);
							soundControllerScript.playSFX("gunCock");
						}	
						GUI.enabled = true;
							if (playerScript.getCurrentScraps() < 249) {
								GUI.enabled = false;//Grey out GUI
								if (GUI.Button (new Rect (55, yTwo+35, 135, 20), "Not Enough Scrap")) {}
							} else if (GUI.Button (new Rect (55, yTwo+35, 135, 20), "Buy More: 250 scrap")) {
									playerScript.subtractScraps(250);
									secondaryController.addStock(1,1);
									soundControllerScript.playSFX("buy");
							}
						GUI.enabled = true;
					}else {						
						if (playerScript.getCurrentScraps() < 499) {
							GUI.enabled = false;
							if (GUI.Button (new Rect (65, yTwo, 120, 30), "Not Enough Scrap\n500 Scrap")) {}
						} else {					
							if (GUI.Button (new Rect (65, yTwo, 120, 30), "Unlock LandMine\n500 Scrap")){													
								playerScript.subtractScraps(500);
								landMinePurchased = 1;
								soundControllerScript.playSFX("buy");

								secondaryController.equipSecondary(1);
								soundControllerScript.playSFX("gunCock");
							}						
						}
					}
				}else {
					GUI.enabled = false;
					if (GUI.Button (new Rect (65, yTwo, 120, 20), "Not Round 2")){}//Button for Round
				}					
			}else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (65, yTwo, 120, 20), survivorButtonsController.getEngineerSurvivors() + "/2 Engineers")){}//Button for engineer surivors
			}
			GUI.enabled = true;
			/// Land Mine Section End
			
			/// Bio-Bomb
			if(survivorButtonsController.getEngineerSurvivors() > 3){
				if(zombieSpawnerScript.getCurrentRound() >= 2){
					if(bioBombPurchased == 1){
						if(secondaryController.getCurrentlyEquipedSecondary() == 2){
							GUI.enabled = false;
						}
						if (GUI.Button (new Rect (65, yThree, 120, 30), "Equip Bomb")){
							secondaryController.equipSecondary(2);
							soundControllerScript.playSFX("gunCock");
						}	
						GUI.enabled = true;
						//if(secondaryController.getStock(2) == 0){
							if (playerScript.getCurrentScraps() < 549) {
								GUI.enabled = false;//Grey out GUI
								if (GUI.Button (new Rect (55, yThree+32, 135, 20), "Not Enough Scrap")) {}
							} else if (GUI.Button (new Rect (55, yThree+35, 135, 20), "Buy More: 550 scrap")) {	
								//if (playerScript.getCurrentScraps() >= 549){
									playerScript.subtractScraps(550);
									secondaryController.addStock(2,1);	
									soundControllerScript.playSFX("buy");
								//}
							}	
						//}else {
						//	GUI.enabled = false;//Grey out GUI
						//	if (GUI.Button (new Rect (55, yThree+35, 135, 20), "Max Carrying")) {}
						//}
						GUI.enabled = true;
					}else {						
						if (playerScript.getCurrentScraps() < 599) {
							GUI.enabled = false;
							if (GUI.Button (new Rect (65, yThree, 120, 30), "Not Enough Scrap\n600 Scrap")) {}
						} else {					
							if (GUI.Button (new Rect (65, yThree, 120, 30), "Unlock Bomb\n600 Scrap")) {	
								playerScript.subtractScraps(600);
								bioBombPurchased = 1;		
								soundControllerScript.playSFX("buy");	

								secondaryController.equipSecondary(2);
								soundControllerScript.playSFX("gunCock");
							}						
						}
					}
				}else {
					GUI.enabled = false;
					if (GUI.Button (new Rect (65, yThree, 120, 20), "Not Round 2")){}//Button for Round
				}					
			}else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (65, yThree, 120, 20), survivorButtonsController.getEngineerSurvivors() + "/4 Engineers")){}//Button for engineer surivors
			}
			GUI.enabled = true;
			/// Bio-Bomb
		//}
		
		//if(secondariesPage == 2){	
			/// Sentry Turret Section Start
			if(survivorButtonsController.getEngineerSurvivors() > 3){
				if(zombieSpawnerScript.getCurrentRound() >= 3){
					if(sentryTurretPurchased == 1){
						if(secondaryController.getCurrentlyEquipedSecondary() == 3){
							GUI.enabled = false;
						}
						if (GUI.Button (new Rect (65, yFour, 120, 30), "Equip Turret")){
							secondaryController.equipSecondary(3);
							soundControllerScript.playSFX("gunCock");
						}	
						GUI.enabled = true;
						//if(secondaryController.getStock(3) == 0){
							if (playerScript.getCurrentScraps() < 399) {
								GUI.enabled = false;//Grey out GUI
								if (GUI.Button (new Rect (55, yFour+35, 135, 20), "Not Enough Scrap")) {}
							} else if (GUI.Button (new Rect (55, yFour+35, 135, 20), "Buy More: 400 scrap")) {	
								//if (playerScript.getCurrentScraps() >= 500) {
									playerScript.subtractScraps(400);
									secondaryController.addStock(3,1);
									soundControllerScript.playSFX("buy");
								//}
							}		
						//}else{
						//	GUI.enabled = false;//Grey out GUI
						//	if (GUI.Button (new Rect (55, yFour+35, 135, 20), "Max Carrying")) {}
						//}
						GUI.enabled = true;
					}else {						
						if (playerScript.getCurrentScraps() < 499) {
							GUI.enabled = false;
							if (GUI.Button (new Rect (65, yFour, 120, 30), "Not Enough Scrap\n500 Scrap")) {}
						} else {					
							if (GUI.Button (new Rect (65, yFour, 120, 30), "Unlock Turret\n500 Scrap")) {	
								playerScript.subtractScraps(500);
								sentryTurretPurchased = 1;	
								soundControllerScript.playSFX("buy");	

								secondaryController.equipSecondary(3);
								soundControllerScript.playSFX("gunCock");
							}						
						}
					}
				}else {
					GUI.enabled = false;
					if (GUI.Button (new Rect (65, yFour, 120, 20), "Not Round 3")){}//Button for Round
				}					
			}else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (65, yFour, 120, 20), survivorButtonsController.getEngineerSurvivors() + "/4 Engineers")){}//Button for engineer surivors
			}
			GUI.enabled = true;
			/// Sentry Turret Section End
			
			/// RotatingLaserBeam
			if(survivorButtonsController.getEngineerSurvivors() > 6){
				if(zombieSpawnerScript.getCurrentRound() >= 4){
					if(rotatingLaserBeamPurchased == 1){
						if(secondaryController.getCurrentlyEquipedSecondary() == 4){
							GUI.enabled = false;
						}
						if (GUI.Button (new Rect (65, yFive, 120, 30), "Equip R-Beam")){
							secondaryController.equipSecondary(4);
							soundControllerScript.playSFX("gunCock");
						}	
						GUI.enabled = true;
						if(secondaryController.getStock(4) == 0){
							if (playerScript.getCurrentScraps() < 449) {
								GUI.enabled = false;//Grey out GUI
							if (GUI.Button (new Rect (55, yFive+35, 135, 20), "Not Enough Scrap")) {}
						} else if (GUI.Button (new Rect (55, yFive+35, 135, 20), "Buy More: 450 scrap")) {	
								//if (playerScript.getCurrentScraps() >= 450) {
									playerScript.subtractScraps(450);
									secondaryController.addStock(4,1);
									soundControllerScript.playSFX("buy");
								//}
							}	
						}else {
							GUI.enabled = false;//Grey out GUI
							if (GUI.Button (new Rect (55, yFive+35, 135, 20), "Max Carrying")) {}
						}
						GUI.enabled = true;
					}else {						
						if (playerScript.getCurrentScraps() < 549) {
							GUI.enabled = false;
						if (GUI.Button (new Rect (65, yFive, 120, 30), "Not Enough Scrap\n550 Scrap")) {}
						} else {					
						if (GUI.Button (new Rect (65, yFive, 120, 30), "Unlock R-Beam\n550 Scrap")) {	
								playerScript.subtractScraps(550);
								rotatingLaserBeamPurchased = 1;	
								soundControllerScript.playSFX("buy");	

								secondaryController.equipSecondary(4);
								soundControllerScript.playSFX("gunCock");
							}						
						}
					}
				}else {
					GUI.enabled = false;
				if (GUI.Button (new Rect (65, yFive, 120, 20), "Not Round 4")){}//Button for Round
				}					
			}else {
				GUI.enabled = false;
			if (GUI.Button (new Rect (65, yFive, 120, 20), survivorButtonsController.getEngineerSurvivors() + "/7 Engineers")){}//Button for engineer surivors
			}
			GUI.enabled = true;
			/// RotatingLaserBeam
			
			/// LaserLine
			if(survivorButtonsController.getEngineerSurvivors() > 6){
				if(zombieSpawnerScript.getCurrentRound() >= 5){
					if(laserLinePurchased == 1){
						if(secondaryController.getCurrentlyEquipedSecondary() == 5){
							GUI.enabled = false;
						}
						if (GUI.Button (new Rect (65, ySix, 120, 30), "Equip L-Laser")){
							secondaryController.equipSecondary(5);
							soundControllerScript.playSFX("gunCock");
						}
						GUI.enabled = true;
						if(secondaryController.getStock(5) == 0){
							if (playerScript.getCurrentScraps() < 549) {
								GUI.enabled = false;//Grey out GUI
							if (GUI.Button (new Rect (55, ySix+35, 135, 20), "Not Enough Scrap")) {}
						} else if (GUI.Button (new Rect (55, ySix+35, 135, 20), "Buy More: 550 scrap")) {	
								//if (playerScript.getCurrentScraps() >= 800) {
									playerScript.subtractScraps(550);
									secondaryController.addStock(5,1);
									soundControllerScript.playSFX("buy");
								//}
							}		
						}else{
							GUI.enabled = false;//Grey out GUI
						if (GUI.Button (new Rect (55, ySix+35, 135, 20), "Max Carrying")) {}
						}
						GUI.enabled = true;
					}else {						
						if (playerScript.getCurrentScraps() < 599) {
							GUI.enabled = false;
						if (GUI.Button (new Rect (65, ySix, 120, 30), "Not Enough Scrap\n600 Scrap")) {}
						} else {					
						if (GUI.Button (new Rect (65, ySix, 120, 30), "Unlock L-Line\n600 Scrap")) {	
								playerScript.subtractScraps(600);
								laserLinePurchased = 1;			
								soundControllerScript.playSFX("buy");

								secondaryController.equipSecondary(5);
								soundControllerScript.playSFX("gunCock");
							}						
						}
					}
				}else {
					GUI.enabled = false;
				if (GUI.Button (new Rect (65, ySix, 120, 20), "Not Round 5")){}//Button for Round
				}					
			}else {
				GUI.enabled = false;
			if (GUI.Button (new Rect (65, ySix, 120, 20), survivorButtonsController.getEngineerSurvivors() + "/7 Engineers")){}//Button for engineer surivors
			}
			GUI.enabled = true;
			/// LaserLine
		//}
		//-------------------------------------------------------Secondaries End


		/*if (secondariesPage == 1) {//Page Controller at Bottom
			if (GUI.Button (new Rect (110, 490, 25, 20), "2")) {
				secondariesPage = 2;
			}
		} else {
			if (GUI.Button (new Rect (110, 490, 25, 20), "1")) {
				secondariesPage = 1;
			}
		}*/

	}

	void drawEquipedSquare(){// Method to Draw Square behind currently equiped weapon & Secondary
		if(secondaryController.getCurrentlyEquipedSecondary() == 0){
			//storeControllerScript.DrawQuad (new Rect (50, 260, 145, 61), Color.cyan);//Rectangle over Barricade
			storeControllerScript.DrawEquippedSquareSecondaries(190,205);//Draw Fancy Texture Code Here
		}else if(secondaryController.getCurrentlyEquipedSecondary() == 1){
			//storeControllerScript.DrawQuad (new Rect (50, 328, 145, 61), Color.cyan);//Rectangle over Land Mine
			storeControllerScript.DrawEquippedSquareSecondaries(190,270);//Draw Fancy Texture Code Here
		}else if(secondaryController.getCurrentlyEquipedSecondary() == 2){
			//storeControllerScript.DrawQuad (new Rect (50, 392, 145, 61), Color.cyan);//Rectangle over Bio-Bomb
			storeControllerScript.DrawEquippedSquareSecondaries(190,335);//Draw Fancy Texture Code Here
		}else if(secondaryController.getCurrentlyEquipedSecondary() == 3){
			//storeControllerScript.DrawQuad (new Rect (50, 260, 145, 61), Color.cyan);//Rectangle over Sentry Turret
			storeControllerScript.DrawEquippedSquareSecondaries(190,400);//Draw Fancy Texture Code Here
		}else if(secondaryController.getCurrentlyEquipedSecondary() == 4){
			//storeControllerScript.DrawQuad (new Rect (50, 328, 145, 61), Color.cyan);//Rectangle over Rotating Laser Beam
			storeControllerScript.DrawEquippedSquareSecondaries(190,465);//Draw Fancy Texture Code Here
		}else if(secondaryController.getCurrentlyEquipedSecondary() == 5){
			//storeControllerScript.DrawQuad (new Rect (50, 392, 145, 61), Color.cyan);//Rectangle over Laser Line
			storeControllerScript.DrawEquippedSquareSecondaries(190,530);//Draw Fancy Texture Code Here
		}		
	}
	public void reset(){
		 landMinePurchased = 0;
		 bioBombPurchased = 0;
		 sentryTurretPurchased = 0;
		 rotatingLaserBeamPurchased = 0;
		 laserLinePurchased = 0;

		 yTwo = 265;//190+65=
		 yThree = 330;
		 yFour = 398;
		 yFive = 460;
		 ySix = 525;
	}
}
