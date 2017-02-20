using UnityEngine;
using System.Collections;

public class PrimaryController : MonoBehaviour {

	int pistolAmmo = 0;
	int rifleAmmo = 0;
	int shotGunAmmo = 0;
	int rocketAmmo = 0;
	int uziAmmo = 0;
	int grenadeAmmo = 0;
	int gatlingGunAmmo = 0;
	int crossBowAmmo = 0;
	int bolterAmmo = 0;	
	int dildoAmmo = 0;

	int currentlyEquipedWeapon = 1; //0-Pistol, 1-Rifle, 2-Shotgun, 3-RocketLauncher
	int inventorySlotOne = 1;//1-Rifle, 2-Shotgun, 3-RocketLauncher

	float pistolFireRate = 0f;
	float rifleFireRate = 0f;
	float shotGunFireRate = 0f;
	float rocketFireRate = 0f;
	float uziFireRate = 0f;
	float grenadeFireRate = 0f;
	float gatlingGunFireRate = 0f;
	float crossBowFireRate = 0f;
	float bolterFireRate = 0f;
	float dildoFireRate = 0f;
	
	//float laserFireRate = 0f;
	float laserRate = 3f;
	float laserCharge = 0f;

	/// <Upgrades Start>
	int shotGunUpgraded = 0;
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


	PlayerController playerScript;
	PoolingSystem pS;
	
	GameObject soundController;
	SoundController soundControllerScript;

	void Start () {
		playerScript = this.GetComponent<PlayerController> ();
		pS = PoolingSystem.Instance;
		
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();

		pistolAmmo = 1;
		rifleAmmo = 40;		
		shotGunAmmo = 20;		
		rocketAmmo = 8;
		uziAmmo = 30; 
		grenadeAmmo = 15;
		gatlingGunAmmo = 25;
		crossBowAmmo = 15;
		bolterAmmo = 25;
		dildoAmmo = 35;

		laserCharge = laserRate;
	}

	void Update () {
		pistolFireRate-=Time.deltaTime;//Keep Track of Pistol Fire Rate
		rifleFireRate-=Time.deltaTime;
		shotGunFireRate-=Time.deltaTime;		
		rocketFireRate-=Time.deltaTime;
		uziFireRate-=Time.deltaTime;		
		grenadeFireRate-=Time.deltaTime;
		gatlingGunFireRate-=Time.deltaTime;		
		crossBowFireRate-=Time.deltaTime;
		bolterFireRate-=Time.deltaTime;
		dildoFireRate-=Time.deltaTime;

		if (laserCharge < laserRate) {//If needs to Charge, charge
			laserCharge += Time.deltaTime; //Charge Laser
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)){//If Number 1, switch to weapon 1
			equipWeapon(0);
			soundControllerScript.playSFX("gunCock");
		}else if (Input.GetKeyDown(KeyCode.Alpha1)){//If Number 2, swtich to Pistol
			equipWeapon(inventorySlotOne);
			soundControllerScript.playSFX("gunCock");
		}
	}

	public void shoot(GameObject bulletSpawnPoint, GameObject shotGunSpawn1, GameObject shotGunSpawn2, GameObject shotGunSpawn3, GameObject shotGunSpawn4, GameObject gatlingSpawnPoint){
		if(currentlyEquipedWeapon == 0){//Check if Current Gun is Pistol
			if(pistolAmmo > 0){//Make sure you have Pistol Ammo
				if(pistolFireRate <= 0){
					pS.InstantiateAPS("PistolBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);					
					soundControllerScript.playSFX("Pistol");//Play Firing Sound
					pistolFireRate = 1f; // Reset Bullet Fire Rate so it counts down and fires onces every second
				}
			}
		}else if(currentlyEquipedWeapon == 1){//Check if Current Gun is Rifle
			if(rifleAmmo > 0){//Make sure you have Rifle Ammo
				if(rifleFireRate <= 0){
					pS.InstantiateAPS("RifleBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
					if(rifleUpgraded == 1){
						rifleFireRate = .3f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}else {
						rifleFireRate = .7f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}
					soundControllerScript.playSFX("Rifle");//Play Firing Sound
					rifleAmmo--;
				}
			}else {//If out of Rifle Ammo switch to Pistol
				equipWeapon(0);
			}
		}else if(currentlyEquipedWeapon == 2){//Check if Current Gun is Shotgun
			if(shotGunAmmo > 0){//Make sure you have Rifle Ammo
				if(shotGunFireRate <= 0){
					pS.InstantiateAPS("ShotGunBullet", shotGunSpawn1.transform.position, shotGunSpawn1.transform.rotation);
					pS.InstantiateAPS("ShotGunBullet", shotGunSpawn2.transform.position, shotGunSpawn2.transform.rotation);
					pS.InstantiateAPS("ShotGunBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
					pS.InstantiateAPS("ShotGunBullet", shotGunSpawn3.transform.position, shotGunSpawn3.transform.rotation);
					pS.InstantiateAPS("ShotGunBullet", shotGunSpawn4.transform.position, shotGunSpawn4.transform.rotation);

					if(shotGunUpgraded == 1){
						shotGunFireRate = .3f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}else {
						shotGunFireRate = 1.2f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}
					soundControllerScript.playSFX("Shotgun");//Play Firing Sound
					shotGunAmmo--;
				}
			}else {//If out of ShotGun Ammo switch to Pistol
				equipWeapon(0);
			}
		}else if(currentlyEquipedWeapon == 3){//Check if Current Gun is Rocket
			if(rocketAmmo > 0){//Make sure you have Rifle Ammo
				if(rocketFireRate <= 0){
					if(rocketUpgraded == 1){
						pS.InstantiateAPS("RocketBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						pS.InstantiateAPS("RocketBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						rocketFireRate = 1.0f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}else {
						pS.InstantiateAPS("RocketBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						rocketFireRate = 1.5f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}					
					soundControllerScript.playSFX("Rocket");//Play Firing Sound
					rocketAmmo--;
				}
			}else {//If out of rocket Ammo switch to Pistol
				equipWeapon(0);;
			}
		}else if(currentlyEquipedWeapon == 4){//Check if Current Gun is Uzi
			if(uziAmmo > 0){//Make sure you have Uzi Ammo
				if(uziFireRate <= 0){
					//Play Firing Sound						
					if(uziUpgraded == 1){
						pS.InstantiateAPS("UziBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						pS.InstantiateAPS("UziBullet", shotGunSpawn2.transform.position, shotGunSpawn2.transform.rotation);
						pS.InstantiateAPS("UziBullet", shotGunSpawn3.transform.position, shotGunSpawn3.transform.rotation);
						uziFireRate = .1f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}else {
						pS.InstantiateAPS("UziBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						uziFireRate = .2f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}
					soundControllerScript.playSFX("Pistol");//Play Firing Sound
					uziAmmo--;
				}
			}else {//If out of Uzi Ammo switch to Pistol
				equipWeapon(0);
			}
		}else if(currentlyEquipedWeapon == 5){//Check if Current Gun is Grenade
			if(grenadeAmmo > 0){//Make sure you have Grenade Ammo
				if(grenadeFireRate <= 0){
					if (grenadeUpgraded == 1){
						pS.InstantiateAPS("GrenadeBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						pS.InstantiateAPS("GrenadeBullet", shotGunSpawn1.transform.position, shotGunSpawn1.transform.rotation);
						pS.InstantiateAPS("GrenadeBullet", shotGunSpawn4.transform.position, shotGunSpawn4.transform.rotation);
						
						grenadeFireRate = .5f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}else {
						pS.InstantiateAPS("GrenadeBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						grenadeFireRate = 1f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}
					soundControllerScript.playSFX("Grenade");//Play Firing Sound
					grenadeAmmo--;
				}
			}else {//If out of Grenade Ammo switch to Pistol
				equipWeapon(0);
			}
		}else if(currentlyEquipedWeapon == 6){//Check if Current Gun is GatlingGun
			if(gatlingGunAmmo > 0){//Make sure you have GatlingGun Ammo
				if(gatlingGunFireRate <= 0){
					if (gatlingGunUpgraded == 1){
						pS.InstantiateAPS("GatlingGunBullet", shotGunSpawn1.transform.position, shotGunSpawn1.transform.rotation);
						pS.InstantiateAPS("GatlingGunBullet", shotGunSpawn2.transform.position, shotGunSpawn2.transform.rotation);
						pS.InstantiateAPS("GatlingGunBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						pS.InstantiateAPS("GatlingGunBullet", shotGunSpawn3.transform.position, shotGunSpawn3.transform.rotation);
						pS.InstantiateAPS("GatlingGunBullet", shotGunSpawn4.transform.position, shotGunSpawn4.transform.rotation);
						
						gatlingGunFireRate = .025f; // Reset Bullet Fire Rate so it counts down and fires onces every second		
					}else {
						pS.InstantiateAPS("GatlingGunBullet", gatlingSpawnPoint.transform.position, gatlingSpawnPoint.transform.rotation);
						gatlingGunFireRate = .03f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}
					soundControllerScript.playSFX("Pistol");//Play Firing Sound
					gatlingGunAmmo--;
				}
			}else {//If out of Grenade Ammo switch to Pistol
				equipWeapon(0);
			}
		}else if(currentlyEquipedWeapon == 7){//Check if Current Gun is CrossBow
			if(crossBowAmmo > 0){//Make sure you have CrossBow Ammo
				if(crossBowFireRate <= 0){
					if(crossBowUpgraded == 1){
						pS.InstantiateAPS("CrossBowBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						pS.InstantiateAPS("CrossBowBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

						crossBowFireRate = .7f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}else {
						pS.InstantiateAPS("CrossBowBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						crossBowFireRate = 1f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}
					soundControllerScript.playSFX("crossbowShot");//Play Firing Sound
					crossBowAmmo--;
				}
			}else {//If out of Grenade Ammo switch to Pistol
				equipWeapon(0);
			}
		}else if(currentlyEquipedWeapon == 8){//Check if Current Gun is Bolter
			if(bolterAmmo > 0){//Make sure you have Bolter Ammo
				if(bolterFireRate <= 0){				
					if(bolterUpgraded == 1){
						pS.InstantiateAPS("BolterBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						pS.InstantiateAPS("BolterBullet", shotGunSpawn2.transform.position, shotGunSpawn2.transform.rotation);
						pS.InstantiateAPS("BolterBullet", shotGunSpawn3.transform.position, shotGunSpawn3.transform.rotation);						
						bolterFireRate = .45f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}else{
						pS.InstantiateAPS("BolterBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						bolterFireRate = .7f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}
					soundControllerScript.playSFX("Rifle");//Play Firing Sound
					bolterAmmo--;
				}
			}else {//If out of Grenade Ammo switch to Pistol
				equipWeapon(0);
			}
		}else if(currentlyEquipedWeapon == 9){//Check if Current Gun is Dildo
			if(dildoAmmo > 0){//Make sure you have Dildo Ammo
				if(dildoFireRate <= 0){			
					if(dildoUpgraded == 1){
						pS.InstantiateAPS("DildoBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						pS.InstantiateAPS("DildoBullet", shotGunSpawn1.transform.position, shotGunSpawn1.transform.rotation);
						pS.InstantiateAPS("DildoBullet", shotGunSpawn4.transform.position, shotGunSpawn4.transform.rotation);
						dildoFireRate = .35f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}else {
						pS.InstantiateAPS("DildoBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
						dildoFireRate = 1f; // Reset Bullet Fire Rate so it counts down and fires onces every second
					}
					soundControllerScript.playSFX("Shotgun");//Play Firing Sound
					dildoAmmo--;
				}
			}else {//If out of Grenade Ammo switch to Pistol
				equipWeapon(0);
			}
		}else if(currentlyEquipedWeapon == 10){//Check if Current Gun is Laser
			if(laserCharge >= laserRate){//Make sure you have Laser Ammo
				if (laserUpgraded == 1){
					pS.InstantiateAPS("LaserBullet", shotGunSpawn2.transform.position, shotGunSpawn2.transform.rotation);
					pS.InstantiateAPS("LaserBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
					pS.InstantiateAPS("LaserBullet", shotGunSpawn3.transform.position, shotGunSpawn3.transform.rotation);
					
					laserCharge = 0;
					laserRate = .2f; // Reset Bullet Fire Rate so it counts down and fires onces every second
				}else {
					pS.InstantiateAPS("LaserBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
					laserCharge = 0;
					laserRate = 3f; // Reset Bullet Fire Rate so it counts down and fires onces every second
				}
				soundControllerScript.playSFX("Laser");//Play Firing Sound					
			}
		}
	}

	public void equipWeapon(int selectedWeapon){
		currentlyEquipedWeapon = selectedWeapon;
		playerScript.setPlayerSprite();
	}
	public void addRandomAmmo(int randomAmmoAmount){
		if(inventorySlotOne == 0){//Check if Current Gun is Pistol				
			//pistolAmmo+=randomAmmoAmount;//Pistol is Infinite Ammo
		}else if(inventorySlotOne == 1){//Check if Current Gun is Rifle				
			rifleAmmo+=randomAmmoAmount;//Random Amount of Ammo	
		}else if(inventorySlotOne == 2){//Check if Current Gun is Shotgun				
			shotGunAmmo+=randomAmmoAmount;//Random Amount of Ammo	
		}else if(inventorySlotOne == 3){//Check if Current Gun is Rocket				
			rocketAmmo+=2;	
		}else if(inventorySlotOne == 4){//Check if Current Gun is Uzi				
			uziAmmo+=randomAmmoAmount;//Random Amount of Ammo
		}else if(inventorySlotOne == 5){//Check if Current Gun is Grenade				
			grenadeAmmo+=randomAmmoAmount;//Random Amount of Ammo
		}else if(inventorySlotOne == 6){//Check if Current Gun is GatlingGun				
			gatlingGunAmmo+=randomAmmoAmount;//Random Amount of Ammo
		}else if(inventorySlotOne == 7){//Check if Current Gun is CrossBow				
			crossBowAmmo+=randomAmmoAmount;//Random Amount of Ammo
		}else if(inventorySlotOne == 8){//Check if Current Gun is Bolter				
			bolterAmmo+=randomAmmoAmount;//Random Amount of Ammo
		}else if(inventorySlotOne == 9){//Check if Current Gun is Dildo				
			dildoAmmo+=1;//Random Amount of Ammo
		}else if(inventorySlotOne == 10){//Check if Current Gun is Dildo				
			laserCharge=laserRate;//Random Amount of Ammo
		}
	}
	public void addAmmo(int weapon,int amount){
		if(weapon == 0){ //Pistol
			//Pistol Has infinite ammo and ammo for it can never be purchased
		}else if(weapon == 1){ // Rifle
			rifleAmmo += amount;
		}else if(weapon == 2){ //Shotgun
			shotGunAmmo += amount;
		}else if(weapon == 3){ // Rocket
			rocketAmmo += amount;
		}else if(weapon == 4){// Uzi
			uziAmmo += amount;
		}else if(weapon == 5){// Grenade
			grenadeAmmo += amount;
		}else if(weapon == 6){// Gatling Gun
			gatlingGunAmmo += amount;
		}else if(weapon == 7){// Crossbow
			crossBowAmmo += amount;
		}else if(weapon == 8){// Bolter
			bolterAmmo += amount;
		}else if(weapon == 9){// Dildo Cannon
			dildoAmmo += amount;
		}else if(weapon == 10){// Laser
			//Laser has charge not ammo
		}
	}
	
	public int getAmmo(int weapon){
		if(weapon == 1){ // Rifle
			return rifleAmmo;
		}else if(weapon == 2){ //Shotgun
			return shotGunAmmo;
		}else if(weapon == 3){ // Rocket
			return rocketAmmo;
		}else if(weapon == 4){// Uzi
			return uziAmmo;
		}else if(weapon == 5){// Grenade
			return grenadeAmmo;
		}else if(weapon == 6){// Gatling Gun
			return gatlingGunAmmo;
		}else if(weapon == 7){// Crossbow
			return crossBowAmmo;
		}else if(weapon == 8){// Bolter
			return bolterAmmo;
		}else if(weapon == 9){// Dildo Cannon
			return dildoAmmo;
		}else if(weapon == 0){
			return 1000000;
		}else{
			return 0;
		}
	}
	public void setInventorySlotOne(int weapon){
		inventorySlotOne = weapon;
	}
	public int getInventorySlotOne(){
		return inventorySlotOne;
	}
	public int getCurrentlyEquipedWeapon(){
		return currentlyEquipedWeapon;
	}
	public float getLaserCharge(){
		return laserCharge;
	}
	public void setLaserCharge(float charge){
		laserCharge = charge;
	}
	public void setLaserChargeToMax(){
		laserCharge = laserRate;
	}

	public float getLaserRate(){
		return laserRate;
	}
	public void setUpgraded(int weapon){
		if(weapon == 1){
			rifleUpgraded = 1;
		}else if(weapon == 2){
			shotGunUpgraded = 1;
		}else if(weapon == 3){
			rocketUpgraded = 1;
		}else if(weapon == 4){
			uziUpgraded = 1;
		}else if(weapon == 5){
			grenadeUpgraded = 1;
		}else if(weapon == 6){
			gatlingGunUpgraded = 1;
		}else if(weapon == 7){
			crossBowUpgraded = 1;
		}else if(weapon == 8){
			bolterUpgraded = 1;
		}else if(weapon == 9){
			dildoUpgraded = 1;
		}else if(weapon == 10){
			laserUpgraded = 1;
		}
	}
	
	public int getUpgraded(int weapon){
		if (weapon == 1) {
			return rifleUpgraded;
		} else if (weapon == 2) {
			return shotGunUpgraded;
		}else if (weapon == 3) {
			return rocketUpgraded;
		}else if (weapon == 4) {
			return uziUpgraded;
		}else if (weapon == 5) {
			return grenadeUpgraded;
		}else if (weapon == 6) {
			return gatlingGunUpgraded;
		}else if (weapon == 7) {
			return crossBowUpgraded;
		}else if (weapon == 8) {
			return bolterUpgraded;
		}else if (weapon == 9) {
			return dildoUpgraded;
		}else if (weapon == 10) {
			return laserUpgraded;
		}else {
			return 0;
		}
	}

	public void reset(){
		 pistolAmmo = 0;
		 rifleAmmo = 0;
		 shotGunAmmo = 0;
		 rocketAmmo = 0;
		 uziAmmo = 0;
		 grenadeAmmo = 0;
		 gatlingGunAmmo = 0;
		 crossBowAmmo = 0;
		 bolterAmmo = 0;	
		 dildoAmmo = 0;
		
		 currentlyEquipedWeapon = 1; //0-Pistol, 1-Rifle, 2-Shotgun, 3-RocketLauncher
		 inventorySlotOne = 1;//1-Rifle, 2-Shotgun, 3-RocketLauncher
		
		 pistolFireRate = 0f;
		 rifleFireRate = 0f;
		 shotGunFireRate = 0f;
		 rocketFireRate = 0f;
		 uziFireRate = 0f;
		 grenadeFireRate = 0f;
		 gatlingGunFireRate = 0f;
		 crossBowFireRate = 0f;
		 bolterFireRate = 0f;
		 dildoFireRate = 0f;
		
		//float laserFireRate = 0f;
		 laserRate = 3f;
		 laserCharge = 0f;
		
		/// <Upgrades Start>
		 shotGunUpgraded = 0;
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

		pistolAmmo = 1;
		rifleAmmo = 40;		
		shotGunAmmo = 20;		
		rocketAmmo = 8;
		uziAmmo = 30; 
		grenadeAmmo = 15;
		gatlingGunAmmo = 25;
		crossBowAmmo = 15;
		bolterAmmo = 25;
		dildoAmmo = 35;
		
		laserCharge = laserRate;
	}



}
