using UnityEngine;
using System.Collections;

public class FloodRadiationPoolController : MonoBehaviour {

	float growTimer = 0f;
	float scaleFactor = 0f;

	bool grow = true;
	bool shrink = false;
	
	GameObject player;
	PlayerController playerControllerScript;

	void Start () {
		transform.localScale = new Vector3(1, 1, 1);//Grow		
	}

	void OnEnable(){
		player = GameObject.Find("Player"); //Reference to Player Game Object
		playerControllerScript = player.GetComponent<PlayerController>(); // Reference to PlayerController Script

		growTimer = 0f;
		scaleFactor = 0f;
		
		grow = true;
		shrink = false;

		transform.localScale = new Vector3(1, 1, 1);//Grow
	}

	void Update () {
		scaleFactor = growTimer*1f;

		if (grow) {
			growTimer += Time.deltaTime;
			transform.localScale = new Vector3 (scaleFactor, scaleFactor, 1);//Grow
		}else if (shrink){
			growTimer -= Time.deltaTime;
			transform.localScale = new Vector3 (scaleFactor, scaleFactor, 1);//Grow
		}
		if (growTimer > 12){
			grow = false;
			shrink = true;
		}
		if(growTimer < 0 || playerControllerScript.isAlive() == false){
			//Destroy(this.gameObject);
			gameObject.DestroyAPS();
		}

	}
}