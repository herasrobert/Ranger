using UnityEngine;
using System.Collections;

public class LandMineController : MonoBehaviour {
	
	public GameObject RocketExplosion;	
	PoolingSystem pS;

	void Start () {	
		pS = PoolingSystem.Instance;
	}

	void Update () {		
		transform.Rotate(0,0,-3); //Rotate Bomb
	}

	void OnTriggerEnter2D(Collider2D coll) {		
		if (coll.gameObject.tag == "Zombie" || coll.gameObject.tag == "Player") {
			pS.InstantiateAPS("rocketExplosion", transform.position, transform.rotation);
			Destroy(this.gameObject);
		}	
	}
}
