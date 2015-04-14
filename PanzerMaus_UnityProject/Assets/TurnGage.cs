using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnGage : MonoBehaviour {

	public int turnNumber;
	public Color on, off;
	private Image image;


	void Start(){
		image = GetComponent<Image>();
	}

	void Update(){
		image.color = (turnNumber <= MoveManager.MovesRemaining) ? on : off;
	}
}
