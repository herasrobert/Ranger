using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {
	
	float DestroyLaserTimer = .1f;

	void Start () {
		DestroyLaserTimer = .1f;
	}

	void OnEnable(){
	}

	void Update () {
		DestroyLaserTimer -= Time.deltaTime;

		if(DestroyLaserTimer < 0){
			gameObject.DestroyAPS();
		}
	
	}
}
