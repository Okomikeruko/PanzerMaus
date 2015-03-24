using UnityEngine;
using System.Collections;

public class FireEvent : MonoBehaviour {

	public float LaunchPower = 10, BlastRadius = 5, ExplosiveForce = 3;

	public delegate Vector3 Vectors();
	public static event Vectors position;
	public static event Vectors trajectory;

	public delegate float Floats();
	public static event Floats power;

	private BulletMotion bulletMotion;
	private Collision2D collision2D;

	void Start () {
		bulletMotion = GetComponent<BulletMotion>();
	}

	void OnCollisionEnter2D(Collision2D col){
		collision2D = col;
		if (col.gameObject.tag != "Player") {
			FireEventControl.Explode ();
			SpendMe ();
		}
	}

	public void LoadAmmo () {
		AimControl.WeaponPower = LaunchPower; 
		FireEventControl.fireEvent += FireMe; 
		FireEventControl.explosionData += ExplodeMe;
	}

	void FireMe() {
		AimControl.WeaponPower = 0;
		renderer.enabled = true;
		Vector3 pos = (position != null) ? position() : Vector3.zero;
		Vector3 tra = (trajectory != null) ? trajectory() : Vector3.zero;
		bulletMotion.fire (tra.x < 0);
		float pow = (power != null) ? power() : 0f;
		transform.position = pos;
		rigidbody2D.isKinematic = false;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce(new Vector2(tra.x, tra.y) * pow * LaunchPower, ForceMode2D.Impulse);
		FireEventControl.fireEvent -= FireMe;
		position = null;
		trajectory = null;
		PlayerTurnControl.StartInbetween();
	}

	void SpendMe() {
		bulletMotion.firing = false;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
		rigidbody2D.isKinematic = true;
		transform.position = transform.parent.position;
		transform.rotation = Quaternion.identity;
		renderer.enabled = false;
		FireEventControl.ResetExplosionData();
		PlayerTurnControl.NextTurn();
	}

	Explosion ExplodeMe(){
		return new Explosion(ExplosiveForce, BlastRadius, transform.position, transform.position + (transform.up * BlastRadius), transform.position + (-transform.up * BlastRadius), collision2D); 
	}
}

public class Explosion{
	public float power, radius;
	public Vector3 point, start, end;
	public Collision2D col;
	public Explosion(float p, float r, Vector3 pt, Vector3 st, Vector3 e, Collision2D c){
		power = p; 
		radius = r; 
		point = pt;
		start = st;
		end = e;
		col = c;

	}
}
