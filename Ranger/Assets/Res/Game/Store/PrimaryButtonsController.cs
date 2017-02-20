using UnityEngine;
using System.Collections;

public class PrimaryButtonsController : MonoBehaviour {

	int shotGunPurchased = 0;
	int rocketLauncherPurchased = 0;
	int uziPurchased = 0;
	int grenadePurchased = 0;
	int gatlingGunPurchased = 0;
	int crossBowPurchased = 0;
	int bolterPurchased = 0;
	int dildoCannonPurchased = 0;
	int laserCannonPurchased = 0;

	/// <Upgrades Start>	
	int shotGunUpgraded = 0; // Get from PlayerPref
	int rifleUpgraded = 0;
	int rocketUpgraded = 0;
	int uziUpgraded = 0;
	int grenadeUpgraded = 0;
	int gatlingGunUpgraded = 0;
	int crossBowUpgraded = 0;
	int bolterUpgraded = 0;
	int dildoUpgraded = 0;
	int laserUpgraded = 0;
	/// </Upgrades End>

	int columnOneX = 370;
	int columnTwoX = 595;
	int equipWidth = 115;
	int equipHeight = 20;
	int yOne = 30;
	int yTwo = 110;
	int yThree = 190;
	int yFour = 270;
	int yFive = 350;

	public GameObject player;
	public GameObject storeObject;
	public GameObject ZombieSpawner;
	PrimaryController primaryController;
	PlayerController playerScript;
	StoreController storeControllerScript;
	//Spawner spawnerScript;
	ZombieSpawner zombieSpawnerScript;
	
	GameObject soundController;
	SoundController soundControllerScript;

	GameObject rpController;
	RPController rpControllerScript;

	void Start(){
		player = GameObject.Find("Player");
		ZombieSpawner = GameObject.Find("ZombieSpawner");
		soundController = GameObject.Find ("SoundController");
		playerScript = player.GetComponent<PlayerController> ();
		primaryController = player.GetComponent<PrimaryController> ();
		
		storeControllerScript = storeObject.GetComponent<StoreController>();
		//spawnerScript = ZombieSpawner.GetComponent<Spawner> ();
		zombieSpawnerScript = ZombieSpawner.GetComponent<ZombieSpawner> ();

		soundControllerScript = soundController.GetComponent<SoundController>();

		rpController = GameObject.Find ("RPController");
		rpControllerScript = rpController.GetComponent<RPController>();

		if (PlayerPrefs.HasKey ("ShotGunPurchased")) {
			shotGunPurchased = PlayerPrefs.GetInt ("ShotGunPurchased");
		}		
		if (PlayerPrefs.HasKey ("RocketLauncherPurchased")) {
			rocketLauncherPurchased = PlayerPrefs.GetInt ("RocketLauncherPurchased");
		}
		if (PlayerPrefs.HasKey ("UziPurchased")) {
			uziPurchased = PlayerPrefs.GetInt ("UziPurchased");
		}
		if (PlayerPrefs.HasKey ("GrenadePurchased")) {
			grenadePurchased = PlayerPrefs.GetInt ("GrenadePurchased");
		}
		if (PlayerPrefs.HasKey ("GatlingGunPurchased")) {
			gatlingGunPurchased = PlayerPrefs.GetInt ("GatlingGunPurchased");
		}
		if (PlayerPrefs.HasKey ("CrossBowPurchased")) {
			crossBowPurchased = PlayerPrefs.GetInt ("CrossBowPurchased");
		}
		if (PlayerPrefs.HasKey ("BolterPurchased")) {
			bolterPurchased = PlayerPrefs.GetInt ("BolterPurchased");
		}
		if (PlayerPrefs.HasKey ("DildoCannonPurchased")) {
			dildoCannonPurchased = PlayerPrefs.GetInt ("DildoCannonPurchased");
		}
		if (PlayerPrefs.HasKey ("LaserCannonPurchased")) {
			laserCannonPurchased = PlayerPrefs.GetInt ("LaserCannonPurchased");
		}

		if(PlayerPrefs.HasKey("rifleUpgraded")){			
			rifleUpgraded = PlayerPrefs.GetInt ("rifleUpgraded");
			primaryController.setUpgraded(1);
		}
		if(PlayerPrefs.HasKey("shotGunUpgraded")){			
			shotGunUpgraded = PlayerPrefs.GetInt ("shotGunUpgraded");
			primaryController.setUpgraded(2);
		}
		if(PlayerPrefs.HasKey("rocketUpgraded")){			
			rocketUpgraded = PlayerPrefs.GetInt ("rocketUpgraded");
			primaryController.setUpgraded(3);
		}
		if(PlayerPrefs.HasKey("uziUpgraded")){			
			uziUpgraded = PlayerPrefs.GetInt ("uziUpgraded");
			primaryController.setUpgraded(4);
		}
		if(PlayerPrefs.HasKey("grenadeUpgraded")){			
			grenadeUpgraded = PlayerPrefs.GetInt ("grenadeUpgraded");
			primaryController.setUpgraded(5);
		}
		if(PlayerPrefs.HasKey("gatlingGunUpgraded")){			
			gatlingGunUpgraded = PlayerPrefs.GetInt ("gatlingGunUpgraded");
			primaryController.setUpgraded(6);
		}
		if(PlayerPrefs.HasKey("crossBowUpgraded")){			
			crossBowUpgraded = PlayerPrefs.GetInt ("crossBowUpgraded");
			primaryController.setUpgraded(7);
		}
		if(PlayerPrefs.HasKey("bolterUpgraded")){			
			bolterUpgraded = PlayerPrefs.GetInt ("bolterUpgraded");
			primaryController.setUpgraded(8);
		}
		if(PlayerPrefs.HasKey("dildoUpgraded")){			
			dildoUpgraded = PlayerPrefs.GetInt ("dildoUpgraded");
			primaryController.setUpgraded(9);
		}
		if(PlayerPrefs.HasKey("laserUpgraded")){			
			laserUpgraded = PlayerPrefs.GetInt ("laserUpgraded");
			primaryController.setUpgraded(10);
		}
	}

	public void displayPrimaryButtons(){
		drawEquipedSquare ();
	
		//-------------------------------Weapons Start
			if(primaryController.getCurrentlyEquipedWeapon() == 1){
				GUI.enabled = false;
			}
			if (GUI.Button (new Rect (columnOneX, yOne, equipWidth, equipHeight), "Equip: Rifle")) {	
				primaryController.setInventorySlotOne(1);
				primaryController.equipWeapon (1);
				soundControllerScript.playSFX("gunCock");
			}
			GUI.enabled = true;
			if (playerScript.getCurrentScraps() < 9) {
				GUI.enabled = false;//Grey out GUI
				if (GUI.Button (new Rect (columnOneX-30, yOne+25, equipWidth+65, equipHeight), "Not Enough Scrap")) {
				}
			} else if (GUI.Button (new Rect (columnOneX-30, yOne+25, equipWidth+65, equipHeight), "Buy Ammo: 10 scrap")) {
					playerScript.subtractScraps(10);
					primaryController.addAmmo(1,20);
					soundControllerScript.playSFX("buy");
			}
			GUI.enabled = true;
			if(rifleUpgraded == 0){				
				if(rpControllerScript.getCurrentRP() > 259){
					if (GUI.Button (new Rect (columnOneX-30, yOne+50, equipWidth+65, equipHeight), "Upgrade Rifle: 260 RP")) {
						storeControllerScript.subtractCurrentRP(260);
						storeControllerScript.updateRPPref();
						rifleUpgraded = 1;
						PlayerPrefs.SetInt("rifleUpgraded",1);
						primaryController.setUpgraded(1);
						soundControllerScript.playSFX("buy");
					}
				}else{
					GUI.enabled = false;
					if (GUI.Button (new Rect (columnOneX-30, yOne+50, equipWidth+65, equipHeight), "Not Enough RP: 260 RP")){}//Button - not enough rp
				}
			}else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (columnOneX-30, yOne+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
			}
			GUI.enabled = true;
			///Rifle Section End
			
			///ShotGun Section Start
			if (shotGunPurchased == 1) {//If Shotgun Purchased
				if(primaryController.getCurrentlyEquipedWeapon() == 2){
					GUI.enabled = false;
				}
				if (GUI.Button (new Rect (columnTwoX, yOne, equipWidth, equipHeight), "Equip ShotGun")) {	
					primaryController.setInventorySlotOne(2);
					primaryController.equipWeapon (2);
					soundControllerScript.playSFX("gunCock");
				}
				GUI.enabled = true;
				if (playerScript.getCurrentScraps() < 14) {
					GUI.enabled = false;//Grey out GUI
					if (GUI.Button (new Rect (columnTwoX-30, yOne+25, equipWidth+65, equipHeight), "Not Enough Scrap")){}
				} else if (GUI.Button (new Rect (columnTwoX-30, yOne+25, equipWidth+65, equipHeight), "Buy Ammo: 15 scrap")) {
						playerScript.subtractScraps(15);
						primaryController.addAmmo(2,10);
						soundControllerScript.playSFX("buy");
				}
				GUI.enabled = true;
				if(shotGunUpgraded == 0){				
					if(rpControllerScript.getCurrentRP() > 519){
						if (GUI.Button (new Rect (columnTwoX-30, yOne+50, equipWidth+65, equipHeight), "Upgrade ShotGun: 520 RP")){	
							storeControllerScript.subtractCurrentRP(520);
							storeControllerScript.updateRPPref();
							shotGunUpgraded = 1;
							PlayerPrefs.SetInt("shotGunUpgraded",1);
							primaryController.setUpgraded(2);
							soundControllerScript.playSFX("buy");
						}
					}else{
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-30, yOne+50, equipWidth+65, equipHeight), "Not Enough RP: 520 RP")){}//Button - not enough rp
					}
				}else {
					GUI.enabled = false;
					if (GUI.Button (new Rect (columnTwoX-30, yOne+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
				}
				GUI.enabled = true;
			} else {//Purchase Shotgun Options
				if (playerScript.getCurrentScraps() < 699) {
					GUI.enabled = false;
					if (GUI.Button (new Rect (columnTwoX-15, yOne-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n700 Scrap")) {
					}
				} else {					
					if (GUI.Button (new Rect (columnTwoX-15, yOne-5, equipWidth+35, equipHeight+15), "Purchase Shotgun\n700 Scrap")) {	
						playerScript.subtractScraps(700);
						shotGunPurchased = 1;
						PlayerPrefs.SetInt ("ShotGunPurchased", 1);
						soundControllerScript.playSFX("buy");

						primaryController.setInventorySlotOne(2);
						primaryController.equipWeapon (2);
						soundControllerScript.playSFX("gunCock");
					}
				}
				GUI.enabled = true;
				
				if (rpControllerScript.getCurrentRP() < 520) {
					GUI.enabled = false;
					if (GUI.Button (new Rect (columnTwoX-15, yOne+35, equipWidth+35, equipHeight+15), "Not Enough RP\n520 RP")) {
					}
				} else {					
					if (GUI.Button (new Rect (columnTwoX-15, yOne+35, equipWidth+35, equipHeight+15), "Purchase Shotgun\n520 RP")) {
						storeControllerScript.subtractCurrentRP(520);
						storeControllerScript.updateRPPref();
						shotGunPurchased = 1;
						PlayerPrefs.SetInt ("ShotGunPurchased", 1);
						soundControllerScript.playSFX("buy");

						primaryController.setInventorySlotOne(2);
						primaryController.equipWeapon (2);
						soundControllerScript.playSFX("gunCock");
					}
				}
				GUI.enabled = true;
			}
			///ShotGun Section End
			
			///Rocket Launcher Section Start
			if (zombieSpawnerScript.getCurrentRound() >= 3) {				
				if (rocketLauncherPurchased == 1) {//If Launcher Purchased	
					if(primaryController.getCurrentlyEquipedWeapon() == 3){
						GUI.enabled = false;
					}
					if (GUI.Button (new Rect (columnOneX, yTwo, equipWidth, equipHeight), "Equip Launcher")) {
						primaryController.setInventorySlotOne(3);
						primaryController.equipWeapon (3);
						soundControllerScript.playSFX("gunCock");
					}
					GUI.enabled = true;
					if (playerScript.getCurrentScraps() < 34) {
						GUI.enabled = false;//Grey out GUI
						if (GUI.Button (new Rect (columnOneX-30, yTwo+25, equipWidth+65, equipHeight), "Not Enough Scrap")){}
					} else if (GUI.Button (new Rect (columnOneX-30, yTwo+25, equipWidth+65, equipHeight), "Buy Ammo: 35 scrap")) {
							playerScript.subtractScraps(35);
							primaryController.addAmmo(3,5);
							soundControllerScript.playSFX("buy");
					}
					GUI.enabled = true;
					if(rocketUpgraded == 0){				
						if(rpControllerScript.getCurrentRP() > 519){
							if (GUI.Button (new Rect (columnOneX-30, yTwo+50, equipWidth+65, equipHeight), "Upgrade Launcher: 520 RP")) {	
								storeControllerScript.subtractCurrentRP(520);
								storeControllerScript.updateRPPref();
								rocketUpgraded = 1;
								PlayerPrefs.SetInt("rocketUpgraded",1);
								primaryController.setUpgraded(3);
								soundControllerScript.playSFX("buy");
							}
						}else{
							GUI.enabled = false;
							if (GUI.Button (new Rect (columnOneX-30, yTwo+50, equipWidth+65, equipHeight), "Not Enough RP: 520 RP")){}//Button - not enough rp
						}
					}else {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-30, yTwo+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
					}
					GUI.enabled = true;
				} else {//Purchase Rocket Options
					if (playerScript.getCurrentScraps() < 1499) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-15, yTwo-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n1500 Scrap")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnOneX-15, yTwo-5, equipWidth+35, equipHeight+15), "Purchase Launcher\n1500 Scrap")) {
							playerScript.subtractScraps(1500);
							rocketLauncherPurchased = 1;
							PlayerPrefs.SetInt ("RocketLauncherPurchased", 1);
							soundControllerScript.playSFX("buy");

							primaryController.setInventorySlotOne(3);
							primaryController.equipWeapon (3);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
					if (rpControllerScript.getCurrentRP() < 1170) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-15, yTwo+35, equipWidth+35, equipHeight+15), "Not Enough RP\n1170 RP")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnOneX-15, yTwo+35, equipWidth+35, equipHeight+15), "Purchase Launcher\n1170 RP")) {
							storeControllerScript.subtractCurrentRP(1170);
							storeControllerScript.updateRPPref();
							rocketLauncherPurchased = 1;
							PlayerPrefs.SetInt ("RocketLauncherPurchased", 1);
							soundControllerScript.playSFX("buy");
							
							primaryController.setInventorySlotOne(3);
							primaryController.equipWeapon (3);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
				}
			} else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (columnOneX, yTwo, equipWidth, equipHeight), "Not Round 3")) {
				}
			}
			/// Rocket Launcher Section End
			
			/// Uzi Section Start
			if (zombieSpawnerScript.getCurrentRound() >= 3) {
				if (uziPurchased == 1) {//If Launcher Purchased
					if(primaryController.getCurrentlyEquipedWeapon() == 4){
						GUI.enabled = false;
					}
					if (GUI.Button (new Rect (columnTwoX, yTwo, equipWidth, equipHeight), "Equip Uzi")) {
						primaryController.setInventorySlotOne(4);
						primaryController.equipWeapon (4);
						soundControllerScript.playSFX("gunCock");
					}
					GUI.enabled = true;
					if (playerScript.getCurrentScraps() < 19) {
						GUI.enabled = false;//Grey out GUI
						if (GUI.Button (new Rect (columnTwoX-30, yTwo+25, equipWidth+65, equipHeight), "Not Enough Scrap")){}
					} else if (GUI.Button (new Rect (columnTwoX-30, yTwo+25, equipWidth+65, equipHeight), "Buy Ammo: 20 scrap")) {
							playerScript.subtractScraps(20);
							primaryController.addAmmo(4,20);
							soundControllerScript.playSFX("buy");
					}
					GUI.enabled = true;
					if(uziUpgraded == 0){				
						if(rpControllerScript.getCurrentRP() > 649){
							if (GUI.Button (new Rect (columnTwoX-30, yTwo+50, equipWidth+65, equipHeight), "Upgrade Uzi: 650 RP")) {	
								storeControllerScript.subtractCurrentRP(650);
								storeControllerScript.updateRPPref();
								uziUpgraded = 1;
								PlayerPrefs.SetInt("uziUpgraded",1);
								primaryController.setUpgraded(4);
								soundControllerScript.playSFX("buy");
							}
						}else{
							GUI.enabled = false;
							if (GUI.Button (new Rect (columnTwoX-30, yTwo+50, equipWidth+65, equipHeight), "Not Enough RP: 650 RP")){}//Button - not enough rp
						}
					}else {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-30, yTwo+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
					}
					GUI.enabled = true;
				} else {//Purchase Shotgun Options
					if (playerScript.getCurrentScraps() < 1699) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-15, yTwo-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n1700 Scrap")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnTwoX-15, yTwo-5, equipWidth+35, equipHeight+15), "Purchase Uzi\n1700 Scrap")) {
							playerScript.subtractScraps(1700);
							uziPurchased = 1;
							PlayerPrefs.SetInt ("UziPurchased", 1);
							soundControllerScript.playSFX("buy");
							
							primaryController.setInventorySlotOne(4);
							primaryController.equipWeapon (4);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
					if (rpControllerScript.getCurrentRP() < 1170) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-15, yTwo+35, equipWidth+35, equipHeight+15), "Not Enough RP\n1170 RP")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnTwoX-15, yTwo+35, equipWidth+35, equipHeight+15), "Purchase Uzi\n1170 RP")) {
							storeControllerScript.subtractCurrentRP(1170);
							storeControllerScript.updateRPPref();
							uziPurchased = 1;
							PlayerPrefs.SetInt ("UziPurchased", 1);
							soundControllerScript.playSFX("buy");
							
							primaryController.setInventorySlotOne(4);
							primaryController.equipWeapon (4);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
				}
			} else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (columnTwoX, yTwo, equipWidth, equipHeight), "Not Round 3")){}
			}
			/// Uzi Section End
			
			/// Grenade Section Start
			if (zombieSpawnerScript.getCurrentRound() >= 3) {
				if (grenadePurchased == 1) {//If Launcher Purchased
					if(primaryController.getCurrentlyEquipedWeapon() == 5){
						GUI.enabled = false;
					}
					if (GUI.Button (new Rect (columnOneX, yThree, equipWidth, equipHeight), "Equip Grenades")) {
						primaryController.setInventorySlotOne(5);
						primaryController.equipWeapon (5);
						soundControllerScript.playSFX("gunCock");
					}
					GUI.enabled = true;
					if (playerScript.getCurrentScraps() < 14) {
						GUI.enabled = false;//Grey out GUI
						if (GUI.Button (new Rect (columnOneX-30, yThree+25, equipWidth+65, equipHeight), "Not Enough Scrap")){}
					} else if (GUI.Button (new Rect (columnOneX-30, yThree+25, equipWidth+65, equipHeight), "Buy Ammo: 15 scrap")) {
							playerScript.subtractScraps(15);
							primaryController.addAmmo(5,2);
							soundControllerScript.playSFX("buy");
					}
					GUI.enabled = true;
					if(grenadeUpgraded == 0){				
						if(rpControllerScript.getCurrentRP() > 649){
							if (GUI.Button (new Rect (columnOneX-30, yThree+50, equipWidth+65, equipHeight), "Upgrade Grenades: 650 RP")) {	
								storeControllerScript.subtractCurrentRP(650);
								storeControllerScript.updateRPPref();
								grenadeUpgraded = 1;
								PlayerPrefs.SetInt("grenadeUpgraded",1);
								primaryController.setUpgraded(5);
								soundControllerScript.playSFX("buy");
							}
						}else{
							GUI.enabled = false;
							if (GUI.Button (new Rect (columnOneX-30, yThree+50, equipWidth+65, equipHeight), "Not Enough RP: 650 RP")){}//Button - not enough rp
						}
					}else {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-30, yThree+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
					}
					GUI.enabled = true;
				} else {//Purchase Shotgun Options
					if (playerScript.getCurrentScraps() < 1699) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-15, yThree-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n1700 Scrap")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnOneX-15, yThree-5, equipWidth+35, equipHeight+15), "Purchase Grenades\n1700 Scrap")) {	
							playerScript.subtractScraps(1700);
							grenadePurchased = 1;
							PlayerPrefs.SetInt ("GrenadePurchased", 1);
							soundControllerScript.playSFX("buy");
							
							primaryController.setInventorySlotOne(5);
							primaryController.equipWeapon (5);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
					if (rpControllerScript.getCurrentRP() < 1170) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-15, yThree+35, equipWidth+35, equipHeight+15), "Not Enough RP\n1170 RP")){}
					} else {					
						if (GUI.Button (new Rect (columnOneX-15, yThree+35, equipWidth+35, equipHeight+15), "Purchase Grenades\n1170 RP")) {
							storeControllerScript.subtractCurrentRP(1170);
							storeControllerScript.updateRPPref();
							grenadePurchased = 1;
							PlayerPrefs.SetInt ("GrenadePurchased", 1);
							soundControllerScript.playSFX("buy");

							primaryController.setInventorySlotOne(5);
							primaryController.equipWeapon (5);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
				}
			} else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (columnOneX, yThree, equipWidth, equipHeight), "Not Round 3")) {
				}
			}
			/// Grenade Section end
			
			/// Gatling Gun Section Start
			if (zombieSpawnerScript.getCurrentRound() >= 4) {
				if (gatlingGunPurchased == 1) {//If Launcher Purchased
					if(primaryController.getCurrentlyEquipedWeapon() == 6){
						GUI.enabled = false;
					}
					if (GUI.Button (new Rect (columnTwoX, yThree, equipWidth, equipHeight), "Equip GatlingGun")) {	
						primaryController.setInventorySlotOne(6);
						primaryController.equipWeapon (6);
						soundControllerScript.playSFX("gunCock");
					}
					GUI.enabled = true;
					if (playerScript.getCurrentScraps() < 74) {
						GUI.enabled = false;//Grey out GUI
						if (GUI.Button (new Rect (columnTwoX-30, yThree+25, equipWidth+65, equipHeight), "Not Enough Scrap")){}
					} else if (GUI.Button (new Rect (columnTwoX-30, yThree+25, equipWidth+65, equipHeight), "Buy Ammo: 75 scrap")) {
							playerScript.subtractScraps(75);
							primaryController.addAmmo(6,15);
							soundControllerScript.playSFX("buy");
					}
					GUI.enabled = true;
					if(gatlingGunUpgraded == 0){				
						if(rpControllerScript.getCurrentRP() > 1379){
							if (GUI.Button (new Rect (columnTwoX-30, yThree+50, equipWidth+65, equipHeight), "Upgrade Gatling: 1380 RP")) {	
								storeControllerScript.subtractCurrentRP(1380);
								storeControllerScript.updateRPPref();
								gatlingGunUpgraded = 1;
								PlayerPrefs.SetInt("gatlingGunUpgraded",1);
								primaryController.setUpgraded(6);
								soundControllerScript.playSFX("buy");
							}
						}else{
							GUI.enabled = false;
							if (GUI.Button (new Rect (columnTwoX-30, yThree+50, equipWidth+65, equipHeight), "Not Enough RP: 1380 RP")){}//Button - not enough rp
						}
					}else {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-30, yThree+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
					}
					GUI.enabled = true;
				} else {//Purchase Shotgun Options
					if (playerScript.getCurrentScraps() < 2199) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-15, yThree-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n2200 Scrap")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnTwoX-15, yThree-5, equipWidth+35, equipHeight+15), "Purchase GatlingGun\n2200 Scrap")) {	
							playerScript.subtractScraps(2200);
							gatlingGunPurchased = 1;
							PlayerPrefs.SetInt ("GatlingGunPurchased", 1);
							soundControllerScript.playSFX("buy");

							primaryController.setInventorySlotOne(6);
							primaryController.equipWeapon (6);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
					if (rpControllerScript.getCurrentRP() < 1580) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-15, yThree+35, equipWidth+35, equipHeight+15), "Not Enough RP\n1580 RP")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnTwoX-15, yThree+35, equipWidth+35, equipHeight+15), "Purchase GatlingGun\n1580 RP")) {
							storeControllerScript.subtractCurrentRP(1580);
							storeControllerScript.updateRPPref();
							gatlingGunPurchased = 1;
							PlayerPrefs.SetInt ("GatlingGunPurchased", 1);
							soundControllerScript.playSFX("buy");
							
							primaryController.setInventorySlotOne(6);
							primaryController.equipWeapon (6);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
				}
			} else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (columnTwoX, yThree, equipWidth, equipHeight), "Not Round 4")) {
				}
			}
			/// Gatling Gun Section end
			
			/// CrossBow Section Start
			if (zombieSpawnerScript.getCurrentRound() >= 4) {
				if (crossBowPurchased == 1) {//If Launcher Purchased
					if(primaryController.getCurrentlyEquipedWeapon() == 7){
						GUI.enabled = false;
					}
					if (GUI.Button (new Rect (columnOneX, yFour, equipWidth, equipHeight), "Equip CrossBow")) {	
						primaryController.setInventorySlotOne(7);
						primaryController.equipWeapon (7);
						soundControllerScript.playSFX("gunCock");
					}
					GUI.enabled = true;
					if (playerScript.getCurrentScraps() < 14) {
						GUI.enabled = false;//Grey out GUI
						if (GUI.Button (new Rect (columnOneX-30, yFour+25, equipWidth+65, equipHeight), "Not Enough Scrap")){}
					} else if (GUI.Button (new Rect (columnOneX-30, yFour+25, equipWidth+65, equipHeight), "Buy Ammo: 15 scrap")) {
							playerScript.subtractScraps(15);
							primaryController.addAmmo(7,2);
							soundControllerScript.playSFX("buy");
					}			
					GUI.enabled = true;
					if(crossBowUpgraded == 0){				
						if(rpControllerScript.getCurrentRP() > 1379){
							if (GUI.Button (new Rect (columnOneX-30, yFour+50, equipWidth+65, equipHeight), "Upgrade CrossBow: 1380 RP")) {	
								storeControllerScript.subtractCurrentRP(1380);
								storeControllerScript.updateRPPref();
								crossBowUpgraded = 1;
								PlayerPrefs.SetInt("crossBowUpgraded",1);
								primaryController.setUpgraded(7);
								soundControllerScript.playSFX("buy");
							}
						}else{
							GUI.enabled = false;
							if (GUI.Button (new Rect (columnOneX-30, yFour+50, equipWidth+65, equipHeight), "Not Enough RP: 1380 RP")){}//Button - not enough rp
						}
					}else {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-30, yFour+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
					}
					GUI.enabled = true;
				} else {//Purchase Shotgun Options
					if (playerScript.getCurrentScraps() < 2499) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-15, yFour-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n2500 Scrap")){}
					} else {					
						if (GUI.Button (new Rect (columnOneX-15, yFour-5, equipWidth+35, equipHeight+15), "Purchase CrossBow\n2500 Scrap")) {	
							playerScript.subtractScraps(2500);
							crossBowPurchased = 1;
							PlayerPrefs.SetInt ("CrossBowPurchased", 1);
							soundControllerScript.playSFX("buy");

							primaryController.setInventorySlotOne(7);
							primaryController.equipWeapon (7);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;				
					if (rpControllerScript.getCurrentRP() < 1580) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-15, yFour+35, equipWidth+35, equipHeight+15), "Not Enough RP\n1580 RP")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnOneX-15, yFour+35, equipWidth+35, equipHeight+15), "Purchase CrossBow\n1580 RP")) {
							storeControllerScript.subtractCurrentRP(1580);
							storeControllerScript.updateRPPref();
							crossBowPurchased = 1;
							PlayerPrefs.SetInt ("CrossBowPurchased", 1);
							soundControllerScript.playSFX("buy");

							primaryController.setInventorySlotOne(7);
							primaryController.equipWeapon (7);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
				}
			} else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (columnOneX, yFour, equipWidth, equipHeight), "Not Round 4")) {
				}
			}
			/// CrossBow Section End
			
			/// Bolter Section Start
			if (zombieSpawnerScript.getCurrentRound() >= 5) {
				if (bolterPurchased == 1) {//If Launcher Purchased
					if(primaryController.getCurrentlyEquipedWeapon() == 8){
						GUI.enabled = false;
					}
					if (GUI.Button (new Rect (columnTwoX, yFour, equipWidth, equipHeight), "Equip Bolter")) {	
						primaryController.setInventorySlotOne(8);
						primaryController.equipWeapon (8);
						soundControllerScript.playSFX("gunCock");
					}	
					GUI.enabled = true;
					if (playerScript.getCurrentScraps() < 74) {
						GUI.enabled = false;//Grey out GUI
						if (GUI.Button (new Rect (columnTwoX-30, yFour+25, equipWidth+65, equipHeight), "Not Enough Scrap")){}
					} else if (GUI.Button (new Rect (columnTwoX-30, yFour+25, equipWidth+65, equipHeight), "Buy Ammo: 75 scrap")) {
							playerScript.subtractScraps(75);
							primaryController.addAmmo(8,5);
							soundControllerScript.playSFX("buy");
					}						
					GUI.enabled = true;
					if(bolterUpgraded == 0){				
						if(rpControllerScript.getCurrentRP() > 1579){
							if (GUI.Button (new Rect (columnTwoX-30, yFour+50, equipWidth+65, equipHeight), "Upgrade Bolter: 1580 RP")) {	
								storeControllerScript.subtractCurrentRP(1580);
								storeControllerScript.updateRPPref();
								bolterUpgraded = 1;
								PlayerPrefs.SetInt("bolterUpgraded",1);
								primaryController.setUpgraded(8);
								soundControllerScript.playSFX("buy");
							}
						}else{
							GUI.enabled = false;
							if (GUI.Button (new Rect (columnTwoX-30, yFour+50, equipWidth+65, equipHeight), "Not Enough RP: 1580 RP")){}//Button - not enough rp
						}
					}else {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-30, yFour+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
					}
					GUI.enabled = true;
				} else {//Purchase Bolter Options
					if (playerScript.getCurrentScraps() < 2699) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-15, yFour-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n2700 Scrap")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnTwoX-15, yFour-5, equipWidth+35, equipHeight+15), "Purchase Bolter\n2700 Scrap")) {	
							playerScript.subtractScraps(2700);
							bolterPurchased = 1;
							PlayerPrefs.SetInt ("BolterPurchased", 1);
							soundControllerScript.playSFX("buy");
							
							primaryController.setInventorySlotOne(8);
							primaryController.equipWeapon (8);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;					
					if (rpControllerScript.getCurrentRP() < 1760) {
						GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-15, yFour+35, equipWidth+35, equipHeight+15), "Not Enough RP\n1760 RP")) {
						}
					} else {					
						if (GUI.Button (new Rect (columnTwoX-15, yFour+35, equipWidth+35, equipHeight+15), "Purchase Bolter\n1760 RP")) {
							storeControllerScript.subtractCurrentRP(1760);
							storeControllerScript.updateRPPref();
							bolterPurchased = 1;
							PlayerPrefs.SetInt ("BolterPurchased", 1);
							soundControllerScript.playSFX("buy");
							
							primaryController.setInventorySlotOne(8);
							primaryController.equipWeapon (8);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
				}
			} else {
				GUI.enabled = false;
				if (GUI.Button (new Rect (columnTwoX, yFour, equipWidth, equipHeight), "Not Round 5")) {
				}
			}
			/// Bolter Section End
			GUI.enabled = true;

			/// DildoCannon Section Start
			if (zombieSpawnerScript.getCurrentRound() >= 5) {
				if (dildoCannonPurchased == 1) {//If Launcher Purchased		
					if(primaryController.getCurrentlyEquipedWeapon() == 9){
						GUI.enabled = false;
					}
					if (GUI.Button (new Rect (columnOneX, yFive, equipWidth, equipHeight), "Equip D-Cannon")) {	
						primaryController.setInventorySlotOne(9);
						primaryController.equipWeapon (9);
						soundControllerScript.playSFX("gunCock");
					}	
					GUI.enabled = true;
					if (playerScript.getCurrentScraps() < 79) {
						GUI.enabled = false;//Grey out GUI
					if (GUI.Button (new Rect (columnOneX-30, yFive+25, equipWidth+65, equipHeight), "Not Enough Scrap")) {}
				} else if (GUI.Button (new Rect (columnOneX-30, yFive+25, equipWidth+65, equipHeight), "Buy Ammo: 80 scrap")) {
							playerScript.subtractScraps(80);
							primaryController.addAmmo(9,3);
							soundControllerScript.playSFX("buy");
					}					
					GUI.enabled = true;
					if(dildoUpgraded == 0){				
						if(rpControllerScript.getCurrentRP() > 1579){
						if (GUI.Button (new Rect (columnOneX-30, yFive+50, equipWidth+65, equipHeight), "Upgrade D-Cannon: 1580 RP")) {	
								storeControllerScript.subtractCurrentRP(1580);
								storeControllerScript.updateRPPref();
								dildoUpgraded = 1;
								PlayerPrefs.SetInt("dildoUpgraded",1);
								primaryController.setUpgraded(9);
								soundControllerScript.playSFX("buy");
							}
						}else{
							GUI.enabled = false;
						if (GUI.Button (new Rect (columnOneX-30, yFive+50, equipWidth+65, equipHeight), "Not Enough RP: 1580 RP")){}//Button - not enough rp
						}
					}else {
						GUI.enabled = false;
					if (GUI.Button (new Rect (columnOneX-30, yFive+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
					}
					GUI.enabled = true;
				} else {//Purchase Dildo Options
					if (playerScript.getCurrentScraps() < 2999) {
						GUI.enabled = false;
					if (GUI.Button (new Rect (columnOneX-15, yFive-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n3000 Scrap")) {
						}
					} else {					
					if (GUI.Button (new Rect (columnOneX-15, yFive-5, equipWidth+35, equipHeight+15), "Purchase D-Cannon\n3000 Scrap")) {	
							playerScript.subtractScraps(3000);
							dildoCannonPurchased = 1;
							PlayerPrefs.SetInt ("DildoCannonPurchased", 1);
							soundControllerScript.playSFX("buy");
							
							primaryController.setInventorySlotOne(9);
							primaryController.equipWeapon (9);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
					if (rpControllerScript.getCurrentRP() < 1760) {
						GUI.enabled = false;
					if (GUI.Button (new Rect (columnOneX-15, yFive+35, equipWidth+35, equipHeight+15), "Not Enough RP\n1760 RP")) {
						}
					} else {					
					if (GUI.Button (new Rect (columnOneX-15, yFive+35, equipWidth+35, equipHeight+15), "Purchase D-Cannon\n1760 RP")) {
							storeControllerScript.subtractCurrentRP(1760);
							storeControllerScript.updateRPPref();
							dildoCannonPurchased = 1;
							PlayerPrefs.SetInt ("DildoCannonPurchased", 1);
							soundControllerScript.playSFX("buy");

							primaryController.setInventorySlotOne(9);
							primaryController.equipWeapon (9);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
				}
			} else {
				GUI.enabled = false;
			if (GUI.Button (new Rect (columnOneX, yFive, equipWidth, equipHeight), "Not Round 6")) {
				}
			}
			/// DildoCannon Section End
			
			
			/// LaserCannon Section Start
			if (zombieSpawnerScript.getCurrentRound() >= 10) {
				if (laserCannonPurchased == 1) {//If Launcher Purchased
					if(primaryController.getCurrentlyEquipedWeapon() == 10){
						GUI.enabled = false;
					}
					if (GUI.Button (new Rect (columnTwoX, yFive, equipWidth, equipHeight), "Equip L-Cannon")) {	
						primaryController.setInventorySlotOne(10);
						primaryController.equipWeapon (10);
						soundControllerScript.playSFX("gunCock");
					}
					GUI.enabled = true;
					if(laserUpgraded == 0){				
						if(rpControllerScript.getCurrentRP() > 2799){
						if (GUI.Button (new Rect (columnTwoX-30, yFive+50, equipWidth+65, equipHeight), "Upgrade L-Cannon: 2800 RP")) {	
								storeControllerScript.subtractCurrentRP(2800);
								storeControllerScript.updateRPPref();
								laserUpgraded = 1;
								PlayerPrefs.SetInt("laserUpgraded",1);
								primaryController.setUpgraded(10);
								soundControllerScript.playSFX("buy");
							}
						}else{
							GUI.enabled = false;
						if (GUI.Button (new Rect (columnTwoX-30, yFive+50, equipWidth+65, equipHeight), "Not Enough RP: 2800 RP")){}//Button - not enough rp
						}
					}else {
						GUI.enabled = false;
					if (GUI.Button (new Rect (columnTwoX-30, yFive+50, equipWidth+65, equipHeight), "Already Upgraded")){}//Already Upgraded
					}
					GUI.enabled = true;
				} else {//Purchase L-Cannon Options
					if (playerScript.getCurrentScraps() < 3499) {
						GUI.enabled = false;
					if (GUI.Button (new Rect (columnTwoX-15, yFive-5, equipWidth+35, equipHeight+15), "Not Enough Scrap\n3500 Scrap")) {
						}
					} else {					
					if (GUI.Button (new Rect (columnTwoX-15, yFive-5, equipWidth+35, equipHeight+15), "Purchase L-Cannon\n3500 Scrap")) {	
							playerScript.subtractScraps(3500);
							laserCannonPurchased = 1;
							PlayerPrefs.SetInt ("LaserCannonPurchased", 1);	
							soundControllerScript.playSFX("buy");

							primaryController.setInventorySlotOne(10);
							primaryController.equipWeapon (10);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
					if (rpControllerScript.getCurrentRP() < 1950) {
						GUI.enabled = false;
					if (GUI.Button (new Rect (columnTwoX-15, yFive+35, equipWidth+35, equipHeight+15), "Not Enough RP\n1950 RP")) {
						}
					} else {					
					if (GUI.Button (new Rect (columnTwoX-15, yFive+35, equipWidth+35, equipHeight+15), "Purchase L-Cannon\n1950 RP")) {
							storeControllerScript.subtractCurrentRP(1950);
							storeControllerScript.updateRPPref();
							laserCannonPurchased = 1;
							PlayerPrefs.SetInt ("LaserCannonPurchased", 1);
							soundControllerScript.playSFX("buy");

							primaryController.setInventorySlotOne(10);
							primaryController.equipWeapon (10);
							soundControllerScript.playSFX("gunCock");
						}
					}
					GUI.enabled = true;
				}				
			} else {
				GUI.enabled = false;
			if (GUI.Button (new Rect (columnTwoX, yFive, equipWidth, equipHeight), "Not Round 10")) {
				}
			}
			GUI.enabled = true;
			/// LaserCannon Section End

	}

	void drawEquipedSquare(){// Method to Draw Square behind currently equiped weapon & Secondary	
		if (primaryController.getInventorySlotOne() == 1) {
			storeControllerScript.DrawEquippedSquare(330,20);//Draw Fancy Texture Code Here
		} else if (primaryController.getInventorySlotOne () == 2) {
			storeControllerScript.DrawEquippedSquare(555,20);//Draw Fancy Texture Code Here
		} else if (primaryController.getInventorySlotOne () == 3) {
			storeControllerScript.DrawEquippedSquare(330,100);//Draw Fancy Texture Code Here
		} else if (primaryController.getInventorySlotOne () == 4) {
			storeControllerScript.DrawEquippedSquare(555,100);//Draw Fancy Texture Code Here
		} else if (primaryController.getInventorySlotOne () == 5) {
			storeControllerScript.DrawEquippedSquare(330,180);//Draw Fancy Texture Code Here
		} else if (primaryController.getInventorySlotOne () == 6) {
			storeControllerScript.DrawEquippedSquare(555,180);//Draw Fancy Texture Code Here
		} else if (primaryController.getInventorySlotOne () == 7) {
			storeControllerScript.DrawEquippedSquare(330,260);//Draw Fancy Texture Code Here
		} else if (primaryController.getCurrentlyEquipedWeapon () == 8) {
			storeControllerScript.DrawEquippedSquare(555,260);//Draw Fancy Texture Code Here
		}else if(primaryController.getInventorySlotOne() == 9){
			storeControllerScript.DrawEquippedSquare(330,340);//Draw Fancy Texture Code Here
		}else if(primaryController.getInventorySlotOne() == 10){
			storeControllerScript.DrawEquippedSquare(555,340);//Draw Fancy Texture Code Here
		}
	}
	public void reset(){
		 shotGunPurchased = 0;
		 rocketLauncherPurchased = 0;
		 uziPurchased = 0;
		 grenadePurchased = 0;
		 gatlingGunPurchased = 0;
		 crossBowPurchased = 0;
		 bolterPurchased = 0;
		 dildoCannonPurchased = 0;
		 laserCannonPurchased = 0;
		
		/// <Upgrades Start>	
		 shotGunUpgraded = 0; // Get from PlayerPref
		 rifleUpgraded = 0;
		 rocketUpgraded = 0;
		 uziUpgraded = 0;
		 grenadeUpgraded = 0;
		 gatlingGunUpgraded = 0;
		 crossBowUpgraded = 0;
		 bolterUpgraded = 0;
		 dildoUpgraded = 0;
		 laserUpgraded = 0;
		/// </Upgrades End>
		
		 columnOneX = 370;
		 columnTwoX = 595;
		 equipWidth = 115;
		 equipHeight = 20;
		 yOne = 30;
		 yTwo = 110;
		 yThree = 190;
		 yFour = 270;
		 yFive = 350;

		if (PlayerPrefs.HasKey ("ShotGunPurchased")) {
			shotGunPurchased = PlayerPrefs.GetInt ("ShotGunPurchased");
		}		
		if (PlayerPrefs.HasKey ("RocketLauncherPurchased")) {
			rocketLauncherPurchased = PlayerPrefs.GetInt ("RocketLauncherPurchased");
		}
		if (PlayerPrefs.HasKey ("UziPurchased")) {
			uziPurchased = PlayerPrefs.GetInt ("UziPurchased");
		}
		if (PlayerPrefs.HasKey ("GrenadePurchased")) {
			grenadePurchased = PlayerPrefs.GetInt ("GrenadePurchased");
		}
		if (PlayerPrefs.HasKey ("GatlingGunPurchased")) {
			gatlingGunPurchased = PlayerPrefs.GetInt ("GatlingGunPurchased");
		}
		if (PlayerPrefs.HasKey ("CrossBowPurchased")) {
			crossBowPurchased = PlayerPrefs.GetInt ("CrossBowPurchased");
		}
		if (PlayerPrefs.HasKey ("BolterPurchased")) {
			bolterPurchased = PlayerPrefs.GetInt ("BolterPurchased");
		}
		if (PlayerPrefs.HasKey ("DildoCannonPurchased")) {
			dildoCannonPurchased = PlayerPrefs.GetInt ("DildoCannonPurchased");
		}
		if (PlayerPrefs.HasKey ("LaserCannonPurchased")) {
			laserCannonPurchased = PlayerPrefs.GetInt ("LaserCannonPurchased");
		}
		
		if(PlayerPrefs.HasKey("rifleUpgraded")){			
			rifleUpgraded = PlayerPrefs.GetInt ("rifleUpgraded");
			primaryController.setUpgraded(1);
		}
		if(PlayerPrefs.HasKey("shotGunUpgraded")){			
			shotGunUpgraded = PlayerPrefs.GetInt ("shotGunUpgraded");
			primaryController.setUpgraded(2);
		}
		if(PlayerPrefs.HasKey("rocketUpgraded")){			
			rocketUpgraded = PlayerPrefs.GetInt ("rocketUpgraded");
			primaryController.setUpgraded(3);
		}
		if(PlayerPrefs.HasKey("uziUpgraded")){			
			uziUpgraded = PlayerPrefs.GetInt ("uziUpgraded");
			primaryController.setUpgraded(4);
		}
		if(PlayerPrefs.HasKey("grenadeUpgraded")){			
			grenadeUpgraded = PlayerPrefs.GetInt ("grenadeUpgraded");
			primaryController.setUpgraded(5);
		}
		if(PlayerPrefs.HasKey("gatlingGunUpgraded")){			
			gatlingGunUpgraded = PlayerPrefs.GetInt ("gatlingGunUpgraded");
			primaryController.setUpgraded(6);
		}
		if(PlayerPrefs.HasKey("crossBowUpgraded")){			
			crossBowUpgraded = PlayerPrefs.GetInt ("crossBowUpgraded");
			primaryController.setUpgraded(7);
		}
		if(PlayerPrefs.HasKey("bolterUpgraded")){			
			bolterUpgraded = PlayerPrefs.GetInt ("bolterUpgraded");
			primaryController.setUpgraded(8);
		}
		if(PlayerPrefs.HasKey("dildoUpgraded")){			
			dildoUpgraded = PlayerPrefs.GetInt ("dildoUpgraded");
			primaryController.setUpgraded(9);
		}
		if(PlayerPrefs.HasKey("laserUpgraded")){			
			laserUpgraded = PlayerPrefs.GetInt ("laserUpgraded");
			primaryController.setUpgraded(10);
		}
	}
}
