using UnityEngine;
using System.Collections;

public class BarricadeController : MonoBehaviour {

	int barricadeCurrentHealth = 0;
	int barricadeMaxHealth = 5;

	bool broken = false;

	float barricadeHPReductionTimer = 1f;
	float brokenTimer = 9f;

	SpriteRenderer spriteRenderer;
	BoxCollider2D circleCollider;

	public Sprite brokenOne;
	public Sprite brokenTwo;
	public Sprite brokenThree;

	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		circleCollider = GetComponent<BoxCollider2D>();

		barricadeCurrentHealth = barricadeMaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(barricadeCurrentHealth <= 0){
			broken = true;
			circleCollider.enabled = false;
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

	void OnCollisionStay2D(Collision2D coll) {
		barricadeHPReductionTimer -= Time.deltaTime;
		if (coll.gameObject.tag == "Zombie"){
			if(barricadeCurrentHealth > 0){
				if(barricadeHPReductionTimer <= 0){
					barricadeCurrentHealth--;
					barricadeHPReductionTimer = 1f;
				}
			}
		}
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "RifleBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth--;
		}else if(coll.gameObject.tag == "ShotGunBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth-=3;
		}else if(coll.gameObject.tag == "RocketBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth = 0;
		}else if(coll.gameObject.tag == "UziBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth-=1;
		}else if(coll.gameObject.tag == "GatlingGunBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth-=2;
		}else if(coll.gameObject.tag == "CrossBowBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth-=1;
		}else if(coll.gameObject.tag == "BolterBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth = 0;
		}else if(coll.gameObject.tag == "DildoBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth--;
		}else if(coll.gameObject.tag == "SentryTurretBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth-=2;
		}else if(coll.gameObject.tag == "LaserBullet"){//If Collision with RifleBullet
			barricadeCurrentHealth=0;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "RocketExplosion") {
			barricadeCurrentHealth = 0;//Take 100 Health of Damage
		}else if (coll.gameObject.tag == "LaserBullet") {
			barricadeCurrentHealth = 0;//Take 100 Health of Damage
		}else if (coll.gameObject.tag == "RotatingLaserBeam") {
			barricadeCurrentHealth = 0;//Take 100 Health of Damage
		}else if (coll.gameObject.tag == "LaserLine") {
			barricadeCurrentHealth = 0;//Take 100 Health of Damage
		}
	}

}
