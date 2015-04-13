using UnityEngine;
using System.Collections;

public class PlayerTurnControl : MonoBehaviour {

	public delegate void PlayerTurn(int playerIndex);
	public static event PlayerTurn playerTurn;

	public delegate void Inbetween();
	public static event Inbetween inbetween;

	public delegate void Movement(int move);
	public static event Movement movement;

	public delegate void CameraRefresh();
	public static event CameraRefresh cameraRefresh;

	private static int turn = 0; 
	private static int playerCount = 0;
	private static int move = 0;

	void Start(){
		playerCount = playerTurn.GetInvocationList().Length;
		NextTurn ();
	}

	public static void NextTurn(){
		if(playerTurn != null && cameraRefresh != null)
		{
			turn = ++turn % playerCount;
			playerTurn(turn);
			cameraRefresh();
		}
	}

	public static void StartInbetween() {
		if(inbetween != null) {
			inbetween();
		}
	}

	public static int GetTurn()
	{
		return turn;
	}

	public static int GetMove(int player)
	{
		return (player == turn) ? move : 0;
	}

	public static void SetMove(int m)
	{
		move = m;
	}
}