using UnityEngine;
using System.Collections;

public class RotatorController : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	PlayerController playerScript;
	GameObject store;
	GameObject storeOne;
	GameObject storeTwo;
	GameObject storeThree;
	GameObject player;
	
	Vector3 mouse_pos;
	Vector3 vectorToStore;
	Vector3 vectorToStoreOne;
	Vector3 vectorToStoreTwo;
	Vector3 vectorToStoreThree;

	void Start () {
		store = GameObject.Find("Store");
		storeOne = GameObject.Find("Store 1");
		storeTwo = GameObject.Find("Store 2");
		storeThree = GameObject.Find("Store 3");
		player = GameObject.Find("Player");

		spriteRenderer = GetComponent<SpriteRenderer>(); // Declare SpriteRenderer Component
		playerScript = player.GetComponent<PlayerController> ();

		spriteRenderer.enabled = false;//Disable Sprite Renderer
	}
	void Update (){
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y,0);

		if (playerScript.getRoundEnded()) { // If Round Ended
			vectorToStore = store.transform.position - transform.position;
			vectorToStoreOne = storeOne.transform.position - transform.position;
			vectorToStoreTwo = storeTwo.transform.position - transform.position;
			vectorToStoreThree = storeThree.transform.position - transform.position;

			if ((vectorToStore.magnitude < vectorToStoreOne.magnitude) && (vectorToStore.magnitude < vectorToStoreTwo.magnitude) && (vectorToStore.magnitude < vectorToStoreThree.magnitude)) {
				setup(store);
			}else if((vectorToStoreOne.magnitude < vectorToStore.magnitude) && (vectorToStoreOne.magnitude < vectorToStoreTwo.magnitude) && (vectorToStoreOne.magnitude < vectorToStoreThree.magnitude)) {
				setup(storeOne);
			}else if((vectorToStoreTwo.magnitude < vectorToStore.magnitude) && (vectorToStoreTwo.magnitude < vectorToStoreOne.magnitude) && (vectorToStoreTwo.magnitude < vectorToStoreThree.magnitude)) {
				setup(storeTwo);
			}else if((vectorToStoreThree.magnitude < vectorToStore.magnitude) && (vectorToStoreThree.magnitude < vectorToStoreOne.magnitude) && (vectorToStoreThree.magnitude < vectorToStoreTwo.magnitude)) {
				setup(storeThree);
			}
		} else {
			spriteRenderer.enabled = false;//Disable Sprite Renderer
		}
	}

	void setup(GameObject obj){
		Vector3 vectorToTarget = obj.transform.position - transform.position;
		if (vectorToTarget.magnitude > 3) {
			float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * 10);
			spriteRenderer.enabled = true; //Enable Sprite Renderer
		} else {
			spriteRenderer.enabled = false; //Disable Sprite Renderer
		}
	}
}
