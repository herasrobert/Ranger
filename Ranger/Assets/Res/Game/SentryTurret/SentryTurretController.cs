using UnityEngine;
using System.Collections;

public class SentryTurretController : MonoBehaviour {
	int sentryHealth = 5;
	int sentryAmmo = 100;
	int rotation = 0;

	float sentryFireRate = .3f;
	float brokenTimer = 9f;

	bool sentryUp = true;
	bool broken = false;
	
	SpriteRenderer spriteRenderer;
	CircleCollider2D circleCollider;

	public GameObject sentryTurretBullet;
	GameObject bulletSpawnPoint;

	public Sprite brokenOne;
	public Sprite brokenTwo;
	public Sprite brokenThree;
	PoolingSystem pS;
	
	GameObject soundController;
	SoundController soundControllerScript;

	void Start () { 
		pS = PoolingSystem.Instance;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		circleCollider = this.GetComponent<CircleCollider2D>();
		bulletSpawnPoint = transform.Find("SentryTurretBulletSpawnPoint").gameObject;
		
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();
	}

	void Update () {
		sentryFireRate -= Time.deltaTime;
		if (!broken) {
			if (sentryUp) {
				rotation += 1;
				transform.Rotate (0, 0, +1);
			} else {
				rotation -= 1;
				transform.Rotate (0, 0, -1);
			}
		
			if (rotation == 45) { //Switched from 90 to 80 Degrees
				sentryUp = false;
			} else if (rotation == -45) {
				sentryUp = true;
			}		

			if (sentryFireRate < 0) {
				if (sentryAmmo > 0) {
					//Instantiate (sentryTurretBullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);//Spawn Bullet at location and with facing/rotation
					pS.InstantiateAPS("SentryTurretBullet", bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);

					sentryAmmo--;
					sentryFireRate = .03f;
					soundControllerScript.playSFX("Pistol");
				}
			}
		}

		if(sentryHealth <= 0 || sentryAmmo == 0){
			broken = true;//Destroy this Object like Barricade
			circleCollider.isTrigger = true;
		}

		if(broken){
			brokenTimer-=Time.deltaTime;
			
			if(brokenTimer <= 9 && brokenTimer > 7){
				spriteRenderer.sprite = brokenOne;
			}else if(brokenTimer < 7 && brokenTimer > 4){
				spriteRenderer.sprite = brokenTwo;
			}else if(brokenTimer < 3 && brokenTimer > 1){
				spriteRenderer.sprite = brokenThree;
			}else if(brokenTimer < 0){
				Destroy (this.gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Zombie"){//If Collision with RifleBullet
			sentryHealth = 0;
		}else if(coll.gameObject.tag == "RifleBullet"){//If Collision with RifleBullet
			sentryHealth--;
		}else if(coll.gameObject.tag == "ShotGunBullet"){//If Collision with 
			sentryHealth-=3;
		}else if(coll.gameObject.tag == "RocketBullet"){//If Collision with 
			sentryHealth = 0;
		}else if(coll.gameObject.tag == "UziBullet"){//If Collision with 
			sentryHealth-=1;
		}else if(coll.gameObject.tag == "GatlingGunBullet"){//If Collision with 
			sentryHealth-=2;
		}else if(coll.gameObject.tag == "CrossBowBullet"){//If Collision with 
			sentryHealth-=1;
		}else if(coll.gameObject.tag == "BolterBullet"){//If Collision with 
			sentryHealth = 0;
		}else if(coll.gameObject.tag == "DildoBullet"){//If Collision with 
			sentryHealth--;
		}else if(coll.gameObject.tag == "SentryTurretBullet"){//If Collision with 
			sentryHealth--;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {		
		if (coll.gameObject.tag == "RocketExplosion") {
			sentryHealth = 0;//Take 100 Health of Damage
		}else if (coll.gameObject.tag == "LaserBullet") {
			sentryHealth = 0;//Take 100 Health of Damage
		}else if (coll.gameObject.tag == "RotatingLaserBeam") {
			sentryHealth = 0;//Take 100 Health of Damage
		}
	}
}
