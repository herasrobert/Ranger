using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	int sound = 1;
	int music = 1;

	AudioSource[] audio;
	AudioSource bubbleClick;
	AudioSource hardClick;
	AudioSource heartbeat;
	AudioSource gunCock;
	AudioSource crossbowShot;
	AudioSource backgroundMusic;
	AudioSource storeBuy;
	AudioSource zombieCollision;
	AudioSource dingDing;
	AudioSource scrapDing;
	AudioSource pistolSound;
	AudioSource rifleSound;
	AudioSource shotgunSound;
	AudioSource rocketLauncherSound;
	AudioSource grenadeLauncherSound;
	AudioSource laserSound;
	AudioSource explosionSound;
	AudioSource deploySound;
	AudioSource secondaryLaserSounds;
	AudioSource deathSound;
	AudioSource alarmSound;


	void Start () {
		audio = GetComponents<AudioSource>();
		bubbleClick = audio[0];
		hardClick = audio[1];
		heartbeat = audio[2];
		gunCock = audio[3];
		crossbowShot = audio[4];
		backgroundMusic = audio[5];
		storeBuy = audio[6];
		zombieCollision = audio[7];
		dingDing = audio[8];
		scrapDing = audio[9];
		pistolSound = audio[10];
		rifleSound = audio[11];
		shotgunSound = audio[12];
		rocketLauncherSound = audio[13];
		grenadeLauncherSound = audio[14];
		laserSound = audio[15];
		explosionSound = audio[16];
		deploySound = audio[17];
		secondaryLaserSounds = audio[18];
		deathSound = audio[19];
		alarmSound = audio[20];

		if (PlayerPrefs.HasKey ("Music") == false) {			
			PlayerPrefs.SetInt ("Music", 1);
		} else {
			music = PlayerPrefs.GetInt("Music");
		}

		if (PlayerPrefs.HasKey ("Sound") == false) {			
			PlayerPrefs.SetInt ("Sound", 1);
		} else {
			sound = PlayerPrefs.GetInt("Sound");
		}

		playMusic("backgroundMusic");
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1){// Prevent Duplicated of this GameObject due to DontDestroyOnLoad
			Destroy(gameObject);
		}
	}

	void Update () {

	}

	public void playSFX(string sfx){
		if (sound == 1) {		
			if (sfx.Equals ("bubbleClick")) {
				bubbleClick.Play ();
			}else if (sfx.Equals ("hardClick")) {
				hardClick.Play ();
			}else if (sfx.Equals ("heartbeat")) {
				heartbeat.Play ();
			}else if (sfx.Equals ("gunCock")) {
				gunCock.Play();
			}else if (sfx.Equals ("crossbowShot")) {
				crossbowShot.Play();
			}else if (sfx.Equals ("buy")) {
				storeBuy.Play();
			}else if (sfx.Equals ("ZombieCollision")) {
				zombieCollision.Play();
			}else if (sfx.Equals ("DingDing")) {
				dingDing.Play();
			}else if (sfx.Equals ("ScrapDing")) {
				scrapDing.Play();
			}else if (sfx.Equals ("Pistol")) {
				pistolSound.Play();
			}else if (sfx.Equals ("Rifle")) {
				rifleSound.Play();
			}else if (sfx.Equals ("Shotgun")) {
				shotgunSound.Play();
			}else if (sfx.Equals ("Rocket")) {
				rocketLauncherSound.Play();
			}else if (sfx.Equals ("Grenade")) {
				grenadeLauncherSound.Play();
			}else if (sfx.Equals ("Laser")) {
				laserSound.Play();
			}else if (sfx.Equals ("Explosion")) {
				explosionSound.Play();
			}else if (sfx.Equals ("Deploy")) {
				deploySound.Play();
			}else if (sfx.Equals ("SecondaryLaserSounds")) {
				secondaryLaserSounds.Play();
			}else if (sfx.Equals ("Dead")) {
				deathSound.Play();
			}else if (sfx.Equals ("Alarm")) {
				alarmSound.Play();
			}
		}
	}
	public void stopSFX(string sfx){
		if (sfx.Equals ("heartbeat")) {
			heartbeat.Stop();//AWS Play
		}else if (sfx.Equals ("SecondaryLaserSounds")) {
			secondaryLaserSounds.Stop();
		}
	}
	public void playSFXOnce(string sfx){

	}

	public void playMusic(string piece){
		if(music == 1){			
			if (piece.Equals ("backgroundMusic")) {
				backgroundMusic.Play();
			}
		}
	}
	public void stopMusic(string piece){
		if (piece.Equals ("backgroundMusic")) {
			audio[5].Stop();
		}
	}



	public void setSoundOn(){
		sound = 1;
	}
	public void setSoundOff(){
		sound = 0;
		for(int x = 0; x<= audio.Length-1; x++){
			if(x != 5){ // Skip 5 because 5 is the background music not a sound
				audio[x].Stop();
			}
		}
	}
	public int getSound(){
		return sound;
	}
	public void setMusicOn(){
		music = 1;
	}
	public void setMusicOff(){
		music = 0;
	}
	public int getMusic(){
		return music;
	}


}
