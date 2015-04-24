using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

	public Transform target;

	void Update () {
		Vector3 direction = target.position - transform.position;
		Quaternion toRotation = Quaternion.FromToRotation (Vector3.up, direction);
		transform.rotation = Quaternion.Lerp (transform.rotation, toRotation, 10);
	}
}
