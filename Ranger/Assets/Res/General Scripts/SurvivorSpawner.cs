using UnityEngine;
using System.Collections;

public class SurvivorSpawner : MonoBehaviour {

	public GameObject survivor;
	PoolingSystem pS;
	void Start(){	
		pS = PoolingSystem.Instance;
	}

	void Update(){}

	public void spawnSurvivors (int currentRound){
		for(int i=1; i<= currentRound;i++){
			int randomX = (int)Random.Range(-25f,25f);//Random X for Survivor
			int randomY = (int)Random.Range(-23f,25f);//Random Y for Survivor

			pS.InstantiateAPS("survivor", new Vector3(randomX, randomY, 0f), Quaternion.identity);
		}
	}
}
