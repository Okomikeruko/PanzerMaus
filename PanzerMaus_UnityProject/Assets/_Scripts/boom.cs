using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class boom : MonoBehaviour {

	public GameObject round, flat;

	void Start () {
		FireEventControl.explosionEvent += ExplodingEvent;
	}

	void ExplodingEvent(Explosion data){
		CameraControl.followBullet = false;
		Instantiate(round, data.point, Quaternion.identity);
		StartCoroutine(EndOfTurn(2.0f));
	}

	IEnumerator EndOfTurn(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		MoveManager.EndMove ();
	}
}