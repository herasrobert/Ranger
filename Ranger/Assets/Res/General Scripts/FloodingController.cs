using UnityEngine;
using System.Collections;

public class FloodingController : MonoBehaviour {

	public GameObject FloodRadiationPool;
	public GameObject ZombieSpawner;
	ZombieSpawner zombieSpawnerScript;

	GameObject soundController;
	SoundController soundControllerScript;
	PoolingSystem pS;

	void Start () {
		zombieSpawnerScript = ZombieSpawner.GetComponent<ZombieSpawner> ();
		soundController = GameObject.Find ("SoundController");
		soundControllerScript = soundController.GetComponent<SoundController>();		
		pS = PoolingSystem.Instance;
	}
	void Update () {}

	public void flood(){
		if (zombieSpawnerScript.getCurrentRound() % 2 == 0) {
			spawnPool();
		}

		if (zombieSpawnerScript.getCurrentRound() % 3 == 0) {
			spawnPool();
		}
	}

	void spawnPool(){
		soundControllerScript.playSFX("Alarm");
		int randomQuadrant = (int)Random.Range (0f, 4f); // 0=BottomLeft, 1=TopLeft,2=TopRight,3=BottomRight
		
		if (randomQuadrant == 0) {
			//Instantiate (FloodRadiationPool, new Vector3 (-13f, -13f, 0), Quaternion.identity);//Spawn Barricade
			pS.InstantiateAPS("FloodRadiationPool", new Vector3 (-13f, -13f, 0), Quaternion.identity);
		} else if (randomQuadrant == 1) {
			//Instantiate (FloodRadiationPool, new Vector3 (-13f, 13f, 0), Quaternion.identity);//Spawn Barricade
			pS.InstantiateAPS("FloodRadiationPool", new Vector3 (-13f, 13f, 0), Quaternion.identity);
		} else if (randomQuadrant == 2) {
			//Instantiate (FloodRadiationPool, new Vector3 (13f, 13f, 0), Quaternion.identity);//Spawn Barricade
			pS.InstantiateAPS("FloodRadiationPool", new Vector3 (13f, 13f, 0), Quaternion.identity);
		} else if (randomQuadrant == 3) {
			//Instantiate (FloodRadiationPool, new Vector3 (13f, -13f, 0), Quaternion.identity);//Spawn Barricade
			pS.InstantiateAPS("FloodRadiationPool", new Vector3 (13f, -13f, 0), Quaternion.identity);
		}
	}
}