using UnityEngine;
using System.Collections;

public class JengaVictoryEvent : MonoBehaviour {
	public delegate void d();
	public static event d jengaOne, jengaTwo; 

	void Update (){
		if (jengaOne == null) {
			Debug.Log("Player Two Wins");
		}
		if (jengaTwo == null) {
			Debug.Log ("Player One Wins");
		}
	}
}

 public enum Team{
	teamOne,
	teamTwo
}