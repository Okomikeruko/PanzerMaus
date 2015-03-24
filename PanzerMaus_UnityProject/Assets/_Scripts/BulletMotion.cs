using UnityEngine;
using System.Collections;

public class BulletMotion : MonoBehaviour {

	private Vector3 lastPosition;
	public float offset; 
	public bool firing = false, left;
	public float angle;

	public void fire(bool isLeft) {
		lastPosition = transform.position;;
		firing = true;
		left = isLeft;
	}

	void Update () {
		if(firing){
			float deltaY = transform.position.y - lastPosition.y;
			float deltaX = transform.position.x - lastPosition.x;
			angle = Mathf.Atan (deltaY/deltaX) * Mathf.Rad2Deg;
			angle += left ? 180 : 0;  
			if (Mathf.Abs(angle) > 0.005f){
				transform.rotation = Quaternion.Euler(new Vector3(0,0,angle + offset));
			}
			lastPosition = transform.position;
		}
	}
}
