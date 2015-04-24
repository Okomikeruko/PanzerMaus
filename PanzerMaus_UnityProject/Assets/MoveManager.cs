using UnityEngine;
using System;
using System.Collections;

public class MoveManager : MonoBehaviour {

	public GameObject[] MoveUI, FireUI;
	public GameObject MovesPanel;

	public static int MovesRemaining;

	public static MoveManager Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		ResetMoves ();
		foreach (GameObject G in MoveUI){
			G.SetActive(false);	
		}
		foreach (GameObject G in FireUI){
			G.SetActive(false);
		}
	}

	public static void ResetMoves()
	{
		MovesRemaining = 1;
	}

	public static void EndMove()
	{
		foreach (GameObject G in Instance.MoveUI){
			G.SetActive(false);	
		}
		foreach (GameObject G in Instance.FireUI){
			G.SetActive(false);
		}
		Instance.MovesPanel.SetActive(true);
		UseMove();
	}

	public static void UseMove()
	{
		MovesRemaining--;
		if (MovesRemaining == 0){
			ResetMoves ();
			PlayerTurnControl.NextTurn();
		}else{
			PlayerTurnControl.NextMove();
		}
	}

	public void ChooseMove(string move){
		switch (move)
		{
		case "Fire":
			foreach (GameObject g in FireUI){
				g.SetActive(true);
			}
			PlayerTurnControl.Fire();
			break;
		case "Move":
			foreach (GameObject g in MoveUI){
				g.SetActive(true);
			}
			break;
		}
	}
}
