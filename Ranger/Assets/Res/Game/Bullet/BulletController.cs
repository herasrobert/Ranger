using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	//int shotGunPierceCounter = 0;
	int crossBowPierceCounter = 0;
	int crossBowCounterMax = 2;
	int bolterPierceCounter = 0;
	int bolterCounterMax = 9;

	float grenadeTimer = .5f;

	Rigidbody2D body;
	public BoxCollider2D boxCollider;
	float distanceFromPlayer = 0f;

	public GameObject RocketExplosion;
	GameObject player;
	//PlayerController playerControllerScript;
	PrimaryController primaryController;
	Vector3 mousePos;
	PoolingSystem pS;
	SpriteRenderer spriteRenderer;
	GameObject optionsOverlay;
	OptionsOverlayController optionsOverlayController;

	public Sprite dildoImageBlocked;
	public Sprite dildoImage;

	void Start(){
		pS = PoolingSystem.Instance;
		player = GameObject.Find ("Player");
		primaryController = player.GetComponent<PrimaryController>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();		
		optionsOverlay = GameObject.Find ("OptionsOverlay");
		optionsOverlayController = optionsOverlay.GetComponent<OptionsOverlayController>();	

	}

	void OnEnable(){
		player = GameObject.Find ("Player");
		boxCollider = this.GetComponent<BoxCollider2D>();
		body = this.gameObject.GetComponent<Rigidbody2D>();		
		spriteRenderer = this.GetComponent<SpriteRenderer>();

		if (this.tag == "GrenadeBullet" || this.tag == "DildoBullet") {			
			boxCollider.isTrigger = false;//Make collider Trigger
			body.isKinematic = false;//Makes movement possible
		}
		if(this.tag == "DildoBullet"){
			optionsOverlay = GameObject.Find ("OptionsOverlay");
			optionsOverlayController = optionsOverlay.GetComponent<OptionsOverlayController>();	
			if(optionsOverlayController.getAdultContent() == 0){ //If Adult Content is Off
				this.spriteRenderer.sprite = dildoImageBlocked;
			}else {
				this.spriteRenderer.sprite = dildoImage;
			}
		}
		grenadeTimer = .5f;
		if(primaryController != null){
			if (primaryController.getUpgraded(6) == 1) {
				crossBowCounterMax = 5;
			} else {
				crossBowCounterMax = 2;
			}
			if (primaryController.getUpgraded(8) == 1) {
				bolterCounterMax = 9;
			} else {
				bolterCounterMax = 4;
			}
		}
		crossBowPierceCounter = 0;
		bolterPierceCounter = 0;
	}

	void Update () {
		distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
		if(distanceFromPlayer > 110){//Distance from Corner to Corner should be like 68.
			gameObject.DestroyAPS();
		}

		if(this.tag == "PistolBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}else if(this.tag == "RifleBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*250);
		}else if(this.tag == "ShotGunBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*250);
		}else if(this.tag == "RocketBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}else if(this.tag == "UziBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}else if(this.tag == "GrenadeBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
			grenadeTimer -= Time.deltaTime;
			if(grenadeTimer < 0){
				pS.InstantiateAPS("rocketExplosion", transform.position, transform.rotation);
				grenadeTimer = .5f;
				gameObject.DestroyAPS();
			}
		}else if(this.tag == "GatlingGunBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}else if(this.tag == "CrossBowBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}else if(this.tag == "BolterBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}else if(this.tag == "DildoBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}else if(this.tag == "LaserBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}else if(this.tag == "SentryTurretBullet"){
			body.AddForce (new Vector2 (transform.up.x, transform.up.y)*200);
		}

	}
	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "GameAreaBorder"){//Collision with Wall			
			if(this.tag == "DildoBullet"){
				setStationary();
			}else if(this.tag == "GrenadeBullet"){
				setStationary();
			}else {
				gameObject.DestroyAPS();
			}
		}else if(coll.gameObject.tag == "PistolBullet"){//If Colliding with PistolBullet			
			//Do Nothing
		}else if (this.tag == "ShotGunBullet" && coll.gameObject.tag == "ShotGunBullet"){//Incase shotgun bullet collides with shotgun bullet during spray

		}else if (this.tag == "RocketBullet" && coll.gameObject.tag == "RocketBullet"){//If Rockets Collide with Each Other

		}else if (this.tag == "RocketBullet") {
			pS.InstantiateAPS("rocketExplosion", transform.position, transform.rotation);
			gameObject.DestroyAPS();
		}else if (this.tag == "UziBullet" && coll.gameObject.tag == "UziBullet") {//If uziBullet Collides with uziBullet, do nothing. This can occur because of firing speed. Instantiating bullets ontop of bullets
		
		}else if (this.tag == "GatlingGunBullet" && coll.gameObject.tag == "GatlingGunBullet") {//If GatlingGunBullet Collides with GatlingGunBullet, do nothing. This can occur because of firing speed. Instantiating bullets ontop of bullets
			
		}else if ((this.tag == "SentryTurretBullet" && coll.gameObject.tag == "SentryTurretBullet")) {//If GatlingGunBullet Collides with GatlingGunBullet, do nothing. This can occur because of firing speed. Instantiating bullets ontop of bullets
			
		}else if ((this.tag == "LaserBullet" && coll.gameObject.tag == "Zombie")) {//If laser bullet colliding with zombie, do nothing
		
		}else if(coll.gameObject.tag == "Player"){
			//Do Nothing
		}else if(this.tag == "DildoBullet"){
			if(coll.gameObject.tag == "PistolBullet"){
				//Do Nothing
			}else if(coll.gameObject.tag == "DildoBullet"){
				//Do Nothing
			}else {
				setStationary();
			}
		}else if(this.tag == "GrenadeBullet"){
			if(coll.gameObject.tag == "PistolBullet"){
				//Do Nothing
			}else if(coll.gameObject.tag == "GrenadeBullet"){
				//Do Nothing
			}else {
				setStationary();
			}
		}
		else {
			gameObject.DestroyAPS();
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "ScrapsPack"){
			//Do Nothing
		}else if(coll.gameObject.tag == "AmmoPack"){
			//Do Nothing
		}else if(coll.gameObject.tag == "HealthPack"){
			//Do Nothing
		}else if(coll.gameObject.tag == "RadiationPool"){
			//Do Nothing
		}else if(coll.gameObject.tag == "PistolBullet"){
			//Do Nothing
		}else if (coll.gameObject.tag == "Survivor") {
			//Do Nothing
		}else if (coll.gameObject.tag == "BioBomb") {
			//Do Nothing
		}else if (coll.gameObject.tag == "LandMine") {
			//Do Nothing
		}else if (coll.gameObject.tag == "LaserLine") {
			//Do Nothing
		}else if (coll.gameObject.tag == "RotatingLaserBeam") {
			//Do Nothing
		}else if (this.tag == "RifleBullet") {
			//Do Nothing
		}else if (this.tag == "ShotGunBullet") {
			//Do Nothing
		}else if (this.tag == "RocketBullet") {
			//Do Nothing
		}else if (this.tag == "UziBullet") {
			//Do Nothing
		}else if (this.tag == "GrenadeBullet") {
			//Do Nothing
		}else if (this.tag == "GatlingGunBullet") {
			//Do Nothing
		}else if (this.tag == "DildoBullet") {
			//Do Nothing
		}else if (this.tag == "CrossBowBullet" && coll.gameObject.tag == "CrossBowBullet") {
			//Do Nothing
		}else if (this.tag == "CrossBowBullet" && coll.gameObject.tag == "Zombie") {
			if(crossBowPierceCounter < crossBowCounterMax){
				crossBowPierceCounter++;
			}else {
				gameObject.DestroyAPS();
			}
		}else if (this.tag == "BolterBullet" && coll.gameObject.tag == "BolterBullet") {
			//Do Nothing
		}else if (this.tag == "BolterBullet" && coll.gameObject.tag == "Zombie") {
			if(bolterPierceCounter < bolterCounterMax){
				bolterPierceCounter++;
			}else {
				gameObject.DestroyAPS();
			}
		}else if (this.tag == "LaserBullet" && coll.gameObject.tag == "LaserBullet") {
			//Do Nothing
		}else if (this.tag == "LaserBullet" && coll.gameObject.tag == "Zombie") {
			//Do Nothing - Pierce All
		}else {
			gameObject.DestroyAPS();		
		}
	}


	void setStationary(){
		body.isKinematic = true;//Stop movement
		boxCollider.isTrigger = true;//Make collider Trigger
	}


}
