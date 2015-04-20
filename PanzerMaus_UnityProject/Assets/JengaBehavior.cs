using UnityEngine;
using System.Collections;

public class JengaBehavior : MonoBehaviour {

	public GameObject line;
	public Team team;

	private bool unsubscribed = false;
	void Start(){
		switch (team) {
		case Team.teamOne:
			JengaVictoryEvent.jengaOne += empty;
			break;
		case Team.teamTwo:
			JengaVictoryEvent.jengaTwo += empty;
			break;
		}
	}

	void Update(){
		if (!unsubscribed && transform.position.y < line.transform.position.y) {
			unsubscribed = true;
			switch (team) {
			case Team.teamOne:
				JengaVictoryEvent.jengaOne -= empty;
				break;
			case Team.teamTwo:
				JengaVictoryEvent.jengaTwo -= empty;
				break;
			}
		}
	}

	void empty (){}
}