using UnityEngine;
using System.Collections;

public class TankMotionControl : MonoBehaviour {

	public bool movement = false;
	public float speed, maxSpeed;
	void Update()
	{
		float move = Input.GetAxis("Horizontal");
		if(movement & rigidbody2D.velocity.magnitude < maxSpeed){
			rigidbody2D.AddRelativeForce(new Vector2(
				move,
				-Mathf.Abs (move / 5)) * speed, ForceMode2D.Force);
		}
	}
}
