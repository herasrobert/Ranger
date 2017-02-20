using UnityEngine;
using System.Collections;

public class SurvivorController : MonoBehaviour {

	float lifeTimer = 30f; 

	void Start () {
	
	}
	void OnEnable(){
		lifeTimer = 30f; 
	}

	void Update () {
		transform.Rotate(0,0,-3);
		lifeTimer -= Time.deltaTime;

		if(lifeTimer < 0){
			this.gameObject.DestroyAPS();
		}
	
	}
}
