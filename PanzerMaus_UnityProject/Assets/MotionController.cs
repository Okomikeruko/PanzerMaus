using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {

	public void SetMove(int move)
	{
		PlayerTurnControl.SetMove(move);
		PlayerTurnControl.setTimer(move != 0);
	}
}
