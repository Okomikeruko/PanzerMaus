using UnityEngine;
using System.Collections;

public class TankWheelMotion : MonoBehaviour {

	public float speed = 1;

	void Update(){
		transform.Rotate (0, 0, Input.GetAxis ("Horizontal") * speed);
	}
}
