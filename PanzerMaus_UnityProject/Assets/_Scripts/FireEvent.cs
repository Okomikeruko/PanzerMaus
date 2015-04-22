using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BulletMotion))]
[RequireComponent(typeof(AudioSource))]

public class FireEvent : MonoBehaviour {

	public float LaunchPower = 10, BlastRadius = 5, ExplosiveForce = 3;

	public AudioClip cannonFire;
	private AudioSource source; 

	public delegate Vector3 Vectors();
	public static event Vectors position;
	public static event Vectors trajectory;

	public delegate float Floats();
	public static event Floats power;

	private BulletMotion bulletMotion;
	private Collision2D collision2D;

	void Start () {
		bulletMotion = GetComponent<BulletMotion>();
		source = GetComponent<AudioSource> ();
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
		CameraControl.followBullet = true;
		source.PlayOneShot (cannonFire);
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
	}

	Explosion ExplodeMe() {
		return new Explosion(ExplosiveForce, 
		                     BlastRadius, 
		                     transform.position, 
		                     transform.position + (transform.up * BlastRadius), 
		                     transform.position + (-transform.up * BlastRadius), 
		                     collision2D); 
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

public class TextureData{
	
	public int x, y, width, height;
	
	public TextureData GetOverlap(Texture2D a, Texture2D b, Vector2 pointA, Vector2 pointB, string returnData)
	{
		TextureData output = new TextureData();
		output.width = (int)((isOutside(pointA.x, 0, a.width)) 
		                     ? (pointA.x > 0 ) 
		                     ? smaller ((b.width - pointB.x) - (pointA.x - a.width), a.width)
		                     : smaller ((b.width - pointB.x) + pointA.x, a.width) 
		                     : smaller (pointA.x, pointB.x) + smaller (a.width - pointA.x, b.width - pointB.x));
		
		output.height = (int)((isOutside (pointA.y, 0, a.height)) 
		                      ? (pointA.y > 0 ) 
		                      ? smaller ((b.height - pointB.y) - (pointA.y - a.height), a.height)
		                      : smaller ((b.height - pointB.y) + pointA.y, a.height)
		                      : smaller (pointA.y, pointB.y) + smaller (a.height - pointA.y, b.height - pointB.y)); 
		switch (returnData){
		case "a":
			output.x = (int)Mathf.Clamp (pointA.x - pointB.x, 0, smaller (a.width / 2, b.width / 2));
			output.y = (int)Mathf.Clamp (pointA.y - pointB.y, 0, smaller (a.height / 2, b.height / 2));
			return output;
		case "b":
			output.x = (int)Mathf.Clamp ((b.width - pointB.x) - pointA.x , 0, (b.width)); // - pointB.x));
			output.y = (int)Mathf.Clamp ((b.height - pointB.y) - pointA.y, 0, (b.height)); // - pointB.y));
			return output;
		default:
			return new TextureData();
		}
	}
	
	private float smaller(float a, float b){
		return (a > b) ? b : a;
	}
	
	private bool isOutside(float num, float min, float max)
	{
		return (num < min || num > max);
	}
}
