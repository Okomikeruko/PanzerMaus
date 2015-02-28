using UnityEngine;
using System.Collections;

public class FireEvent : MonoBehaviour {

	public float WeaponPower = 10;

	public delegate Vector3 Vectors();
	public static event Vectors position;
	public static event Vectors trajectory;

	public delegate float Floats();
	public static event Floats power;

	void Start () {
		LoadAmmo ();
	}

	void LoadAmmo () {
		AimControl.WeaponPower = WeaponPower; 
		FireEventControl.fireEvent += FireMe; 
	}

	void FireMe() {
		Vector3 pos = (position != null) ? position() : Vector3.zero;
		Vector3 tra = (trajectory != null) ? trajectory() : Vector3.zero;
		float pow = (power != null) ? power() : 0f;
		transform.position = pos;
		rigidbody2D.velocity = Vector2.zero;
		Debug.Log ("Power: " + pow.ToString());
		rigidbody2D.AddForce(new Vector2(tra.x, tra.y) * pow * WeaponPower, ForceMode2D.Impulse);
	}
}
