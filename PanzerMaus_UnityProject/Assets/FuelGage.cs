using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FuelGage : MonoBehaviour {

	private Image image;

	void Start () {
		image = GetComponent<Image>();
	}

	void Update () {
		image.fillAmount = PlayerTurnControl.GetTimerRatio();
	}
}
