using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MovementSound : MonoBehaviour {

	public AudioClip cannonFire;
	private AudioSource source; 
	private float volLowRange = .5f; 
	private float volHighRange = 1f;

	void Awake () {
		source = GetComponent<AudioSource> ();
	}

	void Update () {
		float vol = Random.Range (volLowRange, volHighRange);
		source.PlayOneShot (cannonFire, vol);
	}
}
