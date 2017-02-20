using UnityEngine;
using System.Collections;

public class GatlingGunSpawnPointRotationController : MonoBehaviour {

	int rotation = 0;
	bool sentryUp = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (sentryUp) {
			rotation += 1;
			transform.Rotate (0, 0, +1);
		} else {
			rotation -= 1;
			transform.Rotate (0, 0, -1);
		}
		
		if (rotation == 5) { //Switched from 90 to 80 Degrees
			sentryUp = false;
		} else if (rotation == -5) {
			sentryUp = true;
		}
	}
}
