using UnityEngine;
using System.Collections;

public class RotatingLaserBeamController : MonoBehaviour {

	int rotationCounter = -1;

	float lifeTimer = 10f;
	float speedUpCounter = 1f;
	
	GameObject soundController;
	SoundController soundControllerScript;

	void Start () {		
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
		soundControllerScript.playSFX ("SecondaryLaserSounds");
	}

	void Update () {
		lifeTimer -= Time.deltaTime;
		speedUpCounter -= Time.deltaTime;

		if(speedUpCounter < 0){
			rotationCounter-=2;
			speedUpCounter=1f;
		}

		transform.Rotate(0,0,rotationCounter);

		if(lifeTimer < 0){
			soundControllerScript.stopSFX("SecondaryLaserSounds");
			Destroy(this.gameObject);
		}
	}
}