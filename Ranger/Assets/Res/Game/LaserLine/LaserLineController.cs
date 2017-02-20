using UnityEngine;
using System.Collections;

public class LaserLineController : MonoBehaviour {

	float laserLineLife = 7f;

	GameObject soundController;
	SoundController soundControllerScript;

	void Start () {
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
		soundControllerScript.playSFX ("SecondaryLaserSounds");
	}

	void Update () {
		laserLineLife -= Time.deltaTime;

		if(laserLineLife < 0){
			soundControllerScript.stopSFX("SecondaryLaserSounds");
			Destroy(this.gameObject);
		}	
	}
}
