using UnityEngine;
using System.Collections;

public class BioBombController : MonoBehaviour {
	float detonationTimer = 15f;
	bool runOnce = true;

	CircleCollider2D circleCollider;
	SpriteRenderer spriteRenderer;

	float flashTimer = .2f;
	public Texture whiteFlash;
	public Sprite bioBombOne;
	public Sprite bioBombTwo;
	public Sprite bioBombThree;
	
	GameObject soundController;
	SoundController soundControllerScript;
	void Start () {
		circleCollider = this.GetComponent<CircleCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
	}

	void Update () {
		detonationTimer -= Time.deltaTime;
		
		transform.Rotate(0,0,(detonationTimer-15)*2); //Rotate Bomb

		if(detonationTimer > 9 && detonationTimer < 16){			
			spriteRenderer.sprite = bioBombOne;
		}else if(detonationTimer > 4 && detonationTimer < 10){
			spriteRenderer.sprite = bioBombTwo;
		}else if(detonationTimer > -1 && detonationTimer < 5){
			spriteRenderer.sprite = bioBombThree;
		}


		if (detonationTimer < 0) {
			circleCollider.radius = 10; // Increase Collision Radius
			circleCollider.isTrigger = true; // Trigger for Multip "Collsion"
			flashTimer -=Time.deltaTime;
			if(runOnce){
				runOnce = false;
				soundControllerScript.playSFX("Explosion");
			}
		} 			

		if(flashTimer < 0){				
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Zombie") {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "RotatingLaserBeam") {
			Destroy (this.gameObject);
		}
	}

	void OnGUI(){
		if (detonationTimer < 0) {
			GUI.DrawTexture (new Rect (0, 0, 800, 600), whiteFlash, ScaleMode.StretchToFill);
		}
		//GUI.Label(new Rect(this.transform.position.x, this.transform.position.y, 300, 20), "Bio-Bomb Detonation: T-"+(int)detonationTimer);	
	}
}
