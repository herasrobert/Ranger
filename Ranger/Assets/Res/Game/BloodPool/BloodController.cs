using UnityEngine;
using System.Collections;

public class BloodController : MonoBehaviour {

	float bloodPoolTimer = 9f;

	SpriteRenderer spriteRenderer;

	public Sprite bloodOne;
	public Sprite bloodTwo;
	public Sprite bloodThree;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	void OnEnable(){
		bloodPoolTimer = 9f; 
	}

	void Update () {
		bloodPoolTimer-=Time.deltaTime;

		if(bloodPoolTimer <= 9 && bloodPoolTimer > 7){
			spriteRenderer.sprite = bloodOne;
		}else if(bloodPoolTimer < 7 && bloodPoolTimer > 4){
			spriteRenderer.sprite = bloodTwo;
		}else if(bloodPoolTimer < 3 && bloodPoolTimer > 1){
			spriteRenderer.sprite = bloodThree;
		}else if(bloodPoolTimer < 0){
			this.gameObject.DestroyAPS();
		}
	}
}
