using UnityEngine;
using System.Collections;

public class CannonEndNodeBehavior : MonoBehaviour {

	public int playerIndex;
	public GameObject UICanvas;

	public void Awake() {
		PlayerTurnControl.playerTurn += setTurn;
		PlayerTurnControl.inbetween += deactivate;
	}

	public void setTurn(int p) {
		if (p == playerIndex){
			UICanvas.SetActive(true);
			FireEvent.position += GetPosition;
			FireEvent.trajectory += GetTrajectory;
			AimControl.trajectory += GetTrajectory;
		}
	}

	public void deactivate(){
		UICanvas.SetActive(false);
	}

	public Vector3 GetPosition() {
		return transform.position;
	}

	public Vector3 GetTrajectory() {
		return Vector3.Normalize(transform.position - transform.parent.parent.position);
	}
}