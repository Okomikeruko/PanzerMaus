using UnityEngine;
using System.Collections;

public class BulletMotion : MonoBehaviour {

	private Vector3 lastPosition;
	public float offset; 
	public bool firing = false;

	public void fire() {
		lastPosition = transform.position;;
		firing = true;
	}

	void Update () {
		if(firing){
			float deltaY = transform.position.y - lastPosition.y;
			float deltaX = transform.position.x - lastPosition.x;
			float angle = Mathf.Atan (deltaY/deltaX) * Mathf.Rad2Deg;
			if (Mathf.Abs(angle) > 0.005f){
				transform.rotation = Quaternion.Euler(new Vector3(0,0,angle + offset));
			}
			lastPosition = transform.position;
		}
	}
}
