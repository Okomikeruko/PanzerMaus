using UnityEngine;
using System.Collections;

public class TankMotionControl : MonoBehaviour {

	public bool movement = false;
	private int stopper = 1;
	public float speed, maxSpeed, groundDistance, angularDistance, rotationalSpeed, rotationalTolerance, gravitationalForce, forwardDistance, rearDistance, angularGroundDistance;
	public Vector2 offset;
	private Vector3 offsetTransform;
	private float move;
	private bool turn = false;
	private Vector3 gravity;
	private GameObject climber;
	private RaycastHit2D down, forwardDown, backwardDown, forward, backward;
	private CannonEndNodeBehavior c;

	void Start()
	{
		c = GetComponentInChildren<CannonEndNodeBehavior>();
	}

	void Update()
	{
		offsetTransform = new Vector3 (transform.position.x + offset.x,
		                               transform.position.y + offset.y,
		                               transform.position.z);

		move = PlayerTurnControl.GetMove(c.playerIndex); 
		if(movement & rigidbody2D.velocity.magnitude < maxSpeed){
			rigidbody2D.AddForce((transform.right + (transform.up * 0.1f)) * move * stopper * speed, ForceMode2D.Force);
		}

		down = getDirection(-transform.up);
		forwardDown = getDirection ((-transform.up + transform.right).normalized);
		backwardDown = getDirection ((-transform.up + -transform.right).normalized);
		forward = getDirection (transform.right);
		backward = getDirection (-transform.right);

		gravity = (down.distance < groundDistance * ((turn) ? 2 : 1)) ? new Vector3(-down.normal.x, -down.normal.y, 0) : Vector3.down;

		Debug.DrawRay (transform.position, -transform.up);
		Debug.DrawRay (transform.position, (-transform.up + transform.right).normalized);
		Debug.DrawRay (transform.position, (-transform.up + -transform.right).normalized);
		Debug.DrawRay (offsetTransform, transform.right);
		Debug.DrawRay (offsetTransform, -transform.right);

		float sqrt = down.distance * Mathf.Sqrt(2) + rotationalTolerance;

		/* Outide Corner Turn */

		if (sqrt <= forwardDown.distance * angularGroundDistance && move > 0 && forwardDistance < forward.distance && !turn || 
		    sqrt <= backwardDown.distance * angularGroundDistance && move < 0 && rearDistance < backward.distance && !turn){
				rigidbody2D.velocity = Vector2.zero;
				stopper = 0;
				transform.Rotate(0, 0, -move * rotationalSpeed);
		}else{
			if(!turn){
				rigidbody2D.AddForce(gravity * gravitationalForce, ForceMode2D.Force);
			}
			stopper = 1;
		}

		rigidbody2D.fixedAngle = turn;

		/* Inside Corner Turn */
		if (forward.distance < forwardDistance && move > 0 || 
		    backward.distance < rearDistance && move < 0){
			turn = true;
		}

		if (forward.distance > forwardDistance * angularDistance && move > 0 ||
		    backward.distance > rearDistance * angularDistance && move < 0 ||
			move == 0) {
			turn = false;
		}



		if (turn){
//			rigidbody2D.velocity = Vector2.zero;
//			stopper = 0;
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