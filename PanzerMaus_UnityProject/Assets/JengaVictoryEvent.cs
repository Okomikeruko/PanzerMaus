using UnityEngine;
using System;
using System.Collections;

public class JengaVictoryEvent : MonoBehaviour {
	public delegate void MyDelegate();
	public static event MyDelegate jengaOne, jengaTwo; 

	void Update (){
		if (jengaOne.GetInvocationList ().Length <= 0) {
			Debug.Log("Player Two Wins");
		}
		if (jengaTwo.GetInvocationList ().Length <= 0) {
			Debug.Log ("Player One Wins");
		}
	}
}

 public enum Team{
	teamOne,
	teamTwo
}