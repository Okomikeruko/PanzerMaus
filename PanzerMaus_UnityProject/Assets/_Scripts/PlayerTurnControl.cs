using UnityEngine;
using System.Collections;

public class PlayerTurnControl : MonoBehaviour {

	public delegate void PlayerTurn(int playerIndex);
	public static event PlayerTurn playerTurn;

	public delegate void Inbetween();
	public static event Inbetween inbetween;


	private static int turn = 0; 
	private static int playerCount = 0;

	void Start(){
		playerCount = playerTurn.GetInvocationList().Length;
		NextTurn ();
	}

	public static void NextTurn(){
		if(playerTurn != null)
		{
			playerTurn(turn);
			turn = ++turn % playerCount;
		}
	}

	public static void StartInbetween() {
		if(inbetween != null) {
			inbetween();
		}
	}
}
