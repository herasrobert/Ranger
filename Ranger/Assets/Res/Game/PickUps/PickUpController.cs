using UnityEngine;
using System.Collections;

public class PickUpController : MonoBehaviour {
	float destroyTimer = 16f;

	void Start () {
	}
	void OnEnable(){
		destroyTimer = 16f;
	}

	void Update () {
		transform.Rotate(0,0,-3);
		destroyTimer -= Time.deltaTime;
		if(destroyTimer <= 0){
			this.gameObject.DestroyAPS();
		}
	}
}
