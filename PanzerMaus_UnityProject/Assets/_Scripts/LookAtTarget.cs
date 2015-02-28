using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

	public Transform target;

	void Update () {
		transform.LookAt(new Vector3(target.position.x, target.position.y, 0), Vector3.up);
	}
}
