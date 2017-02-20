using UnityEngine;
using System.Collections;

public class DildoController : MonoBehaviour {

	float dildoDestroyTimer = 10f;
	int rotation = 0;
	bool dildoUp = true;

	void Start () {
	}
	void OnEnable(){
		dildoDestroyTimer = 10f;
	}


	void Update () {
		dildoDestroyTimer -= Time.deltaTime;


		if(dildoUp){
			rotation += 1;
			transform.Rotate(0,0,+1);
		}else{
			rotation -= 1;
			transform.Rotate(0,0,-1);
		}
		
		if(rotation==80){ //Switched from 90 to 80 Degrees
			dildoUp=false;
		}else if(rotation==1){
			dildoUp=true;
		}

		if (dildoDestroyTimer < 0){
			gameObject.DestroyAPS();
		}
	}


}
