using UnityEngine;
using System.Collections;

public class SecondaryController : MonoBehaviour {

	int barricadeStock = 0;
	int landMineStock = 0;
	int bioBombStock = 0;
	int sentryTurretStock = 0;
	int rotatingLaserBeamStock = 0;
	int laserLineStock = 0;
		
	int currentlyEquipedSecondary = 10;  //10 so there is no default Item assigned

	public GameObject barricade;
	public GameObject landMine;
	public GameObject bioBomb;
	public GameObject sentryTurret;
	public GameObject rotatingLaserBeam;
	public GameObject laserLine;

	GameObject soundController;
	SoundController soundControllerScript;

	void Start () {
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();

		barricadeStock = 1; //Starting Amount of Barricades <- Delete Later
		landMineStock = 1; 
		bioBombStock = 1;
		sentryTurretStock = 1;
		rotatingLaserBeamStock = 1;
		laserLineStock = 1;
	}

	void Update(){}

	public void deploySecondary(GameObject bulletSpawnPoint){
		if(currentlyEquipedSecondary == 0){//If Barricade
			if(barricadeStock > 0){//Barricade In Stock
				Instantiate(barricade, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);//Spawn Barricade
				barricadeStock--;
				soundControllerScript.playSFX("Deploy");
			}
		}else if(currentlyEquipedSecondary == 1){//
			if(landMineStock > 0){//If LandMine In Stock
				Instantiate(landMine, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);//Spawn Bullet at location and with facing/rotation
				landMineStock--;
				soundControllerScript.playSFX("Deploy");
			}
		}else if(currentlyEquipedSecondary == 2){//
			if(bioBombStock > 0){//If Bio-Bomb In Stock
				Instantiate(bioBomb, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);//Spawn Bullet at location and with facing/rotation
				bioBombStock--;
				soundControllerScript.playSFX("Deploy");
			}
		}else if(currentlyEquipedSecondary == 3){//
			if(sentryTurretStock > 0){//If SentryTurret In Stock
				Instantiate(sentryTurret, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);//Spawn Bullet at location and with facing/rotation
				sentryTurretStock--;
				soundControllerScript.playSFX("Deploy");
			}
		}else if(currentlyEquipedSecondary == 4){//
			if(rotatingLaserBeamStock > 0){//If RotatingLaserBeam In Stock
				Instantiate(rotatingLaserBeam, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);//Spawn Bullet at location and with facing/rotation
				rotatingLaserBeamStock--;
				soundControllerScript.playSFX("Deploy");
			}
		}else if(currentlyEquipedSecondary == 5){//
			if(laserLineStock > 0){//If RotatingLaserBeam In Stock
				Instantiate(laserLine, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);//Spawn Bullet at location and with facing/rotation
				laserLineStock--;
				soundControllerScript.playSFX("Deploy");
			}
		}
	}

	public void equipSecondary(int selectedSecondary){
		currentlyEquipedSecondary = selectedSecondary;
	}

	public int getCurrentlyEquipedSecondary(){
		return currentlyEquipedSecondary;
	}

	public void addStock(int secondary,int amount){
		if(secondary == 0){ //Barrcade
			barricadeStock += amount;
		}else if(secondary == 1){// Land Mine
			landMineStock += amount;
		}else if(secondary == 2){ // Bio-Bomb
			bioBombStock += amount;
		}else if(secondary == 3){ //Sentry Turret
			sentryTurretStock += amount;
		}else if(secondary == 4){ // Rotating Laser Beam
			rotatingLaserBeamStock += amount;
		}else if(secondary == 5){// Laser Line
			laserLineStock += amount;
		}
	}

	public int getStock (int secondary){
		if (secondary == 0) {
			return barricadeStock;
		} else if (secondary == 1) {
			return landMineStock;
		} else if (secondary == 2) {
			return bioBombStock;			
		} else if (secondary == 3) {
			return sentryTurretStock;			
		} else if (secondary == 4) {			
			return rotatingLaserBeamStock;
		} else if (secondary == 5) {			
			return laserLineStock;
		} else {
			return 0;
		}
	}

	public void reset(){
		barricadeStock = 0;
		landMineStock = 0;
		bioBombStock = 0;
		sentryTurretStock = 0;
		rotatingLaserBeamStock = 0;
		laserLineStock = 0;
		
		currentlyEquipedSecondary = 10;  //10 so there is no default Item assigned

		barricadeStock = 1; //Starting Amount of Barricades <- Delete Later
		landMineStock = 1; 
		bioBombStock = 1;
		sentryTurretStock = 1;
		rotatingLaserBeamStock = 1;
		laserLineStock = 1;
	}

}
