using UnityEngine;
using System.Collections;

public class CannonEndNodeBehavior : MonoBehaviour {

	void Start()
	{
		FireEvent.position += GetPosition;
		FireEvent.trajectory += GetTrajectory;
		AimControl.trajectory += GetTrajectory;
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public Vector3 GetTrajectory()
	{
		return Vector3.Normalize(transform.position - transform.parent.parent.position);
	}

}
