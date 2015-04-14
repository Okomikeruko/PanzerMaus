using UnityEngine;
using System.Collections;

public class PlayerTurnControl : MonoBehaviour {

	public delegate void DelegateInt(int i);
	public static event DelegateInt playerTurn, movement, fire;

	public delegate void Delegate();
	public static event Delegate inbetween, cameraRefresh;

	private static int turn = 0, playerCount = 0, move = 0;
	private static float timer = 5;
	private static bool timerOn = false; 
	void Start(){
		playerCount = fire.GetInvocationList().Length;
		turn = playerCount;
		NextTurn ();
	}

	void Update() {
		if (timerOn) {
			timer -= Time.deltaTime;
		}
		if (timer <= 0f){
			ResetTimer();
		}
	}

	public static void setTimer(bool b){
		timerOn = b;
	}

	public static float GetTimerRatio(){
		return timer / 5f;
	}

	public static void ResetTimer() {
		timer = 5;
		timerOn = false;
		move = 0;
		MoveManager.EndMove();
		NextMove ();
	}

	public static void NextTurn(){
		turn = ++turn % playerCount;
		NextMove ();
	}

	public static void NextMove(){
		if(cameraRefresh != null)
		{
			cameraRefresh();
		}
	}
	public static void Fire(){
		if(fire != null)
		{
			fire(turn);
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