using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {
	int currentHealth = 1;
	int maxHealth = 100;
	float movementSpeed = 1f;

	bool dead = true;

	GameObject player;
	PlayerController playerControllerScript;
	PrimaryController primaryController;
	SpriteRenderer spriterenderer;

	public GameObject HealthPack;
	public GameObject ScrapsPack;
	public GameObject AmmoPack;
	public GameObject BloodPool;
	public GameObject ZombieSpawner;
	ZombieSpawner zombieSpawnerScript;

	public Sprite zombieZero;
	public Sprite zombieOne;
	public Sprite zombieTwo;
	public Sprite zombieThree;
	public Sprite zombieFour;
	public Sprite zombieFive;
	public Sprite zombieSix;
	public Sprite zombieSeven;

	PoolingSystem pS;
	/*void Start () {
		pS = PoolingSystem.Instance;
		body = this.GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player"); //Reference to Player Game Object
		primaryController = player.GetComponent<PrimaryController>(); // Reference to PlayerController Script	
		ZombieSpawner = GameObject.Find("ZombieSpawner"); //Reference to Player Game Object
		playerControllerScript = player.GetComponent<PlayerController>(); // Reference to PlayerController Script	
		//spawnerScript = ZombieSpawner.GetComponent<Spawner> ();
		spawnAI = this.GetComponent<SpawnAI>(); 
		circleCollider = GetComponent<CircleCollider2D>();
		spriterenderer = this.GetComponent<SpriteRenderer>();

		
		int zombieType = (int)Random.Range(0f,3f);

		if (zombieType == 0) {//Regular Zombie
			movementSpeed = 1f + (spawnerScript.getCurrentRound () / 10f);
			if(movementSpeed > 1.5f){
				movementSpeed = 1.5f;
			}
			maxHealth = 100 + (spawnerScript.getCurrentRound () * 10);
			this.gameObject.name = "Zombie";
			//Set Animator To Regular
		} else if (zombieType == 1) {
			movementSpeed = 1f + (spawnerScript.getCurrentRound () / 2f);
			if(movementSpeed > 1.7f){
				movementSpeed = 1.7f;
			}
			maxHealth = 75;
			spriterenderer.sprite = fastZombie;
			this.gameObject.name = "FastZombie";
			//Set Animator to Fast
		} else if (zombieType == 2){
			movementSpeed = 1f + (spawnerScript.getCurrentRound () / 10f)/2f;
			if(movementSpeed > 1.4f){
				movementSpeed = 1.4f;
			}
			maxHealth = 100 + (spawnerScript.getCurrentRound () * 10)*2;
			spriterenderer.sprite = fatZombie;
			this.gameObject.name = "FatZombie";
			//animator.SetInteger("Type",2);//Set Animator to Fat
		}
		currentHealth = maxHealth;
	}*/

	void OnEnable(){
		pS = PoolingSystem.Instance;
		player = GameObject.Find("Player"); //Reference to Player Game Object
		primaryController = player.GetComponent<PrimaryController>(); // Reference to PlayerController Script	
		ZombieSpawner = GameObject.Find("ZombieSpawner"); //Reference to Player Game Object
		zombieSpawnerScript = ZombieSpawner.GetComponent<ZombieSpawner>();
		playerControllerScript = player.GetComponent<PlayerController>(); // Reference to PlayerController Script
		spriterenderer = this.GetComponent<SpriteRenderer>();	

		dead = true;

		int zombieType = (int)Random.Range(0f,3f);
		
		if (zombieType == 0) {//Regular Zombie
			movementSpeed = 1f + (zombieSpawnerScript.getCurrentRound() / 10f);
			if(movementSpeed > 1.5f){
				movementSpeed = 1.5f;
			}
			maxHealth = 100 + (zombieSpawnerScript.getCurrentRound() * 12);
			this.gameObject.name = "Zombie";
			//Set Animator To Regular
		} else if (zombieType == 1) {
			movementSpeed = 1f + (zombieSpawnerScript.getCurrentRound() / 2f);
			if(movementSpeed > 1.7f){
				movementSpeed = 1.7f;
			}
			maxHealth = 75 + (zombieSpawnerScript.getCurrentRound() * 10);
			//spriterenderer.sprite = fastZombie;
			this.gameObject.name = "FastZombie";
			//Set Animator to Fast
		} else if (zombieType == 2){
			movementSpeed = 1f + (zombieSpawnerScript.getCurrentRound() / 10f)/2f;
			if(movementSpeed > 1.4f){
				movementSpeed = 1.4f;
			}
			maxHealth = 100 + (zombieSpawnerScript.getCurrentRound() * 12)*2;
			//spriterenderer.sprite = fatZombie;
			this.gameObject.name = "FatZombie";
			//animator.SetInteger("Type",2);//Set Animator to Fat
		}
		
		int zombieSprite = (int)Random.Range(0f,8f); // 0-7 - inclusive

		if (zombieSprite == 1) {
			spriterenderer.sprite = zombieOne;
		} else if (zombieSprite == 2) {
			spriterenderer.sprite = zombieTwo;
		} else if (zombieSprite == 3) {
			spriterenderer.sprite = zombieThree;
		} else if (zombieSprite == 4) {
			spriterenderer.sprite = zombieFour;
		} else if (zombieSprite == 5) {
			spriterenderer.sprite = zombieFive;
		} else if (zombieSprite == 6) {
			spriterenderer.sprite = zombieSix;
		} else if (zombieSprite == 7) {
			spriterenderer.sprite = zombieSeven;
		} else {
			spriterenderer.sprite = zombieZero;
		}

		currentHealth = maxHealth;
	}

	void Update () {	
		if(dead){
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);//Move Towards Player

			Vector3 playerPos = new Vector3(player.transform.position.x,player.transform.position.y,0);//Get Player Position Vector
			transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPos - transform.position);//Rotate to Face Player
		}
		if(currentHealth <= 0 && dead){//Basically on death, run this once
			OnDeath();
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "PistolBullet") {
			int criticalHitChance = (int)Random.Range(0f,10f);//Random Number to Determine if and what you get
			if(criticalHitChance <= 1){//Simulate HeadShot
				currentHealth -= 100;//Take 25 Health of Damage(Critical Hit)
			}else {
				currentHealth -= 25 + playerControllerScript.getCurrentRound();//Take 25 Health of Damage
			}
		}else if (coll.gameObject.tag == "RifleBullet") {
			int criticalHitChance = (int)Random.Range(0f,10f);//Random Number to Determine if and what you get
			if(criticalHitChance <= 1){//Simulate HeadShot
				currentHealth -= 100;//Take 100 Health of Damage(Critical Hit)
			}else {
				if(primaryController.getUpgraded(1) == 1){ // If Rifle is Upgraded
					currentHealth -= 75;
				}else {
					currentHealth -= 40;//Take 40 Health of Damage
				}
			}
		}else if (coll.gameObject.tag == "ShotGunBullet") {
				//currentHealth -= 100;//Take 100 Health of Damage
			if(primaryController.getUpgraded(2) == 1){ // If Shotgun is Upgraded				
				currentHealth -= 65;//Changed for Spray instead of Buck Shot
			}else {
				currentHealth -= 40;//Changed for Spray instead of Buck Shot
			}
		}else if (coll.gameObject.tag == "UziBullet") {
			if(primaryController.getUpgraded(4) == 1){ // If Shotgun is Upgraded
				currentHealth -= 45;//Take 25 Health of Damage
			}else {
				currentHealth -= 35;//Take 25 Health of Damage
			}
		}else if (coll.gameObject.tag == "GatlingGunBullet") {
			if(primaryController.getUpgraded(6) == 1){ // If Shotgun is Upgraded
				currentHealth -= 80;//Take 25 Health of Damage
			}else {
				currentHealth -= 50;//Take 100 Health of Damage
			}
		}else if (coll.gameObject.tag == "DildoBullet") {
			currentHealth -= 100;//Take 100 Health of Damage
		}else if (coll.gameObject.tag == "SentryTurretBullet") {
			currentHealth -= 100;//Take 100 Health of Damage
		}

		if (coll.gameObject.tag == "Player"){
			Vector3 playerPos = new Vector3(player.transform.position.x,player.transform.position.y,0);//Get Zombie Facing Position Vector
			player.GetComponent<Rigidbody2D>().AddForce(playerPos*15); //Knock Back Player

			int criticalHitChance = (int)Random.Range(0f,10f);//Random Number to Determine if and what you get
			if(criticalHitChance <= 1){//Simulate HeadShot
				playerControllerScript.subtractHealth(25);//Take 25 Health of Damage (Critical Hit)
			}else {
				playerControllerScript.subtractHealth(10);//Take 10 Health of Damage
			}
		}

	}
	void OnTriggerEnter2D(Collider2D coll) {		
		if (coll.gameObject.tag == "RocketExplosion") {
			if(primaryController.getUpgraded(3) == 1 || primaryController.getUpgraded(5) == 1){ // If Shotgun is Upgraded
				currentHealth -= 185;//Take 25 Health of Damage
			}else{
				currentHealth -= 125;//Take 100 Health of Damage
			}
		}else if (coll.gameObject.tag == "LaserBullet") {
			if(primaryController.getUpgraded(10) == 1){ // If Shotgun is Upgraded
				currentHealth -= 300;//Take 25 Health of Damage
			}else {
				currentHealth -= 200;//Take 100 Health of Damage
			}
		}else if (coll.gameObject.tag == "BioBomb") {
			currentHealth -= 150;//Take 100 Health of Damage
		}else if (coll.gameObject.tag == "RotatingLaserBeam") {
			currentHealth -= 75;//Take 100 Health of Damage
		}else if (coll.gameObject.tag == "LaserLine") {
			currentHealth -= 75;//Take 100 Health of Damage
		}

		if (coll.gameObject.tag == "Survivor") {
			coll.gameObject.DestroyAPS();
		}

		if (coll.gameObject.tag == "CrossBowBullet") {
			if(primaryController.getUpgraded(7) == 1){ // If Shotgun is Upgraded
				currentHealth -= 120;//Take 25 Health of Damage
			}else {
				currentHealth -= 80;//Take 100 Health of Damage
			}
		}else if (coll.gameObject.tag == "BolterBullet") {
			if(primaryController.getUpgraded(8) == 1){
				currentHealth -= 150;
			}else {
				currentHealth -= 100;//Take 100 Health of Damage
			}
		}

	}

	void OnTriggerStay2D(Collider2D coll) {
		if(coll.gameObject.tag == "RadiationPool"){//While continously on radiation pool
			currentHealth = maxHealth;
		}
	}

	void OnDeath(){
		playerControllerScript.addScore(10+zombieSpawnerScript.getCurrentRound()*2); //Increase Current Score
		int randomObject = (int)Random.Range(0f,8f);//Random Number to Determine if and what you get

		if(randomObject==0){
			pS.InstantiateAPS("HealthPack", transform.position, transform.rotation);
		}else if(randomObject==1){
			pS.InstantiateAPS("ScrapsPack", transform.position, transform.rotation);
		}else if(randomObject==2){
			pS.InstantiateAPS("AmmoPack", transform.position, transform.rotation);
		}
		//circleCollider.enabled = false;
		//body.isKinematic = true;//Set to Kinematic so no collisions of any kind occur
		//dead = false;
		int bloodPoolChance = (int)Random.Range(0f,10f);//Random Number to Determine blood pool gets spawned
		if(bloodPoolChance >= 0 && bloodPoolChance <= 2){
			pS.InstantiateAPS("BloodPool", transform.position, transform.rotation);
		}
		//playerControllerScript.addScraps(10+zombieSpawnerScript.getCurrentRound()*2);
		playerControllerScript.addScraps(10+zombieSpawnerScript.getCurrentRound());
		playerControllerScript.killedZombie(); //Add one to total zombies killed in whole play time
		zombieSpawnerScript.deadZombie(); // Add one to total zombies killed this round
		gameObject.DestroyAPS();//spawnAI.Remove();//Destroy (this.gameObject);//Replace with DeSpawn
	}
	public void setCurrentHealthToZero(){
		currentHealth = 0;
	}




}