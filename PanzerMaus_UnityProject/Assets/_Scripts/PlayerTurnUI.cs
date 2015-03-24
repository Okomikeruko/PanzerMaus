using UnityEngine;
using System.Collections;

public class PlayerTurnUI : MonoBehaviour {

	public GameObject UICanvas;

	public void EnableGui(bool b){
		UICanvas.SetActive(b);
	}
}
