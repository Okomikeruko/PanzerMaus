using UnityEngine;
using System.Collections;

public class FireEventControl : MonoBehaviour {

	public delegate void FireEvent();
	public static event FireEvent fireEvent;

	public delegate void ExplosionEvent(Explosion x);
	public static event ExplosionEvent explosionEvent;

	public delegate Explosion ExplosionData();
	public static event ExplosionData explosionData;

	public void Fire() {
		if (fireEvent != null) {
			fireEvent();
		}
	}

	public static void Explode() {
		if (explosionEvent != null && explosionData != null){
			explosionEvent(explosionData());
		}
	}

	public static void ResetExplosionData(){
		explosionData = null;
	}
}
