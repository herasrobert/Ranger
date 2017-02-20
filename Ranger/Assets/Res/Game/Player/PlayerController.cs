using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	int currentHealth = 0;
	int maxHealth = 100;
	int currentScraps = 0;
	int currentScore = 0;
	int numberOfZombiesKilled = 0;
	int jumpScareToggle = 1;
	int currentlyEquiped = 1;

	string playerName;

	float movementSpeed = 2f;

	float currentTimePassed = 0;
	float timeUntilRoundStarts = 46f;

	float biteTimer = 8f;
	float biteDamage = 1f;
	float bittenTextureEffect = 8f;
	float radiationPoolDamageTimer = 1f;

	bool fire = false;
	bool bitten = false;
	bool startRoundTimer = false;
	bool setSpawnOnOnce = false;
	bool alive = true;
	bool roundEndedCheck = true;
	bool paused;
	bool displayHUDCheck = true;
	bool godMode = false;
	bool bleedSFXRunOnce = true;
	bool deadSoundOnce = true;

	Rigidbody2D body;

	GameObject floodingControllerObject;
	FloodingController floodingControllerScript;

	GameObject bulletSpawnPoint;
	public GameObject ZombieSpawner;
	public GameObject storeObject;
	GameObject survivorSpawner;
	SurvivorSpawner survivorSpawnerScript;
	StoreController storeControllerScript;
	SecondaryController secondaryController;	
	PrimaryController primaryController;
	PlayerHUD hud;
	SpriteRenderer spriteRenderer;
	//MilestoneController milestoneControllerScript;
	//Spawner spawnerScript;

	public Texture2D hurtEffect;
	public Texture2D hurtEffectTwo;	
	public Texture2D hurtEffectThree;

	public Sprite pistolImage;
	public Sprite rifleImage;
	public Sprite shotGunImage;
	public Sprite launcherImage;
	public Sprite uziImage;
	public Sprite grenadeImage;
	public Sprite gatlingGunImage;
	public Sprite crossBowImage;
	public Sprite bolterImage;
	public Sprite dildoImage;
	public Sprite laserImage;

	GameObject shotGunSpawn1;
	GameObject shotGunSpawn2;
	GameObject shotGunSpawn3;
	GameObject shotGunSpawn4;
	GameObject gatlingSpawnPoint;
	ZombieSpawner zombieSpawnerScript;
	SurvivorButtonsController survivorButtonsController;	
	GameObject soundController;
	SoundController soundControllerScript;


	void Start () {
		Time.timeScale = 1f; // Death Scene Pauses game. This ensures it is not paused when the game starts

		floodingControllerObject = GameObject.Find ("FloodingController");
		floodingControllerScript = floodingControllerObject.GetComponent<FloodingController>();
		body = this.GetComponent<Rigidbody2D>();
		//milestoneControllerScript = this.GetComponent<MilestoneController>();
		bulletSpawnPoint = transform.Find ("BulletSpawnPoint").gameObject;		
		gatlingSpawnPoint = transform.Find ("GatlingGunBulletSpawnPoint").gameObject;
		storeControllerScript = storeObject.GetComponent<StoreController>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();		
		survivorSpawner = GameObject.Find ("SurvivorSpawner");
		survivorSpawnerScript = survivorSpawner.GetComponent<SurvivorSpawner>();
		secondaryController = this.GetComponent<SecondaryController>();
		primaryController = this.GetComponent<PrimaryController>();
		survivorButtonsController = storeObject.GetComponent<SurvivorButtonsController>();
		zombieSpawnerScript = ZombieSpawner.GetComponent<ZombieSpawner>();
		
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();

		hud = this.GetComponent<PlayerHUD>();
		displayHUDCheck = true;
		shotGunSpawn1 = GameObject.Find ("ShotGunSpawnPoint1");
		shotGunSpawn2 = GameObject.Find ("ShotGunSpawnPoint2");
		shotGunSpawn3 = GameObject.Find ("ShotGunSpawnPoint3");
		shotGunSpawn4 = GameObject.Find ("ShotGunSpawnPoint4");

		if(PlayerPrefs.HasKey("JumpScare")){			
			jumpScareToggle = PlayerPrefs.GetInt("JumpScare");
		}

		if (PlayerPrefs.HasKey ("PlayerName")) {			
			playerName = PlayerPrefs.GetString ("PlayerName");
		} else {
			playerName = "Player";
		}

		setPlayerSprite();

		int spawnLocation = (int)Random.Range(0f,4f);

		if(spawnLocation == 0){
			this.transform.position = new Vector3(-12,-12,0);
		}else if(spawnLocation == 1){
			this.transform.position = new Vector3(-12,12,0);
		}else if(spawnLocation == 2){
			this.transform.position = new Vector3(12,12,0);			
		}else if(spawnLocation == 3){
			this.transform.position = new Vector3(12,-12,0);			
		}

		currentHealth = maxHealth;
		currentScraps = 100;//500;
		jumpScareToggle = 0; //Set JumpScare to off, because I'm removing it.
	}

	void Update () {
		if(playerName.Equals("Hunter2379")){			
			cheatCodes(); // <-------------------------------------------------DELETE WHEN DONE!
		}

		if(!paused){
			currentTimePassed += Time.fixedDeltaTime;	
		}

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Get Mouse Position Vector

		if(alive && !paused){
			transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);//Rotate to Face Mouse
			PlayerMovement();//Handle Player Movement
		}

		if (bitten && alive == true) {
			if (bleedSFXRunOnce) {
				soundControllerScript.playSFX ("heartbeat");
				bleedSFXRunOnce = false;
			}

			biteTimer -= Time.deltaTime;//Decrease Bitten Effect Timer
			biteDamage -= Time.deltaTime; // Timer to take Damage every 1 second		
			bittenTextureEffect -= Time.deltaTime;

			if (biteTimer <= 0) {
				bitten = false;
			} 
			if (biteDamage <= 0) {
				currentHealth -= 2;//Take 2 Damage ever second if bitten
				biteDamage = 1f;//Reset BiteDamage Timer
			}
		} else {
			soundControllerScript.stopSFX ("heartbeat");
			bleedSFXRunOnce = true;		
		}
		if(Input.GetKeyDown(KeyCode.Space) && storeControllerScript.getDisplayStore() == false && alive == true && !paused && !roundEndedCheck){ // Deploy Object but only if store not open
			secondaryController.deploySecondary(bulletSpawnPoint);
		}
		if (Input.GetMouseButton(0) && storeControllerScript.getDisplayStore() == false && alive == true && !paused && !roundEndedCheck) {//Click to Shoot but only if store not open
		//if (Input.GetMouseButton(0) && storeControllerScript.getDisplayStore() == false && alive == true && !paused) {//Click to Shoot but only if store not open
			fire = true; //If mouse is helf down, fire will stay true.
		} else {
			fire = false;
		}
		if(fire){//If shooting
			primaryController.shoot(bulletSpawnPoint, shotGunSpawn1, shotGunSpawn2, shotGunSpawn3, shotGunSpawn4, gatlingSpawnPoint);
		}

		if(currentHealth > maxHealth){//So we never pass Max Health
			currentHealth = maxHealth;
		}

		if(currentHealth <= 0){
			OnDeath();
		}

		if((startRoundTimer == true || currentTimePassed > 120f) && setSpawnOnOnce == false){ // If Start button was pressed to start round OR game time > 120 seconds
			startRoundTimer = true; // Not sure if this should be false...
			roundEndedCheck = false;
			//spawnerScript.StartSpawn();//Start Round
			setSpawnOnOnce = true; // Variable to only run this IF Statement Once
		}

		if(roundEndedCheck && setSpawnOnOnce == true){ // Run every round except before first round - setSpawnOnOnce stops it from running before first round starts
			timeUntilRoundStarts -= Time.deltaTime;
		}

		if(timeUntilRoundStarts < 0){//This is what happens when the round starts
			roundEndedCheck = false;
			timeUntilRoundStarts = 46f;
			floodingControllerScript.flood(); //Flooding Controller Run
			survivorSpawnerScript.spawnSurvivors(zombieSpawnerScript.getCurrentRound());
		}

		if(godMode){
			currentHealth = maxHealth;
		}

		if (Input.GetKeyDown(KeyCode.P)) {
			paused = togglePause();
		}

	}
	bool jumpScare = false;
	float jumpScaretimer = .5f;
	public Texture jumpScareFace;

	void OnGUI(){
		if (!storeControllerScript.getDisplayStore() && alive == true) { //If no store overlay and player is alive
			if(displayHUDCheck){
				hud.displayHUD(roundEndedCheck, timeUntilRoundStarts);
			}
		}

		if(bitten && alive){ // If Bitten
			if(bittenTextureEffect >= 6 && bittenTextureEffect <= 8){
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),hurtEffect,ScaleMode.StretchToFill);
			}else if(bittenTextureEffect >= 3 && bittenTextureEffect <= 6){
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),hurtEffectTwo,ScaleMode.StretchToFill);
			}else if(bittenTextureEffect >= 0 && bittenTextureEffect <= 3){
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),hurtEffectThree,ScaleMode.StretchToFill);
			}
		}
		if(jumpScare){
			jumpScaretimer -= Time.deltaTime;
			GUI.DrawTexture(new Rect(100,0,600,600),jumpScareFace,ScaleMode.StretchToFill);
			if(jumpScaretimer < 0){
				jumpScaretimer = .5f;
				jumpScare = false;
			}
		}

	}
	
	void fixedUpdate(){		

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "SentryTurretBullet") {
			currentHealth -= 15;
		}
		if (coll.gameObject.tag == "Zombie") {
			//Knock back and damage are done in Zombie Script
			bitten = true;
			biteTimer = 8f;//Reset BiteTimer			
			biteDamage = 1f;//Reset BiteDamage Timer
			bittenTextureEffect = 8f;

			if(jumpScareToggle == 1){ // Set to 0 by default because I'm removing it
				int jumpScareChance = (int)Random.Range(0f,10f);//Chance for JumpScare
				if(jumpScareChance > -1 && jumpScareChance < 3){//0,1,2 - Then
					jumpScare = true;
				}else {
					//Play Regular Zombie Sound
				}
			}else {
				soundControllerScript.playSFX("ZombieCollision");				
				//Play Regular Zombie Sound
			}
		}
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "RocketExplosion") {
			currentHealth -= 90;//Take 90 Health of Damage if caught within Rocket Explosion
		}			
		if (coll.gameObject.tag == "RotatingLaserBeam") {
			currentHealth -= 20;
		}
		if (coll.gameObject.tag == "LaserLine") {
			currentHealth -= 20;
		}
		if (coll.gameObject.tag == "DeathZone") {
			currentHealth = 0;
		}
		if (coll.gameObject.tag == "HealthPack") {
			int randomHealthAmount = (int)Random.Range(15f,30f);//Random Number for Random Object
			currentHealth+=randomHealthAmount;//Gain Random Amount of Health Health from HealthPack
			if(bitten){ // Reset Bitten Result
				bitten = false;
				biteTimer = 8f;//Reset BiteTimer			
				biteDamage = 1f;//Reset BiteDamage Timer
				bittenTextureEffect = 8f;
			}
			coll.gameObject.DestroyAPS();
			soundControllerScript.playSFX("DingDing");
		}
		if (coll.gameObject.tag == "ScrapsPack") {
			int randomScrapsAmount = (int)Random.Range(10f,20f);//Random Number for Random Object
			currentScraps+=randomScrapsAmount;//Random Amount of Scraps
			coll.gameObject.DestroyAPS();
			soundControllerScript.playSFX("ScrapDing");
		}
		if (coll.gameObject.tag == "AmmoPack") {
			int randomAmmoAmount = (int)Random.Range(10f,17f);//Random Number for Random Object
			primaryController.addRandomAmmo(randomAmmoAmount);
			coll.gameObject.DestroyAPS();
			soundControllerScript.playSFX("gunCock");
		}
		if (coll.gameObject.tag == "DildoBullet") {
			primaryController.addAmmo(9,1);
			coll.gameObject.DestroyAPS();
		}

		if(coll.gameObject.tag == "store"){ // Enable store if player walks within range
			if(storeControllerScript.getStoreAvailable()){//Check to see if in the middle of round
				storeControllerScript.setDisplayStore(true);
			}
		}
		if(coll.gameObject.tag == "RadiationPool"){ // Initial Collision with Radiation Pool
				currentHealth -= 10;
				//This is to give red texture from Radiation Pool like Zombies
				bitten = true;
				biteTimer = 8f;//Reset BiteTimer			
				biteDamage = 1f;//Reset BiteDamage Timer
				bittenTextureEffect = 8f;
		}
		if(coll.gameObject.tag == "Survivor"){ // Initial Collision with Radiation Pool
			if(survivorButtonsController.getFoundSurvivors() < survivorButtonsController.getMaxSurvivors()){
				survivorButtonsController.addUnallocatedSurvivors();
				survivorButtonsController.addFoundSurvivors();
			}
			coll.gameObject.DestroyAPS();
			soundControllerScript.playSFX("DingDing");
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		if(coll.gameObject.tag == "RadiationPool"){//While continously on radiation pool
			radiationPoolDamageTimer -= Time.deltaTime;
			if(radiationPoolDamageTimer < 0){
				currentHealth -= 10;
				radiationPoolDamageTimer = 1f;	
			}
			bitten = true;
			biteTimer = 8f;//Reset BiteTimer			
			biteDamage = 1f;//Reset BiteDamage Timer
			bittenTextureEffect = 8f;
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if(coll.gameObject.tag == "store"){ // Disable store if player walks away from it
			storeControllerScript.setDisplayStore(false);
		}
	}

	void OnDeath(){
		storeControllerScript.setDisplayStore(false);
		alive = false;
		soundControllerScript.stopSFX ("heartbeat");
		if(deadSoundOnce){
			soundControllerScript.playSFX("Dead");
			deadSoundOnce = false;
		}
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
		
		foreach(GameObject item in zombies){
			item.DestroyAPS();
		}
	}
	void PlayerMovement(){
		Vector3 vel = new Vector3();		
		if(Input.GetKey(KeyCode.W)){
			Vector3 velUp = new Vector3();
			// just use 1 to set the direction.
			velUp.y = 1;
			vel += velUp;
		}
		else if(Input.GetKey(KeyCode.S)){
			Vector3 velDown = new Vector3();
			velDown.y = -1;
			vel += velDown;
		}		
		// no else here. Combinations of up/down and left/right are fine.
		if(Input.GetKey(KeyCode.A)){
			Vector3 velLeft = new Vector3();
			velLeft.x = -1;
			vel += velLeft;
		}
		else if(Input.GetKey(KeyCode.D)){
			Vector3 velRight = new Vector3();
			velRight.x = 1;
			vel += velRight;
		}		
		// check if player wants to move at all. Don't check exactly for 0 to avoid rounding errors
		// (magnitude will be 0, 1 or sqrt(2) here)
		if (vel.magnitude > 0.001) {
			Vector3.Normalize(vel);
			vel *= movementSpeed;
			body.velocity = vel;
		}
	}

	public bool isAlive(){
		return alive;
	}
	public void killedZombie(){
		numberOfZombiesKilled++;
	}
	public int howManyZombiesKilled(){
		return numberOfZombiesKilled;
	}
	public float getCurrentTimePassed(){
		return currentTimePassed;
	}
	public int getNumberOfZombiesKilled(){
		return numberOfZombiesKilled;
	}
	public int getCurrentScraps(){
		return currentScraps;
	}
	public void addScraps(int amount){
		currentScraps += amount;
	}
	public void subtractScraps(int amount){
		currentScraps -= amount;
	}
	public int getCurrentScore(){
		return currentScore;
	}
	public void addScore(int amount){
		currentScore += amount;
	}
	public int getCurrentHealth(){
		return currentHealth;
	}
	public void subtractHealth(int amount){
		currentHealth -= amount;
	}
	public void roundEnded(){//This is what happens when round Ends
		roundEndedCheck = true;
	}
	public bool getRoundEnded(){
		return roundEndedCheck;
	}
	public void setRoundEndedCheck(bool value){
		roundEndedCheck = value;
	}
	bool togglePause(){
		if(Time.timeScale == 0f){
			Time.timeScale = 1f;
			return(false);
		}else{
			Time.timeScale = 0f;
			return(true);    
		}
	}
	public void pauseGame(){
		paused = togglePause();
	}
	public bool getPaused(){
		return paused;
	}
	public bool getStartRoundTimer(){
		return startRoundTimer;
	}
	public void setStartRoundTimer(bool value){
		startRoundTimer = value;
	}
	public int getMaxHealth(){
		return maxHealth;
	}
	public void resetTimeUntilRoundStarts(){
		timeUntilRoundStarts = -1;
	}
	public int getCurrentRound(){
		return zombieSpawnerScript.getCurrentRound();
	}
	public float getTimeUntilRoundStarts(){
		return timeUntilRoundStarts;
	}
	public bool getSetSpawnOnOnce(){
		return setSpawnOnOnce;
	}

	public void DrawQuad(Rect position, Color color) {//Method to draw rectangle
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(position, GUIContent.none);
	}
	void cheatCodes(){ // Testing Cheat Codes
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			addScraps(1000);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)){
			storeControllerScript.addCurrentRP(1000);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			zombieSpawnerScript.nextRound();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			zombieSpawnerScript.previousRound();
		}
		if (Input.GetKeyDown(KeyCode.End)){
			if(survivorButtonsController.getFoundSurvivors() < survivorButtonsController.getMaxSurvivors()){
				survivorButtonsController.addUnallocatedSurvivors();
				survivorButtonsController.addFoundSurvivors();
			}
		}
		if (Input.GetKeyDown(KeyCode.G)){
			if(godMode){
				godMode = false;
			}else{
				godMode = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.Delete)){
			addScore(10+zombieSpawnerScript.getCurrentRound());
			numberOfZombiesKilled+=1;
		}
		if(Input.GetKeyDown(KeyCode.K)){//TESTING PURPOSES ONLY - DIE
			currentHealth = 0;
		}

		if(Input.GetKeyDown(KeyCode.Backspace)){
			Application.LoadLevel("MainMenu");
		}

		if (Input.GetKeyDown(KeyCode.Pause)) {
			paused = togglePause();
		}

	}

	public void setPlayerSprite(){
		currentlyEquiped = primaryController.getCurrentlyEquipedWeapon();
		if(currentlyEquiped == 0){
			this.spriteRenderer.sprite = pistolImage;
		}else if(currentlyEquiped == 1){
			this.spriteRenderer.sprite = rifleImage;
		}else if(currentlyEquiped == 2){
			this.spriteRenderer.sprite = shotGunImage;			
		}else if(currentlyEquiped == 3){
			this.spriteRenderer.sprite = launcherImage;			
		}else if(currentlyEquiped == 4){
			this.spriteRenderer.sprite = uziImage;			
		}else if(currentlyEquiped == 5){
			this.spriteRenderer.sprite = grenadeImage;			
		}else if(currentlyEquiped == 6){
			this.spriteRenderer.sprite = gatlingGunImage;			
		}else if(currentlyEquiped == 7){
			this.spriteRenderer.sprite = crossBowImage;			
		}else if(currentlyEquiped == 8){
			this.spriteRenderer.sprite = bolterImage;			
		}else if(currentlyEquiped == 9){
			this.spriteRenderer.sprite = dildoImage;			
		}else if(currentlyEquiped == 10){
			this.spriteRenderer.sprite = laserImage;			
		}
	}

	public void reset(){
		 currentHealth = 0;
		 maxHealth = 100;
		 currentScraps = 0;
		 currentScore = 0;
		 numberOfZombiesKilled = 0;
		 jumpScareToggle = 1;
		 currentlyEquiped = 1;
				
		 movementSpeed = 2f;
		
		 currentTimePassed = 0;
		 timeUntilRoundStarts = 46f;
		
		 biteTimer = 8f;
		 biteDamage = 1f;
		 bittenTextureEffect = 8f;
		 radiationPoolDamageTimer = 1f;
		
		 fire = false;
		 bitten = false;
		 startRoundTimer = false;
		 setSpawnOnOnce = false;
		 alive = true;
		 roundEndedCheck = true;

		 displayHUDCheck = true;
		 godMode = false;
		 bleedSFXRunOnce = true;
		 deadSoundOnce = true;

		displayHUDCheck = true;
		
		if(PlayerPrefs.HasKey("JumpScare")){			
			jumpScareToggle = PlayerPrefs.GetInt("JumpScare");
		}
		
		if (PlayerPrefs.HasKey ("PlayerName")) {			
			playerName = PlayerPrefs.GetString ("PlayerName");
		} else {
			playerName = "Player";
		}
		
		setPlayerSprite();
		
		int spawnLocation = (int)Random.Range(0f,4f);
		
		if(spawnLocation == 0){
			this.transform.position = new Vector3(-12,-12,0);
		}else if(spawnLocation == 1){
			this.transform.position = new Vector3(-12,12,0);
		}else if(spawnLocation == 2){
			this.transform.position = new Vector3(12,12,0);			
		}else if(spawnLocation == 3){
			this.transform.position = new Vector3(12,-12,0);			
		}
		
		currentHealth = maxHealth;
		currentScraps = 100;//500;
		jumpScareToggle = 0; //Set JumpScare to off, because I'm removing it.

		body.isKinematic = true;
		body.isKinematic = false;
	}
}