using UnityEngine;
using System.Collections;

public class TankMotionControl : MonoBehaviour {

	public bool movement = false;
	private int stopper = 1;
	public float speed, maxSpeed, groundDistance, rotationalSpeed, rotationalTolerance, gravitationalForce, forwardDistance, rearDistance;

	private float move;
	private bool turn = false;
	private Vector3 gravity;
	private GameObject climber;
	private RaycastHit2D down, forwardDown, backwardDown, forward, backward;

	void Update()
	{
		move = Input.GetAxis("Horizontal");
		if(movement & rigidbody2D.velocity.magnitude < maxSpeed){
			rigidbody2D.AddForce(transform.right * move * stopper * speed, ForceMode2D.Force);
		}

		down = getDirection(-transform.up);
		forwardDown = getDirection ((-transform.up + transform.right).normalized);
		backwardDown = getDirection ((-transform.up + -transform.right).normalized);
		forward = getDirection (transform.right);
		backward = getDirection (-transform.right);

		Debug.DrawRay (transform.position, -transform.up);
		Debug.DrawRay (transform.position, (-transform.up + transform.right).normalized);
		Debug.DrawRay (transform.position, (-transform.up + -transform.right).normalized);
		Debug.DrawRay (transform.position, transform.right);
		Debug.DrawRay (transform.position, -transform.right);

		float sqrt = down.distance * Mathf.Sqrt(2) + rotationalTolerance;

		if (sqrt <= forwardDown.distance && move > 0 && forwardDistance < forward.distance && !turn || 
		    sqrt <= backwardDown.distance && move < 0 && rearDistance < backward.distance && !turn){
				rigidbody2D.velocity = Vector2.zero;
				stopper = 0;
				transform.Rotate(0, 0, -move * rotationalSpeed);
		}else{
			stopper = 1;
		}

		if (forward.distance < forwardDistance && move > 0 || 
		    backward.distance < rearDistance && move < 0){
			turn = true;
		}

		if (forward.distance > forwardDistance * 3 && move > 0 ||
		    backward.distance > rearDistance * 3 && move < 0){
			turn = false;
		}

		Debug.Log (turn);



		gravity = (down.distance < groundDistance) ? new Vector3(-down.normal.x, -down.normal.y, 0) : Vector3.down;
		rigidbody2D.AddForce(gravity * gravitationalForce, ForceMode2D.Force);

		if (turn){
			rigidbody2D.velocity = Vector2.zero;
			stopper = 0;
			transform.Rotate(0, 0, move * rotationalSpeed);
		}
	}


	private RaycastHit2D getDirection (Vector2 v)
	{
		return Physics2D.Raycast (transform.position, 
		                          v, 
		                          Mathf.Infinity, 
		                          LayerMask.GetMask("Ground"));
	}
}