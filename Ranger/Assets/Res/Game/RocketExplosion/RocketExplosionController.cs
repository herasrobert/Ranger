using UnityEngine;
using System.Collections;

public class RocketExplosionController : MonoBehaviour {

	float destroyTimer = 0f;
		
	GameObject soundController;
	SoundController soundControllerScript;
	void Start(){
	}

	void OnEnable(){
		if(soundController == null){
			soundController = GameObject.Find ("SoundController");
			soundControllerScript = soundController.GetComponent<SoundController>();
		}
		destroyTimer = .1f;
	}


	void Update () {
		destroyTimer -= Time.deltaTime;
		if(destroyTimer < 0){
			soundControllerScript.playSFX ("Explosion");
			//Destroy(this.gameObject);
			gameObject.DestroyAPS();
		}
	}
}
